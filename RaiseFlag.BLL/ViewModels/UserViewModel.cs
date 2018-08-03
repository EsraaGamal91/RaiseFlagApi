using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaiseFlag.BLL.ViewModels
{
    public class UserViewModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

        public string ProfilePicture { get; set; }

        public string Password { get; set; }

        public string Mobile { get; set; }

        public DateTime Birthdate { get; set; }

        public string EmergencyNo { get; set; }

        public string PassportID { get; set; }
        public int CountryID { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public int GenderID { get; set; }
        public string RememberToken { get; set; }
        public string DeviceToken { get; set; }





    }
}
