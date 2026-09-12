using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.License
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        private int _PersonId = -1;
        public frmShowPersonLicenseHistory(int PersonId)
        {
            InitializeComponent();
            _PersonId = PersonId;
            
        }
        public frmShowPersonLicenseHistory()
        {
            InitializeComponent();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonId = obj;
            ctrlDriverLicenses1.LoadInfoByPersonID(_PersonId);
  

        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
           if(_PersonId != -1)
            {
                ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonId);
            }

        }
    }
}
