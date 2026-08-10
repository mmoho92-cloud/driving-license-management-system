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

namespace DVLD.Users
{
    public partial class ctrlUserInfo : UserControl
    {
        private clsUser _User;

        private int _UserId = -1;

        public int UserId
        {
            get { return _UserId; }
        }
        public ctrlUserInfo()
        {
            InitializeComponent();
            _ResetPersonInfo();
        }

        public void LoadUser(int UserID)
        {
            _User = clsUser.FindUserByID(UserID);

            if (_User == null)
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetPersonInfo();
                return;
            }
            else
            {
                ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
                lUserID.Text = _User.UserID.ToString();
                lUserName.Text = _User.UserName;
                if(_User.IsActive)
                {
                    lIsActive.Text = "Yes";

                }
                else
                {
                    lIsActive.Text = "No";
                }
            }
        }
        private void _ResetPersonInfo()
        {
            ctrlPersonCard1.ResetPersonInfo();
            lUserName.Text = "?????";
            lUserID.Text = "?????";
            lIsActive.Text = "?????";

        }
    }
}
