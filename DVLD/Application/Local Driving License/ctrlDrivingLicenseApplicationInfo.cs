using DVLD.Applications;
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
using DVLD.License.Local_Driving_License;

namespace DVLD.Application.Local_Driving_License
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        private int _LocalDrivingLicenseApplicationID=-1;

        private int _LicenseID=-1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void _ResetAllValues()
        {
            _LocalDrivingLicenseApplicationID = -1;
            ctrlApplicationBasicInfo1.RestartAllValues();
            lblLocalDrivingLicenseApplicationID.Text = "[????]";
            lblAppliedFor.Text = "[????]";
        }

        private void _FillAllValues()
        {
            _LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();

            //incase there is license enable the show link.
            llShowLicenceInfo.Enabled = (_LicenseID != -1);


            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedFor.Text = clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName;
            lblPassedTests.Text = _LocalDrivingLicenseApplication.GetPassedTestCount().ToString() + "/3";
            ctrlApplicationBasicInfo1.LoadApplicationInfo(_LocalDrivingLicenseApplication.ApplicationID);
        }

        public void LoadApplicationInfoByLocalDrivingAppID(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.GetApplicationByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                _ResetAllValues();


                MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillAllValues();
        }

        public void LoadApplicationInfoByApplicationID(int ApplicationID)
        {
          
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.GetApplicationByLocalDrivingLicenseAppID(ApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                _ResetAllValues();


                MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillAllValues();
        }

     
        private void llShowLicenceInfo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_LicenseID);
            frm.ShowDialog();
        }

        
    }
}
