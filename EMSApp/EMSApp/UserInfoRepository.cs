using EMSApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSApp
{
    public class UserInfoRepository : IUserInfoRepository<UserInfo>
    {

        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataReader sqlDataReader;
        public bool AddUser(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }

        public bool ValidateUser(UserInfo userInfo)
        {
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "VALIDATE_USER";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@EMAIL", SqlDbType.VarChar, 50).Value = userInfo.Email;
                sqlCommand.Parameters.Add("@PASSWORD", SqlDbType.VarChar, 20).Value = userInfo.Password;
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (SqlException ex)
            {
                return false;
            }
            finally
            {
                sqlDataReader.Close();
                sqlConnection.Close();
            }
        }
    }
}
