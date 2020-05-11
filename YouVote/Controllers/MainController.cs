using System.Globalization;
using System.Web.Mvc;
using YouVote.Models.Plugin;

namespace YouVote.Controllers
{
    public class MainController : SecureController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        [Authorize(Roles = "Full")] 
        public ActionResult About()
        {
            return View();
        }
    }
}
