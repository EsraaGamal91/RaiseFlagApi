using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaiseFlag.DAL.Models;
using RaiseFlag.BLL.ViewModels;
using RaiseFlag.BLL;

namespace RaiseFlag.ServiceLayer
{
   public class UsersFunction : IUsersFunction
    {
        private readonly Lazy<IUsersRepository> _usersRepository;
        public UsersFunction(Lazy<IUsersRepository> usersRepository)
        {

            _usersRepository = usersRepository;
        }

        private IUsersRepository UsersRepository => _usersRepository.Value;
        public bool IsUserExist(string UserName)
        {
            return false;
        }

     public List<UserReport> PostUserLocation(PostLocationViewModel model)
        {

            var result = UsersRepository.PostUserLocation(model);
            return result ;
        }

        public List<UserReport> GetAllUsers(long groupId)
        {

          var result =  UsersRepository.GetAllGroupUsers(groupId);
            return result;
        }

    }
}
