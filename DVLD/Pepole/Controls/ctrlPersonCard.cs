using DVLD.Properties;
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
using System.Xml.Linq;
using System.IO;

namespace DVLD.Pepole.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        private clsPerson _Person;

        private int _PersonID = -1;

        public int PersonID
        {
            get { return _PersonID; }

        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }

        private void _LoadPersonImage()
        {
            if (_Person.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void _FillPersonInfo()
        {
            _PersonID= _Person.PersonID ;
            llEditPersonInfo.Visible = true;
            lbName2.Text = _Person.FullName;
            lbPersonID2.Text = _Person.PersonID.ToString();
            lbNationalN2.Text = _Person.NationalNo;
            if (_Person.Gendor == 0)
            {
                lbGendor2.Text = "Male";

            }
            else
                lbGendor2.Text = "Female";
            lbEmail2.Text = _Person.Email;
            lbAddress2.Text = _Person.Address;
            lbDateOfBirth2.Text = _Person.DateOfBirth.ToShortDateString();
            lbPhone2.Text = _Person.Phone;
            lbCountry2.Text = _Person.CountryInfo.CountryName;

            _LoadPersonImage();


        }

        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with National No. = " + NationalNo.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        public void ResetPersonInfo()
        {
            llEditPersonInfo.Visible = false;
            lbName2.Text = "?????";
            lbPersonID2.Text = "?????";
            lbNationalN2.Text = "?????";
            lbGendor2.Text = "?????";
            lbEmail2.Text = "?????";
            lbAddress2.Text = "?????";
            lbDateOfBirth2.Text = "?????";
            lbPhone2.Text = "?????";
            lbCountry2.Text = "?????";
            pbPersonImage.Image = Resources.Male_512;

        }

        private void ctrlPersonCard_Load(object sender, EventArgs e)
        {

        }
    }
}
