using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SAA.Model.Models.Mapping;

namespace SAA.Model.Models
{
    public partial class KERBEROSContext : DbContext
    {
        static KERBEROSContext()
        {
            Database.SetInitializer<KERBEROSContext>(null);
        }

        public KERBEROSContext()
            : base("Name=KERBEROSContext")
        {
        }

        public DbSet<Acao> Acaos { get; set; }
        public DbSet<Aplicacao> Aplicacaos { get; set; }
        public DbSet<AplicacaoUrl> AplicacaoUrls { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<FuncionarioPerfil> FuncionarioPerfils { get; set; }
        public DbSet<FuncionarioPerfilPermissao> FuncionarioPerfilPermissaos { get; set; }
        public DbSet<HitoricoFuncionarioPerfilPermissao> HitoricoFuncionarioPerfilPermissaos { get; set; }
        public DbSet<PerfilAplicacao> PerfilAplicacaos { get; set; }
        public DbSet<StatusFuncionario> StatusFuncionarios { get; set; }
        public DbSet<StatusUsuario> StatusUsuarios { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<TipoAplicacao> TipoAplicacaos { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Transacao> Transacaos { get; set; }
        public DbSet<UserTokenAppToken> UserTokenAppTokens { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioAplicacao> UsuarioAplicacaos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AcaoMap());
            modelBuilder.Configurations.Add(new AplicacaoMap());
            modelBuilder.Configurations.Add(new AplicacaoUrlMap());
            modelBuilder.Configurations.Add(new FuncionarioMap());
            modelBuilder.Configurations.Add(new FuncionarioPerfilMap());
            modelBuilder.Configurations.Add(new FuncionarioPerfilPermissaoMap());
            modelBuilder.Configurations.Add(new HitoricoFuncionarioPerfilPermissaoMap());
            modelBuilder.Configurations.Add(new PerfilAplicacaoMap());
            modelBuilder.Configurations.Add(new StatusFuncionarioMap());
            modelBuilder.Configurations.Add(new StatusUsuarioMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new TipoAplicacaoMap());
            modelBuilder.Configurations.Add(new TokenMap());
            modelBuilder.Configurations.Add(new TransacaoMap());
            modelBuilder.Configurations.Add(new UserTokenAppTokenMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new UsuarioAplicacaoMap());
        }
    }
}
