using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using YouVote.Models.Plugin;
using YouVote.Models.User;
using YouVote.Resources.Views;

namespace YouVote.Controllers
{
    //[RequiresSsl(RequireSecure = true)]
    public class UserController : Controller
    {
        private readonly IUserDataObject _userDataObject;

        public UserController(IUserDataObject userDataObject)
        {
            _userDataObject = userDataObject;
        }

        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
        }

        public ActionResult Login()
        {
            var model = new UserModel { Login = new UserLiteModel()};
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(UserLiteModel model, string returnUrl)
        {
            var res = Url.Action("Login");
            var suc = false;
            var message = LoginRes.WrongPass;
            if (Membership.ValidateUser(model.Email, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                suc = true;
                message = MessageMenager.Login();
                res = GetReturnUrl(returnUrl);
            }
            return JsonNetResult.Convert(new DefaultJsonObject{ReturnUrl = res, Success = suc, Message = message, Type = suc ? JsAlertType.Success : JsAlertType.Error});
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            SysUserPermissionsCache.ClearCache();
            return RedirectToAction("Index", "Main");
        }

        [HttpPost]
        public ActionResult Register(UserModel model, string returnUrl)
        {
            var res = Url.Action("Login");
            var saveRes = _userDataObject.Save(model);

            if (saveRes.Success)
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);
                res = GetReturnUrl(returnUrl);
            }

            return JsonNetResult.Convert(new DefaultJsonObject { ReturnUrl = res, Success = saveRes.Success, Message = saveRes.MessageText, Type = saveRes.Success ? JsAlertType.Success : JsAlertType.Error });
        }

        private string GetReturnUrl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
            {
                return returnUrl;
            }
            return Url.Action("Index", "Main");
        }
    }
}
