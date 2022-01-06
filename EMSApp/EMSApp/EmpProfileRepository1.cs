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
    internal class EmpProfileRepository1 : IEmpProfileRepository<EmpProfile>
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
        SqlDataAdapter sqlDataAdapter =new SqlDataAdapter();
        DataSet dataSet = new DataSet("NordicEMS");
        public bool DeleteEmployee(EmpProfile empProfile)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "DeleteEmployee";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@EmpCode", SqlDbType.Int).Value = empProfile.EmpCode;

                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataSet);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<EmpProfile> GetAllEmpProfiles()
        {
            var empList = new List<EmpProfile>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "SELECT_ALL_EMP";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand = sqlCommand;
                dataSet.Reset();
                sqlDataAdapter.Fill(dataSet, "EmpAll");

                //Export data into XMl
                dataSet.WriteXml("AllEmp.xml");
                //Export Schema as XSD
                dataSet.WriteXmlSchema("AllEmp.xsd");

                if (dataSet.Tables["EmpAll"].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables["EmpAll"].Rows)
                    {
                        var empProfile = new EmpProfile();
                        empProfile.EmpCode = Convert.ToInt32(dataRow["EmpCode"]);
                        empProfile.EmpName = Convert.ToString(dataRow["EmpName"]);
                        empProfile.DateOfBirth = DateTime.Parse(dataRow["DateOfBirth"].ToString());
                        empProfile.Email = Convert.ToString(dataRow["Email"]);
                        empProfile.DeptCode = Convert.ToInt32(dataRow["DeptCode"]);
                        empList.Add(empProfile);
                    }
                    return empList;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                return null;
            }
        }

        public EmpProfile GetEmployee(int empId)
        {
            EmpProfile empProfile = new EmpProfile();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "SELECT_EMP_BY_CODE";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@EmpCode", SqlDbType.Int).Value =empId;

                sqlDataAdapter.SelectCommand = sqlCommand;
                dataSet.Reset();
                sqlDataAdapter.Fill(dataSet, "Emp");

                if (dataSet.Tables["Emp"].Rows.Count > 0)
                {
                    DataRow dataRow = dataSet.Tables["Emp"].Rows[0];
                    

                    empProfile.EmpCode = Convert.ToInt32(dataRow["EmpCode"]);
                    empProfile.EmpName = Convert.ToString(dataRow["EmpName"]);
                    empProfile.DateOfBirth = DateTime.Parse(dataRow["DateOfBirth"].ToString());
                    empProfile.Email = Convert.ToString(dataRow["Email"]);
                    empProfile.DeptCode = Convert.ToInt32(dataRow["DeptCode"]);

                    
                }
                
                return empProfile;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool SaveEmployee(EmpProfile empProfile)
        {
            try
            {
                //sqlDataAdapter = new SqlDataAdapter("insert into EmpProfile values(" + empProfile.EmpCode + ",'" + empProfile.EmpName + "','" + empProfile.DateOfBirth + "','" + empProfile.Email + "'," + empProfile.DeptCode + ")", sqlConnection);

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "SP_SAVE_EMPLOYEE";

                sqlCommand.Parameters.Add("@EMPCODE", SqlDbType.Int).Value = empProfile.EmpCode;
                sqlCommand.Parameters.Add("@EMPNAME", SqlDbType.VarChar, 50).Value = empProfile.EmpName;
                sqlCommand.Parameters.Add("@DATEOFBIRTH", SqlDbType.DateTime).Value = empProfile.DateOfBirth;
                sqlCommand.Parameters.Add("@EMAIL", SqlDbType.VarChar, 100).Value = empProfile.Email;
                sqlCommand.Parameters.Add("@DEPTCODE", SqlDbType.Int).Value = empProfile.DeptCode;

                sqlDataAdapter.SelectCommand=sqlCommand;

                sqlDataAdapter.Fill(dataSet);

               
                return true;
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool UpdateEmployee(EmpProfile empProfile)
        {
            try
            {

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "UPDATE_EMPLOYEE";

                sqlCommand.Parameters.Add("@EMPCODE", SqlDbType.Int).Value = empProfile.EmpCode;
                sqlCommand.Parameters.Add("@EMPNAME", SqlDbType.VarChar, 50).Value = empProfile.EmpName;
                sqlCommand.Parameters.Add("@DATEOFBIRTH", SqlDbType.DateTime).Value = empProfile.DateOfBirth;
                sqlCommand.Parameters.Add("@EMAIL", SqlDbType.VarChar, 100).Value = empProfile.Email;
                sqlCommand.Parameters.Add("@DEPTCODE", SqlDbType.Int).Value = empProfile.DeptCode;

                sqlDataAdapter.SelectCommand = sqlCommand;

                sqlDataAdapter.Fill(dataSet);
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
