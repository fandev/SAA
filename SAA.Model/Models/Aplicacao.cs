using System;
using System.Collections.Generic;
using SAA.Infra;

namespace SAA.Model.Models
{
    public partial class Aplicacao : ModelBase
    {

	
        public Aplicacao()
        {
            this.AplicacaoUrls = new List<AplicacaoUrl>();
            this.PerfilAplicacaos = new List<PerfilAplicacao>();
            this.Transacaos = new List<Transacao>();
            this.UserTokenAppTokens = new List<UserTokenAppToken>();
            this.UsuarioAplicacaos = new List<UsuarioAplicacao>();
        }

		public override int Id { get; set; }
		      public string AppId { get; set; }
      public string AppKey { get; set; }
      public string Nome { get; set; }
      public System.DateTime DataCriacao { get; set; }
      public System.DateTime DataExpiracao { get; set; }
      public int IdTipoAplicacao { get; set; }
        public virtual TipoAplicacao TipoAplicacao { get; set; }
        public virtual ICollection<AplicacaoUrl> AplicacaoUrls { get; set; }
        public virtual ICollection<PerfilAplicacao> PerfilAplicacaos { get; set; }
        public virtual ICollection<Transacao> Transacaos { get; set; }
        public virtual ICollection<UserTokenAppToken> UserTokenAppTokens { get; set; }
        public virtual ICollection<UsuarioAplicacao> UsuarioAplicacaos { get; set; }
    }
}
