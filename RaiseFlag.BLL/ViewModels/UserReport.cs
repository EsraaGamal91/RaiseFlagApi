using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaiseFlag.BLL.ViewModels
{
   public class UserReport
    {
        public long id { get; set; }
        public long groupId { get; set; }
        public string fullName { get; set; }
        public string mobile { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public bool outOfRange { get; set; }
        public bool active { get; set; }
        public bool tracked { get; set; }
        public bool notify { get; set; }
        public bool hasReported { get; set; }
    }
}
