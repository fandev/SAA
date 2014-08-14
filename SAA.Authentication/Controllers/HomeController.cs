using SAA.Authentication.Security;
using SAA.Infra;
using System.Web.Mvc;

namespace SAA.Authentication.Controllers
{
    
    [RequireHttps]
    [TokenValidateActionFilter]    
    public class HomeController : ControllerSAA
    {
        [Authorize]
        public ActionResult Index()
        {

            ViewBag.Title = "Home Page";
            var user = User.Identity;
            ListAlert.Add(new Alert("Entrei na Home/Index", AlertType.Info));
            return View();
        }

        public ActionResult UsuarioNaoExiste()
        {
            var request = Request;
            return View();
        }

    }
}
