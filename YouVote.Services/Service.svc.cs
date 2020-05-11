using System;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using YouVote.ServicesInterfece;

namespace YouVote.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class Service : IService
    {
        [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
        public BaseResponse SaveUser(BaseRequest request)
        {
            return new BaseResponse(true);
        }

        private static string GetCurrentUser()
        {
            try
            {
                return HttpContext.Current.User.Identity.Name.Trim();
            }
            catch (NullReferenceException)
            {
                return "N/A";
            }
        }
    }
}
