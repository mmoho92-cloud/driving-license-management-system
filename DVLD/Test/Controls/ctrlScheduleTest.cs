using DVLD.Global_Classes;
using DVLD.Properties;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Test.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        public enum enMode { Add = 0, Update = 1 }
        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };
        private enMode _Mode { get; set; } = enMode.Add;
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;
        public clsTestType.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {

                    case clsTestType.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }

                    case clsTestType.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestType.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;


                        }
                }
            }
        }

        public void LoadInfo(int LocalDrivingLicenseApplicationID, int AppointmentID = -1)
        {
            if(AppointmentID == -1) 
            {_Mode = enMode.Add; }
            else { _Mode = enMode.Update;}
            _TestAppointmentID = AppointmentID;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.GetApplicationByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Local Driving License Application not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            if (_LocalDrivingLicenseApplication.DoesAttendTestType(_TestTypeID))

                _CreationMode = enCreationMode.RetakeTestSchedule;
            else
                _CreationMode = enCreationMode.FirstTimeSchedule;

            if(_CreationMode==enCreationMode.RetakeTestSchedule)
            {
                lblTitle.Text = "Retake Test Schedule";
                gbRetakeTestInfo.Enabled = true;
                lblRetakeAppFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fees.ToString();
                lblRetakeTestAppID.Text = "0";

            }
            else
            {
                lblTitle.Text = "Schedule Test";
                gbRetakeTestInfo.Enabled = false;
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.ApplicantPersonInfo.FullName;
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();
            lblFees.Text = clsTestType.Find(_TestTypeID).Fees.ToString();

            if(_Mode == enMode.Add)
            {
                dtpTestDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";

                _TestAppointment = new clsTestAppointment();

            }
            else
            {

                if (!_LoadTestAppointmentInfo())
                {
                    return;
                }
            }
            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeAppFees.Text)).ToString();

            if(!_HandleActive_HandleActiveTestAppointmentConstraint())
            {
                return;
            }

            if(!_HandleAppointmentLockedConstraint())
            {
                return;
            }
            
            if(!_HandlePrviousTestConstraint())
            {
                return;
            }

        }

        private bool _LoadTestAppointmentInfo()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if (_TestAppointment == null)
            {
                MessageBox.Show("Test Appointment not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }
            lblFees.Text = _TestAppointment.PaidFees.ToString();

            if(DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
            {
                dtpTestDate.MinDate = DateTime.Now;
            }
            else
            {
                dtpTestDate.MinDate = _TestAppointment.AppointmentDate; 
            }
            dtpTestDate.Value = _TestAppointment.AppointmentDate;

            if(_TestAppointment.RetakeTestApplicationID ==-1)
            {
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                lblRetakeAppFees.Text = _TestAppointment.RetakeTestAppInfo.PaidFees.ToString();
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
            }

            return true;
        }

        private bool _HandleActive_HandleActiveTestAppointmentConstraint()
        {
            if (_Mode == enMode.Add && clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID, TestTypeID)) 
            {

                lblUserMessage.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }
            return true;
        }

        private bool _HandleAppointmentLockedConstraint()
        {
            //if appointment is locked that means the person already sat for this test
            //we cannot update locked appointment
            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person already sat for the test, appointment loacked.";
                dtpTestDate.Enabled = false;
                btnSave.Enabled = false;
                return false;

            }
            else
                lblUserMessage.Visible = false;

            return true;
        }

        private bool _HandlePrviousTestConstraint()
        {
            switch (TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    {
                        lblUserMessage.Visible = false;

                        return true;

                    }
                case clsTestType.enTestType.WrittenTest:
                    {
                        if (!_LocalDrivingLicenseApplication.DoesPassPreviousTest(clsTestType.enTestType.WrittenTest))
                        {
                            lblUserMessage.Visible = true;
                            lblUserMessage.Text = "Person must pass Vision Test before scheduling Written Test.";
                            btnSave.Enabled = false;
                            dtpTestDate.Enabled = false;
                            return false;
                        }
                        else
                        {
                            lblUserMessage.Visible = false;
                            btnSave.Enabled = true;
                            dtpTestDate.Enabled = true;
                        }
                        return true;
                    }
                case clsTestType.enTestType.StreetTest:
                    {
                        if (!_LocalDrivingLicenseApplication.DoesPassPreviousTest(clsTestType.enTestType.WrittenTest))
                        {
                            lblUserMessage.Visible = true;
                            lblUserMessage.Text = "Person must pass Written Test before scheduling Street Test.";
                            btnSave.Enabled = false;
                            dtpTestDate.Enabled = false;
                            return false;
                        }
                        else
                        {
                            lblUserMessage.Visible = false;
                            btnSave.Enabled = true;
                            dtpTestDate.Enabled = true;
                        }
                        return true;
                    }
            }
            return false;
        }

        private bool _HandleRetakeApplication()
        {
            if (_Mode == enMode.Add && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                clsApplication Application = new clsApplication();
                Application.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = _LocalDrivingLicenseApplication.PaidFees ;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                if (!Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;
            }
        
            return true;

        }
        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;

            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
