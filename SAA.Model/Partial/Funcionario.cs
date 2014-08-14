using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAA.Infra;

namespace SAA.Model.Models
{
    public partial class Funcionario
    {
        private Funcionario(DirectoryEntry directoryUser)
        {
            Nome = GetProperty(directoryUser, ADProp.NOMECOMPLETO);
            ADUser = GetProperty(directoryUser, ADProp.LOGIN);
            Matricula = Convert.ToInt32(GetProperty(directoryUser, ADProp.MATRICULA));
        }

       private static String GetProperty(DirectoryEntry userDetail, String propertyName)
        {
            if (userDetail.Properties.Contains(propertyName))
            {
                return userDetail.Properties[propertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static Funcionario GetFuncionario(DirectoryEntry directoryUser)
        {
            return new Funcionario(directoryUser);
        }
    }
}
