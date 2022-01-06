using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSApp.Models;
namespace EMSApp
{
    internal class ConnectModelDemo
    {
        static void Main()
        {
            EmpProfileRepository empProfileRepository = new EmpProfileRepository();

            UserInfoRepository userInfoRepository = new UserInfoRepository();
            UserInfo userInfo = new UserInfo {Email="admin@gmail.com" ,Password="admin123"};
            var result = userInfoRepository.ValidateUser(userInfo);
            if (result)
            {
                Console.WriteLine("Enter 1.Save 2.Delete 3.Update 4.GetById 5.GetAll");
                int response = Convert.ToInt32(Console.ReadLine());

                switch (response)
                {
                    case 1:
                        EmpProfile empProfile = new EmpProfile { EmpCode = 103, EmpName = "Adam", DateOfBirth = DateTime.Parse("05-01-2020"), Email = "adam@gmail.com", DeptCode = 100 };
                        var flag = empProfileRepository.SaveEmployee(empProfile);
                        Console.WriteLine(flag == true ? "Employee is Saved" : "Error");
                        Console.WriteLine($"Employee Count is:{empProfileRepository.EmployeeCount()}");
                        break;
                    case 2:
                        EmpProfile empProfileToDelete = new EmpProfile { EmpCode = 103, EmpName = "Adam", DateOfBirth = DateTime.Parse("05-01-2020"), Email = "adam@gmail.com", DeptCode = 100 };

                        var flag1 = empProfileRepository.DeleteEmployee(empProfileToDelete);
                        Console.WriteLine(flag1 == true ? "Employee is Deleted" : "Error");
                        Console.WriteLine($"Employee Count is:{empProfileRepository.EmployeeCount()}");
                        break;

                    case 3:
                        EmpProfile empProfileToUpdate = new EmpProfile { EmpCode = 100, EmpName = "Scott123", DateOfBirth = DateTime.Parse("05-01-2000"), Email = "scott123@gmail.com", DeptCode = 100 };
                        var flag2 = empProfileRepository.UpdateEmployee(empProfileToUpdate);
                        Console.WriteLine(flag2 == true ? "Employee is Updated" : "Error");
                        break;
                    case 4:
                        int empCode = 100;
                        var emp = empProfileRepository.GetEmployee(empCode);
                        if (emp != null)
                        {
                            Console.WriteLine($"{emp.EmpCode}\t{emp.EmpName}\t{emp.DateOfBirth.ToString("dd-MMM-yyyy")}\t{emp.Email}\t{emp.DeptCode}");
                        }
                        else
                        {
                            Console.WriteLine("Employee does not found");
                        }
                        break;
                    case 5:
                        var employees = empProfileRepository.GetAllEmpProfiles();
                        foreach (var employee in employees)
                        {
                            Console.WriteLine($"{employee.EmpCode}\t{employee.EmpName}\t{employee.DateOfBirth.ToString("dd-MMM-yyyy")}\t{employee.Email}\t{employee.DeptCode}");

                        }
                        break;
                }

            }
            else
            {
                Console.WriteLine("Incorrect Email or Password");
            }
            Console.ReadLine();
        }
    }
}
