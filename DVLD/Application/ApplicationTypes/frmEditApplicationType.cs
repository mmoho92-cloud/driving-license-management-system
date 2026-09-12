using DVLD.Classes;
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

namespace DVLD.Applications
{
    public partial class frmEditApplicationType : Form
    {
        private int _applicationTypeId;
        private clsApplicationType _applicationType;
        public frmEditApplicationType(int applicationTypeId)
        {
            InitializeComponent();
            _applicationTypeId = applicationTypeId;
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            lblApplicationTypeID.Text = _applicationTypeId.ToString();
            txtFees.Text = string.Empty;
            txtTitle.Text = string.Empty;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _applicationType = clsApplicationType.Find(_applicationTypeId);

            if(txtTitle.Text != string.Empty)
            {
                _applicationType.ApplicationTypeName = txtTitle.Text;
            }
            //else
            //{
            //    MessageBox.Show("Application Type Title cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            if(txtFees.Text != string.Empty)
            {
                if (!decimal.TryParse(txtFees.Text, out decimal amount))
                {
                    MessageBox.Show("Please enter a valid amount.");
                    return;
                }
                else
                {
                    _applicationType.Fees = amount;
                }
            }
            //else
            //{
            //    MessageBox.Show("Application Fees cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            if (_applicationType.Save())
            {
                MessageBox.Show("Saved Successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Save Failed.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        private void txtTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ';
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) &&
        !char.IsControl(e.KeyChar) &&
        e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && txtFees.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }
    }
}
