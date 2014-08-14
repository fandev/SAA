using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Infra
{
    public static class ADProp
    {
        public const String OBJECTCLASS = "objectClass";
        public const String CONTAINERNAME = "cn";
        public const String PRIMEIRONOME = "givenName";
        public const String NOMEDOMEIO = "initials";
        public const String ULTIMONOME = "sn";
        public const String NOMECOMPLETO = "name";
        public const String LOGIN = "sAMAccountName";

        public const String CEP = "postalCode";
        public const String ENDERECO = "streetAddress";
        public const String CIDADE = "l";
        public const String ESTADO = "st";
        public const String PAIS = "co";
        public const String NOTACAOPAIS = "c";


        public const String EMAIL = "mail";
        public const String CELULAR = "mobile";
        public const String TELEFONE = "homePhone";
        public const String TELEFONECOMERCIAL = "telephoneNumber";

        public const String TITULO = "title";

        public const String DATADACRIACAO = "whenCreated";
        public const String DATAULTIMAMODIFICACAO = "whenChanged";


        public const String DEPARTAMENTO = "department";
        public const String COMPANIA = "company";
        public const String MATRICULA = "physicalDeliveryOfficeName";
    }
}
