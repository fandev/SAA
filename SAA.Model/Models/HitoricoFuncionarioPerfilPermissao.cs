using System;
using System.Collections.Generic;
using SAA.Infra;

namespace SAA.Model.Models
{
    public partial class HitoricoFuncionarioPerfilPermissao : ModelBase
    {

	
	public HitoricoFuncionarioPerfilPermissao(){}
			public override int Id { get; set; }
		      public int IdFuncionario { get; set; }
      public int IdFuncionarioPerfilTransacao { get; set; }
      public System.DateTime DataRegistro { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public virtual FuncionarioPerfilPermissao FuncionarioPerfilPermissao { get; set; }
    }
}
