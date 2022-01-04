using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSApp.Models;
using System.Data.SqlClient;
using System.Configuration;
namespace EMSApp
{
    //  This program demo how to perform CRUD operations using Connected Model.
    public class EmpProfileRepository : IEmpProfileRepository<EmpProfile>
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataReader sqlDataReader;
        public bool DeleteEmployee(EmpProfile empProfile)
        {
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "delete from EmpProfile where EmpCode="+ empProfile.EmpCode+" ";
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.ExecuteNonQuery();
                return true;

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public List<EmpProfile> GetAllEmpProfiles()
        {
            throw new NotImplementedException();
        }

        public EmpProfile GetEmployee(int empId)
        {
            throw new NotImplementedException();
        }

        public bool SaveEmployee(EmpProfile empProfile)
        {
            try {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "insert into EmpProfile values(" + empProfile.EmpCode + ",'" + empProfile.EmpName + "','" + empProfile.DateOfBirth + "','" + empProfile.Email + "'," + empProfile.DeptCode + ")";
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.ExecuteNonQuery();
                return true;

            }catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public bool UpdateEmployee(EmpProfile empProfile)
        {
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "Update EmpProfile set EmpName='" + empProfile.EmpName+ "',DateOfBirth='" + empProfile.DateOfBirth + "',Email='"+ empProfile.Email +"',DeptCode="+empProfile.DeptCode +" ";
                if (sqlConnection.State == System.Data.ConnectionState.Closed) 
                {
                    sqlConnection.Open();
                }
                sqlCommand.ExecuteNonQuery();
                return true;

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
