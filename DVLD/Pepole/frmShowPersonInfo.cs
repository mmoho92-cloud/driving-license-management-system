using DVLD.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Pepole
{
    public partial class frmShowPersonInfo : Form
    {
        public frmShowPersonInfo(int PersonID)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }
        public frmShowPersonInfo(string NationalN)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonInfo(NationalN);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }
