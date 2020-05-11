using System.Web.Mvc;

namespace YouVote.Models.Plugin
{
    [AuthorizeWithAjax]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class SecureController : Controller
    {
    }
}