using System;
using System.Collections.Generic;
using SAA.Infra;

namespace SAA.Model.Models
{
    public partial class UsuarioAplicacao : ModelBase
    {

	
	public UsuarioAplicacao(){}
			public override int Id { get; set; }
		      public int IdAplicacao { get; set; }
      public int IdUsuario { get; set; }
      public bool LogOut { get; set; }
      public System.DateTime DataRegistro { get; set; }
        public virtual Aplicacao Aplicacao { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
