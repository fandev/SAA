using System;
using System.Collections.Generic;
using SAA.Infra;

namespace SAA.Model.Models
{
    public partial class Usuario : ModelBase
    {

	
        public Usuario()
        {
            this.Tokens = new List<Token>();
            this.UsuarioAplicacaos = new List<UsuarioAplicacao>();
        }

		public override int Id { get; set; }
		      public int IdFuncionario { get; set; }
      public string Login { get; set; }
      public string Senha { get; set; }
      public int IdStatus { get; set; }
      public bool LogOut { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public virtual StatusUsuario StatusUsuario { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
        public virtual ICollection<UsuarioAplicacao> UsuarioAplicacaos { get; set; }
    }
}
