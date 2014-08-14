using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SAA.Infra
{
    public abstract class APIControllerBase : ApiController
    {
        private int _userId;
        private int _funcionarioId;
        private string _appGUID;

        public int UserId
        {
            get
            {
                var converteu = int.TryParse(Request.Headers.GetValues("SAA-userId").SingleOrDefault<string>(), out _userId);
                if (!converteu) throw new System.InvalidCastException(string.Format("Não foi possível converter o userId de string para inteiro : {0}", Request.Headers.GetValues("SAA-userId").SingleOrDefault()));
                return _userId;
            }
            set
            {
                int.TryParse(Request.Headers.GetValues("SAA-userId").SingleOrDefault<string>(), out _userId);
            }
        }

        public int FuncionarioId
        {
            get
            {
                var converteu = int.TryParse(Request.Headers.GetValues("SAA-funcionarioId").SingleOrDefault<string>(), out _funcionarioId);
                if (!converteu) throw new System.InvalidCastException(string.Format("Não foi possível converter o funcionarioId de string para inteiro : {0}", Request.Headers.GetValues("SAA-funcionarioId").SingleOrDefault()));
                return _funcionarioId;
            }
            set
            {
                int.TryParse(Request.Headers.GetValues("SAA-userId").SingleOrDefault<string>(), out _userId);
            }
        }

        public string AppGUID
        {
            get
            {
                return Request.Headers.GetValues("SAA-AppGUID").SingleOrDefault<string>();
            }
            private set
            {
                
            }
        }

    }
}
