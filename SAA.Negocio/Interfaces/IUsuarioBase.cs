using SAA.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Negocio.Interfaces
{
    public interface IUsuarioBase : IIdentity
    {
        string DisplayName { get; }
        bool Autentica();
    }
}
