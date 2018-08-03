using RaiseFlag.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaiseFlag.ServiceLayer
{
   public interface IUsersFunction
    {
        bool IsUserExist(string UserName);
        List<UserReport> PostUserLocation(PostLocationViewModel model);
        List<UserReport> GetAllUsers(long groupId);
    }
}
