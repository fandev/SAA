using SAA.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Pattern.Infrastructure;

namespace SAA.Model.Models
{
    public partial class Acao : EntityBase
    {
        [NotMapped]
        public bool autorizado { get; set; }
    }
}
