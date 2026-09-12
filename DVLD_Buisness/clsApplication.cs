using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Buisness.clsApplication;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enum enApplicationStatus { New=1, Cancelled=2, Completed=3 };
        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };

        public enMode Mode = enMode.AddNew;
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public clsPerson ApplicantPersonInfo { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public enApplicationType ApplicationType { get; set; }
        public clsApplicationType ApplicationTypeInfo { get; set; }
        public enApplicationStatus ApplicationStatus { get; set; }
        public string StatusText
        {
            get
            {

                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }

        }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }

        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            this.Mode = enMode.AddNew;
        }

        private clsApplication( int applicationID, int applicantPersonID,
            DateTime applicationDate, int applicationTypeID,
            enApplicationStatus applicationStatus, DateTime lastStatusDate, 
            float paidFees, int createdByUserID)
        {
         this.Mode = enMode.Update;
         this.ApplicationID = applicationID;
         this.ApplicantPersonID = applicantPersonID;
         this.ApplicantPersonInfo = clsPerson.Find(ApplicantPersonID);
         this.ApplicationDate = applicationDate;
         this.ApplicationTypeID = applicationTypeID;
         this.ApplicationType = (enApplicationType)applicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID) ;
         this.ApplicationStatus = applicationStatus;
         this.LastStatusDate = lastStatusDate;
         this.PaidFees = paidFees;
         this.CreatedByUserID = createdByUserID;
         this.CreatedByUserInfo = clsUser.FindUserByID(CreatedByUserID);
        }

        private bool _AddNewApplication()
        {
           this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
               (int)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
               (byte) ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }

        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now; int ApplicationTypeID = -1;
            int ApplicationStatus = -1; DateTime LastStatusDate = DateTime.Now; float PaidFees = -1; int CreatedByUserID = -1;

            bool isFound = clsApplicationData.GetApplicationByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID,
                ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID);

            if (isFound)
            {
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate,
                    ApplicationTypeID, (enApplicationStatus)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            }
            else
            {
                return null;
            }
        
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    {
                       if(_AddNewApplication())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                       else
                        { return false; }
                    }
                case enMode.Update:
                    return _UpdateApplication();
            }
            return false;
        }

        public bool Cancel()

        {
            return clsApplicationData.UpdateApplicationStatus(ApplicationID, 2);
        }

        public bool SetComplete()

        {
            return clsApplicationData.UpdateApplicationStatus(ApplicationID, 3);
        }

        public bool Delete()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExist(ApplicationID);
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return DoesPersonHaveActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        }

        public int GetActiveApplicationID(clsApplication.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }


    }
}
