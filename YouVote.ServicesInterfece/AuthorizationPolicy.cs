using System;
using System.Collections.Generic;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;

namespace YouVote.ServicesInterfece
{
    public class AuthorizationPolicy : IAuthorizationPolicy
    {
        Guid _id = Guid.NewGuid();
        // this method gets called after the authentication stage
        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            // get the authenticated client identity
            var client = GetClientIdentity(evaluationContext);
            // set the custom principal
            evaluationContext.Properties["Principal"] = new CustomPrincipal(client);
            return true;
        }
        private IIdentity GetClientIdentity(EvaluationContext evaluationContext)
        {
            object obj;
            if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
                throw new Exception("No Identity found");
            var identities = obj as IList<IIdentity>;
            if (identities == null || identities.Count <= 0)
                throw new Exception("No Identity found");
            return identities[0];
        }


        public System.IdentityModel.Claims.ClaimSet Issuer
        {
            get { throw new NotImplementedException(); }
        }

        public string Id
        {
            get { return _id.ToString(); }
        }
    }

    public class CustomPrincipal : IPrincipal
    {
        private string[] _roles;

        public CustomPrincipal(IIdentity identity)
        {
            Identity = identity;
        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            if (Identity.Name == "test")
                _roles = new [] { "ADMIN" };
            else
                _roles = new [] { "USER" };
            return _roles.Contains(role);
        }
    }

    public class UserAuthentication : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            try
            {
                if (userName == "test" && password == "test123")
                {
                    Console.WriteLine("Authentic User");
                }
            }
            catch (Exception ex)
            {
                throw new FaultException("Unknown Username or Incorrect Password");
            }
        }
    }
}
