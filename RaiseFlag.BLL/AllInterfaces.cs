using RaiseFlag.BLL.ViewModels;
using RaiseFlag.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaiseFlag.BLL
{
   public interface IUsersRepository 

    {
        bool IsUserNameExist(string userName);
        List<UserReport> PostUserLocation(PostLocationViewModel model);
        List<UserReport> GetAllGroupUsers(long groupId);
    }
}
