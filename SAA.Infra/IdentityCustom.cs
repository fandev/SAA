using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Infra
{
    public class IdentityCustom : IIdentity
    {
        private string _authenticationType = "";
        private bool _isAuthenticated = false;
        private string _name = "";
        public string AuthenticationType
        {
            get { return _authenticationType; }
        }

        public void SetAuthenticationType(string authenticationType)
        {
            this._authenticationType = authenticationType;
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
        }

        public void SetIsAuthenticated(bool autenticado)
        {
            this._isAuthenticated = autenticado;
        }

        public string Name
        {
            get { return _name; }
        }

        public string SetName(string name)
        {
            return this._name = name;
        }
    }
}
