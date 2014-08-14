using System;
using System.Collections.Generic;
using SAA.Infra;

namespace SAA.Model.Models
{
    public partial class AplicacaoUrl : ModelBase
    {

	
	public AplicacaoUrl(){}
			public override int Id { get; set; }
		      public int IdAplicacao { get; set; }
      public string Url { get; set; }
        public virtual Aplicacao Aplicacao { get; set; }
    }
}
