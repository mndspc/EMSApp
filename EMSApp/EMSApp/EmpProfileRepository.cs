using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSApp.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

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
            finally
            {
                sqlConnection.Close();
            }

        }

        public List<EmpProfile> GetAllEmpProfiles()
        {
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "Select * from EmpProfile";
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    List<EmpProfile> empProfiles=new List<EmpProfile>();
                    while (sqlDataReader.Read())
                    {
                        EmpProfile empProfile = new EmpProfile();
                        empProfile.EmpCode = Convert.ToInt32(sqlDataReader["EmpCode"]);
                        empProfile.EmpName = Convert.ToString(sqlDataReader["EmpName"]);
                        empProfile.DateOfBirth = DateTime.Parse(sqlDataReader["DateOfBirth"].ToString());
                        empProfile.Email = Convert.ToString(sqlDataReader["Email"]);
                        empProfile.DeptCode = Convert.ToInt32(sqlDataReader["DeptCode"]);
                        empProfiles.Add(empProfile);
                       
                    }
                    return empProfiles;
                }
                else
                {
                    return null;
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                sqlDataReader.Close();
                sqlConnection.Close();
            }
        }

        public EmpProfile GetEmployee(int empId)
        {
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "Select * from EmpProfile where EmpCode=" + empId +"";
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    sqlDataReader.Read();
                    EmpProfile empProfile = new EmpProfile();
                    empProfile.EmpCode = Convert.ToInt32(sqlDataReader["EmpCode"]);
                    empProfile.EmpName = Convert.ToString(sqlDataReader["EmpName"]);
                    empProfile.DateOfBirth =DateTime.Parse( sqlDataReader["DateOfBirth"].ToString());
                    empProfile.Email = Convert.ToString(sqlDataReader["Email"]);
                    empProfile.DeptCode = Convert.ToInt32(sqlDataReader["DeptCode"]);
                    return empProfile;
                }
                else
                {
                    return null;
                }

            }catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                sqlDataReader.Close();
                sqlConnection.Close();
            }
        }

        public bool SaveEmployee(EmpProfile empProfile)
        {
            try {
                sqlCommand.Connection = sqlConnection;
                //sqlCommand.CommandText = "insert into EmpProfile values(" + empProfile.EmpCode + ",'" + empProfile.EmpName + "','" + empProfile.DateOfBirth + "','" + empProfile.Email + "'," + empProfile.DeptCode + ")";

                sqlCommand.CommandText = "SP_SAVE_EMPLOYEE";
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@EMPCODE", SqlDbType.Int).Value = empProfile.EmpCode;
                sqlCommand.Parameters.Add("@EMPNAME", SqlDbType.VarChar, 50).Value = empProfile.EmpName;
                sqlCommand.Parameters.Add("@DATEOFBIRTH", SqlDbType.DateTime).Value = empProfile.DateOfBirth;
                sqlCommand.Parameters.Add("@EMAIL", SqlDbType.VarChar, 100).Value = empProfile.Email;
                sqlCommand.Parameters.Add("@DEPTCODE", SqlDbType.Int).Value = empProfile.DeptCode;


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
            finally
            {
                sqlConnection.Close();
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
            finally
            {
                sqlConnection.Close();
            }
        }

        public int EmployeeCount()
        {
            int count = 0;
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select Count(*) from EmpProfile";
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                count = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return count;
            }catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return count;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
