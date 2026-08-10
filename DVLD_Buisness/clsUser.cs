using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using DVLD_Buisness;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsUser
    {
        public enum enMode { Add =0 , Update = 1}

        public enMode Mode =enMode.Update ;
        public int UserID { get; set; }
        public string UserName { get; set; }

        public int PersonID { get; set; }

        public clsPerson PersonInfo;

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public clsUser()
        {
            UserID = -1;
            UserName = "";
            PersonID = -1;
            PersonInfo = null;
            Password = "";
            IsActive = false;

            Mode = enMode.Add;
        }
        private clsUser(int userID, int personID,string userName, string password, bool isActive)
        {
            Mode = enMode.Update;
            UserID = userID;
            UserName = userName;
            PersonID = personID;
            PersonInfo = clsPerson.Find(personID);
            Password = password;
            IsActive = isActive;
        }

        private bool _AddNewUser ()
        {
            this.UserID = clsUserData.AddUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (this.UserID > 0);
        }
        private bool _UpdateUserInfo()
        {
            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
        }

        //private bool _changeUserPassword()
        //{
        //    return clsUserData.ChangePassword(this.UserID, this.Password);
        //}

        public bool Save()
        {
  
            switch(Mode)
            {
                case enMode.Add:
                    if(_AddNewUser())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateUserInfo();
               
            }
            return false;
            
        }

        static public clsUser FindUserByID (int UserID)
        {
            int PersonID =-1;
            string UserName = "";  string Password= ""; bool IsActive =false;
            if(clsUserData.GetUserByUserID(UserID , ref PersonID ,ref UserName ,ref Password , ref IsActive ))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
        static public clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = ""; string Password = ""; bool IsActive = false;
            if (clsUserData.GetUserByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
        static public clsUser FindByUserNameAndPassword(string UserName , string Password)
        {
            int UserID = -1;
            int PersonID = -1;
            bool IsActive = false;
            if (clsUserData.GetUserByUserNameAndPassword(UserName,Password,ref UserID , ref PersonID ,ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        static public DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }
        static public bool IsUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);

        }
        static public bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }
        public static bool IsUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }
        public bool ChangePassword(string NewPassword)
        {
            if (clsUserData.ChangePassword(this.UserID, NewPassword))
            {
                this.Password = NewPassword;
                return true;
            }

            return false;
        }
    }
}
