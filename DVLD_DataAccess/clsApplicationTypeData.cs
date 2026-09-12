using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsApplicationTypeData
    {
        public static bool GetApplicationType(int ApplicationTypeID, ref string ApplicationTypeTitle, ref decimal Fees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {


                    isFound = true;

                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    Fees = (decimal)reader["ApplicationFees"];


                }
                else
                {
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {

                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static DataTable GetAllApplicationType()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM ApplicationTypes order by ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static bool UpdateApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, decimal Fees)
        {
            int RowsEffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE ApplicationTypes SET ApplicationTypeTitle = @ApplicationTypeTitle,ApplicationFees = @ApplicationFees WHERE ApplicationTypeID = @ApplicationTypeID; ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                RowsEffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("DAL : " + ex.Message);

            }
            finally
            {
                connection.Close();
            }
            return (RowsEffected > 0);
        }

        public static int AddApplicationType(string ApplicationTypeTitle, decimal Fees)
        {
            int ApplicationTypeID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO ApplicationTypes (ApplicationTypeTitle,ApplicationFees) VALUES (@ApplicationTypeTitle,@ApplicationFees); SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int newId))
                {
                    ApplicationTypeID = newId;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("DAL : " + ex.Message);

            }
            finally
            {
                connection.Close();
            }
            return ApplicationTypeID;

        }

    }
}

