using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsCountry
    {
        public int ID { set; get; }
        public string CountryName { set; get; }

        public clsCountry()
        {
            this.ID = -1;
            this.CountryName = "";
        }

        private clsCountry(int iD, string countryName)
        {
            ID = iD;
            CountryName = countryName;
        }

        public static clsCountry Find(int ID)
        {
            string CountryName = "";
            if (clsCountryData.GetCountryInfoByID(ID, ref CountryName)) 
            {
                return new clsCountry(ID, CountryName);
            }
            else
            {
                return null;
            }
        }

        public static clsCountry Find(string CountryName)
        {
            int ID = 0;

            if(clsCountryData.GetCountryInfoByName(CountryName , ref ID))
            {
                return new clsCountry(ID,CountryName);

            }
            return null;
        }

        public static DataTable GetAllCountries ()
        {
            return clsCountryData.GetAllCountries();
        }
       
    }
}
