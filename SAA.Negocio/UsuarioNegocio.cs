using SAA.Infra;
using SAA.Model.Models;
using System;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using SAA.Negocio.Modelos;
using SAA.Repository;

namespace SAA.Negocio
{
    public class UsuarioNegocio
    {
        private UnitOfWork unit;
        private TokenManager tokenManager;

        public UsuarioNegocio(UnitOfWork _unit)
        {
            this.unit = _unit;
            tokenManager = new TokenManager(_unit);
        }

        /// <summary>
        /// Autentica Usuário na base de dados local
        /// Gera token
        /// </summary>
        /// <param name="username">login</param>
        /// <param name="password">senha</param>
        /// <param name="request">System.WebHttpResponseBase request</param>
        /// <returns></returns>
        public Status AutenticaUsuario(string username, string password, System.Web.HttpRequestBase request)
        {
            // 1. Verifica se o usuário existe na base de dados
            Usuario user = unit.UsuarioRepository.All().SingleOrDefault(x => x.Login == username);
            if (user != null)
            {
                var passwordMD5 = password.ComputeHash(Infra.HashHelper.eHashType.MD5);
                bool autenticado = unit.UsuarioRepository.All()
                    .Any(x =>
                        x.Login.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
                        x.Senha.Equals(passwordMD5));

                if (autenticado)
                    return new Status { Succeeded = true, Message = "Autenticado com sucesso.", Token = tokenManager.GerarToken(user.Id, request).Hash };
                else return new Status { Succeeded = false, Message = "Usuário ou senha incorreta.", Token = null };
            }

            return new Status { Succeeded = false, Message = "Usuario não encontrado na base de dados.", Token = null };

        }

        /// <summary>
        /// Autentica Usuário em um domínio
        /// Gera token
        /// </summary>
        /// <param name="username">login</param>
        /// <param name="password">senha</param>
        /// <param name="request">System.WebHttpResponseBase request</param>
        /// <param name="dominioAD">nome do Domínio do Windows</param>
        /// <returns></returns>
        public Status AutenticaUsuario(string username, string password, System.Web.HttpRequestBase request, string dominioAD)
        {
            Usuario user = unit.UsuarioRepository.AllInclude(x=> x.StatusUsuario).SingleOrDefault(x => x.Login.Equals(dominioAD + "\\" + username, StringComparison.InvariantCultureIgnoreCase));
            bool _isAutenthicated = false;
            if (user != null)
            {

                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, dominioAD))
                {
                    _isAutenthicated = pc.ValidateCredentials(username, password);
                }

                if (_isAutenthicated && !user.StatusUsuario.Codigo.Equals("A", StringComparison.InvariantCultureIgnoreCase))
                    return new Status { Succeeded = false, Message = "Usuário invativo, contate o administrador do sistema", Token = null };

                if (_isAutenthicated)
                    return new Status { Succeeded = true, Message = "Autenticado com sucesso.", Token = tokenManager.GerarToken(user.Id, request).Hash };
                else return new Status { Succeeded = false, Message = "Usuário ou senha incorreta. Não foi possível autenticar no domínio", Token = null };
            }
            return new Status { Succeeded = false, Message = "Usuario não encontrado na base de dados.", Token = null };

        }
    }
}
