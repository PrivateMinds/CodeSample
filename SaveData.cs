using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleDataFactory.DataModels;

namespace SampleDataFactory
{
    /// <summary>
    /// Date: 01-22-2020
    /// This is an example of an ADO. NET middle-tier save data class.   
    /// This class contains method (s) that perform the following actions:
    ///     1- Save and update data.
    /// </summary>
    /// 
    public class SaveData
    {
        /// <summary>
        /// ===== Example Method 1 ======
        /// This method is to be used to save new or update existing employee  information.
        /// </summary>
        /// <param name="SampleEmployeeDataModel"></param>
        /// <returns>
        /// returns an actual ID for 'success' and 0 for 'failure'
        /// </returns>
        public string SaveEmployeeInformation(SampleEmployeeDataModel SampleEmployeeDataModel)
        {
            //variable that holds the returned id.
            string NewID = "0";

            //connection string
            SqlConnection SQLConn = new SqlConnection();
            SQLConn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeDataBaseConnectionString"].ConnectionString;

            //open connection
            SQLConn.Open();

            //try catch block
            try
            {
                //stored procedure name
                SqlCommand SQLComm = new SqlCommand("[ssp_tblEmployee_SaveNewOrUpdateEmployeeInformation]", SQLConn);
                SQLComm.CommandType = CommandType.StoredProcedure;

                //passed parameters 
                SQLComm.Parameters.AddWithValue("@ID", SampleEmployeeDataModel.ID);
                SQLComm.Parameters.AddWithValue("@FirstName", SampleEmployeeDataModel.FirstName);
                SQLComm.Parameters.AddWithValue("@LastName", SampleEmployeeDataModel.LastName);
                SQLComm.Parameters.AddWithValue("@DOB", SampleEmployeeDataModel.DOB);
                SQLComm.Parameters.AddWithValue("@SSN", SampleEmployeeDataModel.SSN);

                //Returned ID as an output parameter.
                SQLComm.Parameters.Add("@new_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                
                SQLComm.ExecuteNonQuery();

                //set your variable to the returned value
                NewID = SQLComm.Parameters["@new_ID"].Value.ToString();
            }

            catch (Exception e)
            {
                string msg = e.Message;
                NewID = NewID + " " + msg;
            }
            finally
            {
                //close connection
                SQLConn.Close();
            }

            return NewID;
        }
    }
}
