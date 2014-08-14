using System;
using System.Collections.Generic;
using SAA.Infra;

namespace SAA.Model.Models
{
    public partial class PerfilAplicacao : ModelBase
    {

	
        public PerfilAplicacao()
        {
            this.FuncionarioPerfils = new List<FuncionarioPerfil>();
        }

		public override int Id { get; set; }
		      public Nullable<int> IdAplicacao { get; set; }
      public string Nome { get; set; }
        public virtual Aplicacao Aplicacao { get; set; }
        public virtual ICollection<FuncionarioPerfil> FuncionarioPerfils { get; set; }
    }
}
