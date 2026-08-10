using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmAddUpdateUser : Form
    {
        private enum enMode { AddNew = 0, Update = 1 }

        private enMode _Mode = enMode.Update;

        private int _UserID;

        public clsUser CurrentUser;

        public frmAddUpdateUser()
        {
            InitializeComponent();
            
            _Mode = enMode.AddNew;
           

        }
        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _UserID = UserID;
        }
        
        //----------------------------------------------------
       
        private void _ResetDefualtValues()
        {
           if(_Mode==enMode.AddNew)
                {
                lTitle.Text = "Add New User";
                this.Text = "Add New User"; 
                CurrentUser = new clsUser();
                tpLoginInfo.Enabled = false;
                ctrlPersonCardWithFilter1.FilterFocus();
            }
           else
            {
                lTitle.Text = "Update User Info";
                this.Text = "Update User Info";
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;

            }

            tbUserName.Text = "";
            tbPassword.Text = "";
            tbConfirmPassword.Text = "";
            chbIsActive.Checked = true;
        }
        private void _LoadData()
        {
            CurrentUser = clsUser.FindUserByID(_UserID);
            ctrlPersonCardWithFilter1.Enabled = false;

            if (CurrentUser==null)
            {
                MessageBox.Show("User was not found.","Not Found",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }
            else
            {
                lUserID.Text = CurrentUser.UserID.ToString();
                tbUserName.Text = CurrentUser.UserName;
                tbPassword.Text = CurrentUser.Password;
                tbConfirmPassword.Text = CurrentUser.Password;
                chbIsActive.Checked = CurrentUser.IsActive;
                ctrlPersonCardWithFilter1.LoadPersonInfo(CurrentUser.PersonID);
            }
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }
  
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                return;
            }

            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {

                if (clsUser.IsUserExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
                {

                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }

                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                }
            }

            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();

            }
        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (tbConfirmPassword.Text.Trim() != tbPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(tbConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
            {
                errorProvider1.SetError(tbConfirmPassword, null);
            }
            ;

        }

        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(tbPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbPassword, "Password cannot be blank");
            }
            else
            {
                errorProvider1.SetError(tbPassword, null);
            }
            ;
        }

        private void tbUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbUserName, "Username cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(tbUserName, null);
            }
            ;


            if (_Mode == enMode.AddNew)
            {

                if (clsUser.IsUserExist(tbUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(tbUserName, "username is used by another user");
                }
                else
                {
                    errorProvider1.SetError(tbUserName, null);
                }
                ;
            }
            else
            {
                if (CurrentUser.UserName != tbUserName.Text.Trim())
                {
                    if (clsUser.IsUserExist(tbUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(tbUserName, "username is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(tbUserName, null);
                    }
                    ;
                }
            }
        }

        private void frmAddUpdateUser_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            CurrentUser.PersonID = ctrlPersonCardWithFilter1.PersonID;
            CurrentUser.UserName = tbUserName.Text.Trim();
            CurrentUser.Password = tbPassword.Text.Trim();
            CurrentUser.IsActive = chbIsActive.Checked;


            if (CurrentUser.Save())
            {
                lUserID.Text = CurrentUser.UserID.ToString();
                _Mode = enMode.Update;
                lTitle.Text = "Update User";
                this.Text = "Update User";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
