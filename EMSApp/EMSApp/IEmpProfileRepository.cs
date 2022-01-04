using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSApp.Models;
namespace EMSApp
{
    public interface IEmpProfileRepository<EmpProfile>
    {
        bool SaveEmployee(EmpProfile empProfile);

        bool DeleteEmployee(EmpProfile empProfile);

        bool UpdateEmployee(EmpProfile empProfile);

        EmpProfile GetEmployee(int empId);

        List<EmpProfile> GetAllEmpProfiles();
    }
}
