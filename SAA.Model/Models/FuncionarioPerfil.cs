using System;
using System.Collections.Generic;
using SAA.Infra;

namespace SAA.Model.Models
{
    public partial class FuncionarioPerfil : ModelBase
    {

	
        public FuncionarioPerfil()
        {
            this.FuncionarioPerfilPermissaos = new List<FuncionarioPerfilPermissao>();
        }

		public override int Id { get; set; }
		      public int IdFuncionario { get; set; }
      public int IdPerfilAplicacao { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public virtual ICollection<FuncionarioPerfilPermissao> FuncionarioPerfilPermissaos { get; set; }
        public virtual PerfilAplicacao PerfilAplicacao { get; set; }
    }
}
