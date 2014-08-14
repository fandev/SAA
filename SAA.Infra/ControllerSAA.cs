using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SAA.Infra
{
    public class ControllerSAA : Controller
    {
        public List<Alert> ListAlert
        {
            get
            {
                if (TempData["ListAlert"] != null)
                    if (TempData["ListAlert"] is List<Alert>)
                        return TempData["ListAlert"] as List<Alert>;
                return (TempData["ListAlert"] =  new List<Alert>()) as List<Alert>;
            }
        }


    }
}
