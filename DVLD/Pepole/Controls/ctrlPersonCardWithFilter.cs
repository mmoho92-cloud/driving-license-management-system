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

namespace DVLD.Pepole.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

       
      
        public event Action<int> OnPersonSelected;

        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID); 
            }
        }

        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAdd.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }

        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }

        
        private enum enFindBy
        {
            PersonID = 0,
            NationalNumber = 1,

        }
        private enFindBy _FindBy = enFindBy.PersonID;

        private void cbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbFindBy.Enabled = true;
            _FindBy = (enFindBy)cbFindBy.SelectedIndex; 
             tbFindBy.Clear();
            btnSearch.Enabled = false;
            tbFindBy.Focus();
          
        }

        public void LoadPersonInfo(int personID)
        {
            cbFindBy.SelectedIndex = (int)enFindBy.PersonID;
            tbFindBy.Text = personID.ToString();

            _FindNow();
        }
        public void LoadPersonInfo(string NationalaN)
        {
            cbFindBy.SelectedIndex = (int)enFindBy.NationalNumber;
            tbFindBy.Text = NationalaN ;
            _FindNow();
        }
        private void _FindNow()
        {
            switch (_FindBy)
            {
                case enFindBy.PersonID:
                    ctrlPersonCard1.LoadPersonInfo(int.Parse(tbFindBy.Text.Trim()));
                    break;
                case enFindBy.NationalNumber:
                    ctrlPersonCard1.LoadPersonInfo(tbFindBy.Text.Trim());
                    break;
            }

            if (OnPersonSelected != null && FilterEnabled)
               
                OnPersonSelected(ctrlPersonCard1.PersonID);

        }

        public void FilterFocus()
        {
           tbFindBy.Focus();
        }

        private void tbFindBy_TextChanged(object sender, EventArgs e)
        {
            if(_FindBy == enFindBy.PersonID)
            {
                btnSearch.Enabled = (clsValidatoin.ValidateInteger(tbFindBy.Text.Trim()));

                if (btnSearch.Enabled)
                    errorProvider1.SetError(tbFindBy, "");
            }
            else
            {
                btnSearch.Enabled = !string.IsNullOrEmpty(tbFindBy.Text.Trim());
            }
            


        }

        private void tbFindBy_Validating(object sender, CancelEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(tbFindBy.Text))
            {
                if (_FindBy == enFindBy.PersonID)
                {
                    if (!clsValidatoin.ValidateInteger(tbFindBy.Text.Trim()))
                    {
                        //e.Cancel = true;
                        tbFindBy.Focus();
                        errorProvider1.SetError(tbFindBy, "Please Enter Only Numbers. ");
                    }
                    else
                    {
                        e.Cancel = false;
                        btnSearch.Enabled = true;
                        errorProvider1.SetError(tbFindBy, "");
                    }

                }
            }
            else
            {
                //e.Cancel = true;
                tbFindBy.Focus();
                errorProvider1.SetError(tbFindBy, "Please Dont Enter White Spaces");

            }
            
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(tbFindBy.Text.Trim()))
            {
                _FindNow();
            }
            else
            {
                MessageBox.Show("Please Enter Value to Search. ", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.DataBack += _DataResived;
            frm.ShowDialog();
        }

        private void _DataResived(object sender , int PersonID)
        {
            cbFindBy.SelectedIndex = 0;
            tbFindBy.Text = PersonID.ToString();
            _FindNow();

        }

        private void tbFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch.PerformClick();
            }

            
            if (_FindBy == enFindBy.PersonID)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }

}
