using SAA.Infra;
using SAA.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SAA.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _dbContext;

        public DbContext Context
        {
            get
            {
                Type tipo = _dbContext.GetType();
                return _dbContext ?? (DbContext)Activator.CreateInstance(tipo);
            }
        }
        public UnitOfWork(DbContext context)
        {
            if (context != null)
                throw new ArgumentNullException("DbContext", "O contexto para o UnitOfWork não deve ser nulo");
            else
                _dbContext = context;
        }
        /// <summary>
        /// Cria instância usando um contexto padrão
        /// Proxy e LazyLoading são desabilitados para o DbContext
        /// </summary>
        public UnitOfWork()
        {
            _dbContext = new KERBEROSContext();
            _dbContext.Configuration.ProxyCreationEnabled = false;
            _dbContext.Configuration.LazyLoadingEnabled = false;
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                // DebugChangeTracker();
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveChanges()
        {
            try
            {
                // DebugChangeTracker();
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private void DebugChangeTracker()
        {
            var path = "C:\\teste\\";
            path = path + "changeTracker.log";

            using (StreamWriter sw = new StreamWriter(path))
            {
                var changeTracker = _dbContext.ChangeTracker;
                var entries = changeTracker.Entries();
                foreach (var x in entries)
                {

                    var name = x.Entity.ToString();
                    var state = x.State;

                    sw.WriteLine("");
                    sw.WriteLine("***Entity Name: " + name +
                                 "is in a state of " + state);
                    var currentValues = x.CurrentValues;
                    sw.WriteLine("***CurrentValues***");
                    PrintPropertyValues(currentValues, sw);
                    if (state != EntityState.Added)
                    {
                        sw.WriteLine("***Original Values***");
                        PrintPropertyValues(x.OriginalValues, sw);
                    }

                }
            }
        }

        private void PrintPropertyValues(System.Data.Entity.Infrastructure.DbPropertyValues currentValues, StreamWriter sw)
        {
            foreach (var item in currentValues.PropertyNames)
            {
                var valor = currentValues[item];
                sw.WriteLine(item + ": " + valor);
            }
        }

        private FuncionarioRepository _FuncionarioRepository;
        private AcaoRepository _AcaoRepository;
        private TransacaoRepository _TransacaoRepository;
        private AplicacaoRepository _AplicacaoRepository;
        private PerfilAplicacaoRepository _PerfilAplicacaoRepository;
        private TipoAplicacaoRepository _TipoAplicacaoRepository;
        private FuncionarioPerfilPermissaoRepository _FuncionarioPerfilPermissaoRepository;
        private FuncionarioPerfilRepository _UsuarioPerfilRepository;
        private UsuarioRepository _UsuarioRepository;
        private TokenRepository _TokenRepository;
        private StatusUsuarioRepository _StatusUsuarioRepository;
        private UsuarioAplicacaoRepository _UsuarioAplicacaoRepository;
        private UserTokenAppTokenRepository _UserTokenAppTokenRepository;


        public UserTokenAppTokenRepository UserTokenAppTokenRepository { get { return _UserTokenAppTokenRepository ?? (_UserTokenAppTokenRepository = new UserTokenAppTokenRepository(_dbContext)); } }
        public UsuarioAplicacaoRepository UsuarioAplicacaoRepository { get { return _UsuarioAplicacaoRepository ?? (_UsuarioAplicacaoRepository = new UsuarioAplicacaoRepository(_dbContext)); } }
        public TokenRepository TokenRepository { get { return _TokenRepository ?? (_TokenRepository = new TokenRepository(_dbContext)); } }
        public FuncionarioRepository FuncionarioRepositoty { get { return _FuncionarioRepository ?? (_FuncionarioRepository = new FuncionarioRepository(_dbContext)); } }
        public UsuarioRepository UsuarioRepository { get { return _UsuarioRepository ?? (_UsuarioRepository = new UsuarioRepository(_dbContext)); } }
        public StatusUsuarioRepository StatusUsuarioRepository { get { return _StatusUsuarioRepository ?? (_StatusUsuarioRepository = new StatusUsuarioRepository(_dbContext)); } }


        public AcaoRepository AcaoRepository { get { return _AcaoRepository ?? (_AcaoRepository = new AcaoRepository(_dbContext)); } }
        public TransacaoRepository TransacaoRepository { get { return _TransacaoRepository ?? (_TransacaoRepository = new TransacaoRepository(_dbContext)); } }
        public AplicacaoRepository AplicacaoRepository { get { return _AplicacaoRepository ?? (_AplicacaoRepository = new AplicacaoRepository(_dbContext)); } }
        public PerfilAplicacaoRepository PerfilAplicacaoRepository { get { return _PerfilAplicacaoRepository ?? (_PerfilAplicacaoRepository = new PerfilAplicacaoRepository(_dbContext)); } }
        public TipoAplicacaoRepository TipoAplicacaoRepository { get { return _TipoAplicacaoRepository ?? (_TipoAplicacaoRepository = new TipoAplicacaoRepository(_dbContext)); } }
        public FuncionarioPerfilPermissaoRepository FuncionarioPerfilPermissaoRepository { get { return _FuncionarioPerfilPermissaoRepository ?? (_FuncionarioPerfilPermissaoRepository = new FuncionarioPerfilPermissaoRepository(_dbContext)); } }
        public FuncionarioPerfilRepository UsuarioPerfilRepository { get { return _UsuarioPerfilRepository ?? (_UsuarioPerfilRepository = new FuncionarioPerfilRepository(_dbContext)); } }

        public void Dispose()
        {
            _dbContext.Dispose();
            this.Dispose();
        }
    }
}
