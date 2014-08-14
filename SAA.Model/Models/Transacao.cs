using System;
using System.Collections.Generic;
using SAA.Infra;

namespace SAA.Model.Models
{
    public partial class Transacao : ModelBase
    {

	
        public Transacao()
        {
            this.Acaos = new List<Acao>();
        }

		public override int Id { get; set; }
		      public string Nome { get; set; }
      public string Hash { get; set; }
      public int IdAplicacao { get; set; }
      public System.DateTime DataCriacao { get; set; }
        public virtual ICollection<Acao> Acaos { get; set; }
        public virtual Aplicacao Aplicacao { get; set; }
    }
}
