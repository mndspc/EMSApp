using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSApp.Models;
namespace EMSApp
{
    public interface IUserInfoRepository<UserInfo>
    {
        bool AddUser(UserInfo userInfo);

        bool UpdateUser(UserInfo userInfo); 

        bool DeleteUser(UserInfo userInfo);

        bool ValidateUser(UserInfo userInfo);
    }
}
