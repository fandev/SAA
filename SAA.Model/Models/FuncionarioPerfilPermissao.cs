using System;
using System.Collections.Generic;
using SAA.Infra;

namespace SAA.Model.Models
{
    public partial class FuncionarioPerfilPermissao : ModelBase
    {

	
        public FuncionarioPerfilPermissao()
        {
            this.HitoricoFuncionarioPerfilPermissaos = new List<HitoricoFuncionarioPerfilPermissao>();
        }

		public override int Id { get; set; }
		      public int IdFuncionarioPerfil { get; set; }
      public int IdAcao { get; set; }
        public virtual Acao Acao { get; set; }
        public virtual FuncionarioPerfil FuncionarioPerfil { get; set; }
        public virtual ICollection<HitoricoFuncionarioPerfilPermissao> HitoricoFuncionarioPerfilPermissaos { get; set; }
    }
}
