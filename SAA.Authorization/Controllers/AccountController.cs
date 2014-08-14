using System;
using System.Linq;
using System.Web.Http;
using SAA.Infra;
using SAA.Model.ViewModel;
using SAA.Repository;
using System.Threading.Tasks;
using SAA.Negocio;

namespace SAA.Authorization.Controllers
{
    public class AccountController : APIControllerBase, IAPIController<UserInfo>
    {

        private UnitOfWork unit = new UnitOfWork();
        private TokenManager tokenManager;

        public AccountController()
        {
            tokenManager = new TokenManager(unit);
        }
        /// <summary>
        /// Retorna Informações de um usuário
        /// </summary>
        /// <param name="id">userToken</param>
        /// <returns></returns>
        [AllowAnonymous]
        public Task<UserInfo> Get(string id)
        {
            var user = unit.UsuarioRepository.AllInclude(x => x.Funcionario, y => y.StatusUsuario).SingleOrDefault(x => x.Id == this.UserId);
            var userInfo = new UserInfo
            {
                Name = user.Funcionario.Nome,
                Registration = user.Funcionario.Matricula,
                login = user.Login,
                Active = user.StatusUsuario.Codigo.Equals("A", StringComparison.InvariantCultureIgnoreCase) ? true : false,
                IsDomainLogin = user.Login.Contains('/') ? true : false
            };

            return Task<UserInfo>.Factory.StartNew(() => userInfo);
        }


        public IQueryable<UserInfo> Get()
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> Post(UserInfo item)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> Put(UserInfo item)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> Delete(object id)
        {
            throw new NotImplementedException();
        }
    }
}
