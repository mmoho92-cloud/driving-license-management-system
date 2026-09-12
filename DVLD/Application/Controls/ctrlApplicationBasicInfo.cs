using DVLD.Application.Local_Driving_License;
using DVLD.Classes;
using DVLD.Pepole;
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

namespace DVLD.Application.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private int _ApplicationID = -1;
        private clsApplication _Application { get; set; }

        public int ApplicationID { get { return _ApplicationID; } }


        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
            
        }

        public  void RestartAllValues()
        {
            _ApplicationID = -1;
            lblApplicationID.Text = "[???]";
            lblStatus.Text = "[???]";
            lblFees.Text = "[???]";
            lblType.Text = "[???]";
            lblApplicant.Text = "[???]";
            lblDate.Text = "[???/???/????]";
            lblStatusDate.Text = "[???/???/????]";
            lblCreatedByUser.Text = "[???]";
            llViewPersonInfo.Visible = false;
        }

        private void _FeelApplicationInfo()
        {
            _ApplicationID = _Application.ApplicationID;
            lblApplicationID.Text = _Application.ApplicationID.ToString();
            lblStatus.Text = _Application.StatusText;
            lblType.Text = _Application.ApplicationTypeInfo.ApplicationTypeName;
            lblFees.Text = _Application.PaidFees.ToString();
            lblApplicant.Text = _Application.ApplicantPersonInfo.FullName;
            lblDate.Text = clsFormat.DateToShort(_Application.ApplicationDate);
            lblStatusDate.Text = clsFormat.DateToShort(_Application.LastStatusDate);
            lblCreatedByUser.Text = _Application.CreatedByUserInfo.UserName;

        }

        public void LoadApplicationInfo(int ApplicationID)
        {
           _Application = clsApplication.FindBaseApplication(ApplicationID);
            if(_Application == null)
            {
                RestartAllValues();
                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _FeelApplicationInfo();
            }
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(_Application.ApplicantPersonID);
            frm.ShowDialog();

            LoadApplicationInfo(_Application.ApplicationID);
        }

        
    }
}
