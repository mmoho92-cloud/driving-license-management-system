using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.License.Local_Driving_License
{
    public partial class frmShowLicenseInfo : Form
    {
        private int _LocalDrivingLicenseID = -1;
        public frmShowLicenseInfo(int LocalDrivingLicenseID)
        {
            InitializeComponent();
            _LocalDrivingLicenseID = LocalDrivingLicenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfo1.LoadInfo(_LocalDrivingLicenseID);
        }
    }
}
