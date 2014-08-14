using System;
using System.Collections.Generic;
using SAA.Infra;

namespace SAA.Model.Models
{
    public partial class UserTokenAppToken : ModelBase
    {

	
	public UserTokenAppToken(){}
			public override int Id { get; set; }
		      public int IdUserToken { get; set; }
      public int IdAplicacao { get; set; }
      public System.DateTime DataExpiracao { get; set; }
      public System.DateTime DataRegistro { get; set; }
        public virtual Aplicacao Aplicacao { get; set; }
        public virtual Token Token { get; set; }
    }
}
