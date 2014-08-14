using System;
using System.Collections.Generic;
using SAA.Infra;

namespace SAA.Model.Models
{
    public partial class Token : ModelBase
    {

	
        public Token()
        {
            this.UserTokenAppTokens = new List<UserTokenAppToken>();
        }

		public override int Id { get; set; }
		      public int IdUsuario { get; set; }
      public string Hash { get; set; }
      public System.DateTime DataCriacao { get; set; }
      public string IP { get; set; }
      public string UserAgent { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<UserTokenAppToken> UserTokenAppTokens { get; set; }
    }
}
