using SAA.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Model.ViewModel
{
    public class UserInfo : ModelBase
    {
        public string login { get; set; }
        public string Name { get; set; }
        public int Registration { get; set; }
        public bool Active { get; set; }
        public bool IsDomainLogin { get; set; }

    }
}
