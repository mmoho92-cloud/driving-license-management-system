using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsApplicationType
    {
        private enum enMode
        {
            Add = 1,
            Update = 2,
        }
        private enMode _Mode = enMode.Add;
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeName { get; set; }
        public decimal Fees { get; set; }
        public clsApplicationType(int applicationTypeID, string applicationTypeName, decimal fees)
        {
            _Mode = enMode.Update;
            ApplicationTypeID = applicationTypeID;
            ApplicationTypeName = applicationTypeName;
            Fees = fees;
        }

        private bool _Update()
        {
            return clsApplicationTypeData.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeName, this.Fees);
        }
        private bool _AddNew()
        {
            this.ApplicationTypeID = clsApplicationTypeData.AddApplicationType(this.ApplicationTypeName, this.Fees);

            return this.ApplicationTypeID != -1 ;
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Add:
                    if (_AddNew())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    if (_Update())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
            return false;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationType();
        }

        public static clsApplicationType Find(int ApplicationID)
        {
            string Title = string.Empty; decimal Fees = -1;

            if (clsApplicationTypeData.GetApplicationType(ApplicationID, ref Title, ref Fees)) 
            {
                return new clsApplicationType(ApplicationID, Title, Fees);
            }
            else
            {
                return null;
            }
        }

    }

}
