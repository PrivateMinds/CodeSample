using SampleDataFactory.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Date: 01-22-2020
/// This is an example of an ADO.NET middle-tier get data class.   
/// This class contains method (s) that perform the following actions:
///     1- validates user access.
///     2- Retrieves  data from the database.
/// </summary>
/// 
namespace SampleDataFactory
{
    public class GetData
    {
        
        /// <summary>
        ///  ===== EXAMPLE METHOD 1 ============
        ///  This  method is to be used to validate user access. 
        /// </summary>
        /// <param name="CurrentUser"></param>
        /// <returns>
        ///  Returns true for 'granted access' and false for 'denied access'</returns>
        public string CheckuserSecurityAccess(string CurrentUser)
        {
            string ReturnedValue = "false";

            SqlConnection SQLConn = new SqlConnection();
            SqlCommand SQLComm = new SqlCommand();
            SqlDataReader SQLRec;

            //connection string to the database
            SQLConn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeDataBaseConnectionString"].ConnectionString;

            //open connection
            SQLConn.Open();

            //stored procedure name
            string SQL = "ssp_tblOperator_GetUserAccess";
            SQLComm = new SqlCommand(SQL, SQLConn);
            SQLComm.CommandType = CommandType.StoredProcedure;

           //parameter passed
            SQLComm.Parameters.AddWithValue("@CurrentUser", CurrentUser);

            SQLRec = SQLComm.ExecuteReader();

            try
            {
                if (SQLRec.Read())
                {
                    //returned results
                    ReturnedValue = SQLRec.GetString(SQLRec.GetOrdinal("UserHasAccess"));
                }
            }
            catch (Exception e)
            {

                ReturnedValue = e.Message;

            }

            //close connection
            SQLRec.Close();
            SQLConn.Close();
            
            return ReturnedValue;
        }

        /// <summary>
        /// ===== EXAMPLE METHOD 2 ============
        /// </summary>
        /// This method is to be used to return a list of employee information.
         /// <returns> 
         ///  A list of employee information
         /// </returns>
        public List<SampleEmployeeDataModel> GetCodeSampleList()
        {
            
            List<SampleEmployeeDataModel> SampleDataModel = new List<SampleEmployeeDataModel>();

            SqlConnection SQLconn = new SqlConnection();
            SqlCommand SQLcomm = new SqlCommand();
            SqlDataReader SQLRec;

            //connection string to the database
            SQLconn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeDataBaseConnectionString"].ConnectionString;

            //open connection
            SQLconn.Open();

            //stored procedure name
            string SQL = "ssp_tblEmployee_GetEmployeeInformationList";

            SQLcomm = new SqlCommand(SQL, SQLconn);
            SQLcomm.CommandType = CommandType.StoredProcedure;
            
            SQLRec = SQLcomm.ExecuteReader();

            //read data into the model
            while (SQLRec.Read())
            {
                SampleDataModel.Add(new SampleEmployeeDataModel
                {
                    ID = SQLRec.GetInt32(SQLRec.GetOrdinal("ID")),
                    FirstName = SQLRec.GetString(SQLRec.GetOrdinal("FirstName")),
                    LastName = SQLRec.GetString(SQLRec.GetOrdinal("LastName")),
                    DOB = SQLRec.GetDateTime(SQLRec.GetOrdinal("DOB")),
                    SSN = SQLRec.GetString(SQLRec.GetOrdinal("SSN")),
                 
                 
                });
            }

            //close connection
            SQLRec.Close();
            SQLconn.Close();

            return SampleDataModel;
        }

    }
}
