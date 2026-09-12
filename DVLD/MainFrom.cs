using DVLD.Login;
using DVLD.People;
using DVLD.Pepole;
using DVLD.Users;
using DVLD.Drivers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Global_Classes;
using DVLD.Application.ApplicationTypes;
using DVLD.Test;
using DVLD.Application;
using DVLD.Application.Local_Driving_License;
using DVLD.Tests;
using DVLD.Application.Renew_Local_License;
using DVLD.Application.ReplaceLostOrDamagedLicense;
using DVLD.License.Detain_License;
using DVLD.Application.Rlease_Detained_License;
using DVLD.Application.International_License;


namespace DVLD
{
    public partial class MainFrom : Form
    {
        private frmLogin _frmLogin;
        public MainFrom(frmLogin frm)
        {
            InitializeComponent();
            _frmLogin = frm;
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLIstPeople frm = new frmLIstPeople();
            frm.ShowDialog();
        }

        private void MainFrom_Load(object sender, EventArgs e)
        {

        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUser frm = new frmListUser();
            frm.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }


        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmLogin.Show();
            this.Close();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           frmListApplicationTypes frm = new frmListApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes();
            frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
             frmAddUpdateLocalDrivingLicesnseApplication frm = new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
        }

        private void drivingLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void oNewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageLocalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicesnseApplications frm = new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicesnseApplications frm = new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication frm = new frmRenewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void ReplacementLostOrDamagedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLostOrDamagedLicenseApplication frm = new frmReplaceLostOrDamagedLicenseApplication();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frm = new frmListDrivers();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void ManageDetainedLicensestoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void ManageInternationaDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListInternationalLicesnseApplications frm = new frmListInternationalLicesnseApplications();
            frm.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }
    }
       
}
