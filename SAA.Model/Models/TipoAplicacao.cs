using System;
using System.Collections.Generic;
using SAA.Infra;

namespace SAA.Model.Models
{
    public partial class TipoAplicacao : ModelBase
    {

	
        public TipoAplicacao()
        {
            this.Aplicacaos = new List<Aplicacao>();
        }

		public override int Id { get; set; }
		      public string Nome { get; set; }
        public virtual ICollection<Aplicacao> Aplicacaos { get; set; }
    }
}
