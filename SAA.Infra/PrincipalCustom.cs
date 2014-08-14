using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Infra
{
    public class PrincipalCustom : IPrincipal
    {
        private IdentityCustom _identity;
        public IIdentity Identity
        {
            get { return _identity ?? new IdentityCustom(); }
        }

        public PrincipalCustom()
        {
        }
        public PrincipalCustom(IdentityCustom identity)
        {
            this._identity = identity;
        }
        public void SetIdentity(string userName, string authenticationType, bool isAuthenticated)
        {
            var identity = new IdentityCustom();
            identity.SetAuthenticationType(authenticationType);
            identity.SetIsAuthenticated(isAuthenticated);
            identity.SetName(userName);
            this._identity = identity;
        }

        public void SetIdentity(IdentityCustom identity)
        {
            this._identity = identity;
        }

        public bool IsInRole(string role)
        {
            return false;
        }
    }
}
