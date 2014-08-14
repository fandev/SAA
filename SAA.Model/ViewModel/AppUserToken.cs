using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Model.ViewModel
{
    public class AppUserToken
    {
        public string AppId { get; set; }
        public string UserToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
