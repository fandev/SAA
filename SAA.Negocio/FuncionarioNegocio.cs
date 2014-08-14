using SAA.Model.Models;
using SAA.Infra;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAA.Repository;

namespace SAA.Negocio
{
    public class FuncionarioNegocio
    {
        private static UnitOfWork unit;
        private TokenManager tokenManager;

        public FuncionarioNegocio(UnitOfWork _unit)
        {
            unit = _unit;
            tokenManager = new TokenManager(_unit);
        }
        public Funcionario GetFuncionarioAD(Usuario user)
        {
            return GetUserByLogin(user.Login);
        }

        private Funcionario GetFuncionario(Usuario user)
        {
            return null;
        }

        public void ValidFuncionario(string token_hash, string ip)
        {
            var token = tokenManager.GetToken(token_hash);
            IsValidFuncionario(token.IdUsuario);
        }

        public Funcionario GetFuncionario(int id)
        {
            Funcionario funcionario = unit.FuncionarioRepositoty.All().SingleOrDefault(x => x.Id == id);
            return funcionario;
        }

        private void IsValidFuncionario(int id)
        {
            Funcionario funcionario = GetFuncionario(id);
            if (funcionario != null)
            {
                StatusFuncionario status = funcionario.StatusFuncionario;
                if (!status.Codigo.Equals("A"))
                    throw new Exception("Usuário inativo");
            }
            else
                throw new Exception("Usuário inválido");
        }

        private Funcionario GetUserByLogin(String userName)
        {
            try
            {
                ADServ ad = new ADServ();
                //_directoryEntry = null;
                DirectorySearcher directorySearch = new DirectorySearcher(new ADServ().SearchRoot);
                directorySearch.Filter = "(&(objectClass=user)(sAMAccountName=" + userName + "))";
                SearchResult results = directorySearch.FindOne();

                if (results != null)
                {
                     DirectoryEntry user = new DirectoryEntry(results.Path);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }
    }
}
