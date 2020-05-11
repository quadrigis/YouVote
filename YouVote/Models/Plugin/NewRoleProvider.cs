using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using YouVote.Models.User;

namespace YouVote.Models.Plugin
{
    public class NewRoleProvider : RoleProvider
    {
        /// <summary>
        /// http://stackoverflow.com/questions/12259052/asp-net-universal-providers-roleprovider-does-not-cache-roles-in-cookie
        /// </summary>

        private static readonly object LockObject = new object();

        private int _cacheTimeoutInMinutes;

        public override void Initialize(string name, NameValueCollection config)
        {
            // Set Properties
            ApplicationName = config["applicationName"];
            _cacheTimeoutInMinutes = Convert.ToInt32(config["cacheTimeoutInMinutes"]);

            // Call base method
            base.Initialize(name, config);
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override sealed string ApplicationName { get; set; }

        public override string[] GetRolesForUser(string username)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return null;
            }
            
            var userPermissions = SysUserPermissionsCache.GetPermissions(username);
            if (userPermissions != null) return userPermissions;

            lock (LockObject)
            {
                userPermissions = UserDataObject.GetUserPermissions(username);
            }

            SysUserPermissionsCache.InsertToCache(username, userPermissions, _cacheTimeoutInMinutes);
            return userPermissions;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var roles = GetRolesForUser(username);
            return roles.Contains(roleName);

        }
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
            //var roleRepo = TypeResolver.Resolve<IRoleRepository>();
            //var roles = roleRepo.GetByName(roleName);
            //return roles.Any();
        }
    }
}