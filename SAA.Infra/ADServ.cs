using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.DirectoryServices;

namespace SAA.Infra
{
   public class ADServ
    {
        public String LDAPPath
        {
            get
            {
                return ConfigurationManager.AppSettings["LDAPPath"];
            }
        }
        public String LDAPUser
        {
            get
            {
                return ConfigurationManager.AppSettings["LDAPUser"];
            }
        }
        public String LDAPPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["LDAPPassword"];
            }
        }
        private DirectoryEntry _directoryEntry;

        public  DirectoryEntry SearchRoot
        {
            get
            {
                if (_directoryEntry == null)
                {
                    string path = LDAPPath;
                    string user = LDAPUser;
                    string pass = LDAPPassword;

                    _directoryEntry = new DirectoryEntry(LDAPPath, LDAPUser, LDAPPassword, AuthenticationTypes.Secure);
                }
                return _directoryEntry;
            }
        }
    }
}
