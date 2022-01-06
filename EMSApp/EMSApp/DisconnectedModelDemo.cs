using EMSApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSApp
{
    internal class DisconnectedModelDemo
    {
        static void Main()
        {
            EmpProfileRepository1 empProfileRepository = new EmpProfileRepository1();
            Console.WriteLine("Enter 1.Save 2.Delete 3.Update 4.GetById 5.GetAll");
            int response = Convert.ToInt32(Console.ReadLine());

            switch (response)
            {
                case 1:
                    EmpProfile empProfile = new EmpProfile { EmpCode = 104, EmpName = "Mary", DateOfBirth = DateTime.Parse("05-05-2020"), Email = ",mary@gmail.com", DeptCode = 101 };
                    var flag = empProfileRepository.SaveEmployee(empProfile);
                    Console.WriteLine(flag == true ? "Employee is Saved" : "Error");
                    break;
                case 2:
                    EmpProfile empProfileToDelete = new EmpProfile { EmpCode = 103, EmpName = "Adam", DateOfBirth = DateTime.Parse("05-01-2020"), Email = "adam@gmail.com", DeptCode = 100 };

                    var flag1 = empProfileRepository.DeleteEmployee(empProfileToDelete);
                    Console.WriteLine(flag1 == true ? "Employee is Deleted" : "Error");
                    break;

                case 3:
                    EmpProfile empProfileToUpdate = new EmpProfile { EmpCode = 100, EmpName = "Scott123", DateOfBirth = DateTime.Parse("05-01-2000"), Email = "scott123@gmail.com", DeptCode = 100 };
                    var flag2 = empProfileRepository.UpdateEmployee(empProfileToUpdate);
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
            Console.ReadLine();
        }
    }
}
