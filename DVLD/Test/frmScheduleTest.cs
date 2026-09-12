using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Test.Controls;
using DVLD_Buisness;

namespace DVLD.Test
{
    public partial class frmScheduleTest : Form
    {

        private int _LocalDrivingLicenseID =-1;

        private clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;

        private int _AppointmentID = -1;

        public frmScheduleTest(int localDrivingLicenseID, clsTestType.enTestType testType , int appointmentID=-1)
        {
            InitializeComponent();
            _LocalDrivingLicenseID = localDrivingLicenseID;
            _TestType = testType;
            _AppointmentID = appointmentID;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            crlScheduleTest1.TestTypeID = _TestType;
            crlScheduleTest1.LoadInfo(_LocalDrivingLicenseID,_AppointmentID);

        }
    }
}
