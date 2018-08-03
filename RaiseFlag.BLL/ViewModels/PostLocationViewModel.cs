using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaiseFlag.BLL.ViewModels
{
   public class PostLocationViewModel
    {
        public long userId { get; set; }
        public long groupId { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }

    }
}
