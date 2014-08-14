using System;
using System.Collections.Generic;
using SAA.Infra;

namespace SAA.Model.Models
{
    public partial class Acao : ModelBase
    {

	
        public Acao()
        {
            this.FuncionarioPerfilPermissaos = new List<FuncionarioPerfilPermissao>();
        }

		public override int Id { get; set; }
		      public string Hash { get; set; }
      public string Rota { get; set; }
      public string Descricao { get; set; }
      public int IdTansacao { get; set; }
      public System.DateTime DataCriacao { get; set; }
        public virtual ICollection<FuncionarioPerfilPermissao> FuncionarioPerfilPermissaos { get; set; }
        public virtual Transacao Transacao { get; set; }
    }
}
