using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Global_Classes;
using DVLD.Tests.Controls;
using DVLD_Buisness;

namespace DVLD.Test
{
    public partial class frmTakeTest : Form
    {
        private int _TestAppointmentID =-1;
        private clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;

        private int _TestID=-1;



        public frmTakeTest(int TestAppointmentID , clsTestType.enTestType TestType)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            _TestType = TestType;
            ctrlSecheduledTest1.LoadInfo(_TestAppointmentID);
        }
        private void _LoadTestInfo()
        {
            

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                      "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No
             )
            {
                return;
            }
            clsTest test = new clsTest();
            test.TestAppointmentID = _TestAppointmentID;
            test.TestResult = rbPass.Checked;
            test.Notes = txtNotes.Text;
            test.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if(test.Save())
            {
                MessageBox.Show("Test results saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to save test results.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
