using System;
using System.Collections.Generic;
using SAA.Infra;

namespace SAA.Model.Models
{
    public partial class StatusUsuario : ModelBase
    {

	
        public StatusUsuario()
        {
            this.Usuarios = new List<Usuario>();
        }

		public override int Id { get; set; }
		      public string Codigo { get; set; }
      public string Descricao { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
