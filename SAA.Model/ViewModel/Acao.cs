using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Model.Models
{
    public partial class Acao
    {
        [NotMapped] 
        public bool Autorizado { get; set; }
    }
}
