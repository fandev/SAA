using SAA.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Negocio.Modelos
{
    public class Status
    {

        public bool Succeeded {get;set;}
        public string Token { get; set; }
        public string Message { get; set; }

    }
}
