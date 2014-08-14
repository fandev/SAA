using System;
using System.Collections.Generic;
using SAA.Infra;

namespace SAA.Model.Models
{
    public partial class Funcionario : ModelBase
    {

	
        public Funcionario()
        {
            this.FuncionarioPerfils = new List<FuncionarioPerfil>();
            this.Usuarios = new List<Usuario>();
            this.HitoricoFuncionarioPerfilPermissaos = new List<HitoricoFuncionarioPerfilPermissao>();
        }

		public override int Id { get; set; }
		      public string Nome { get; set; }
      public int Matricula { get; set; }
      public int IdStatus { get; set; }
        public virtual StatusFuncionario StatusFuncionario { get; set; }
        public virtual ICollection<FuncionarioPerfil> FuncionarioPerfils { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<HitoricoFuncionarioPerfilPermissao> HitoricoFuncionarioPerfilPermissaos { get; set; }
    }
}
