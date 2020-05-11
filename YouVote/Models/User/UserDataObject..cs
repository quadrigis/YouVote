using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;
using YouVote.ServicesInterfece;

namespace YouVote.Models.User
{
    public class UserDataObject : IUserDataObject
    {
        public BaseResponse Save(UserModel dto)
        {
            var binding = new WSHttpBinding();
            binding.Security.Mode = SecurityMode.Message;
            binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
            var address = new EndpointAddress(new Uri(ConfigurationManager.AppSettings["endpointAdress"]), EndpointIdentity.CreateDnsIdentity("youvote"));


            var client = new ChannelFactory<IService>(binding, address);
            client.Credentials.UserName.UserName = "test";
            client.Credentials.UserName.Password = "test123";

            var channel = client.CreateChannel();
            return channel.SaveUser(new BaseRequest());


            //using (var factory = new ChannelFactory<IService>(""))
            //{
            //    var client = factory.CreateChannel();
            //    return client.SaveUser(new BaseRequest());

            //}

        }

        public static string[] GetUserPermissions(string login)
        {
            return new []{"Admin"};
        }

        public static bool IsValid(string login, string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            return true;

            //var repo = TypeResolver.Resolve<IUserRepository>();
            //var user = repo.GetByEmail(username);
            //if (user == null) return false;
            //var res = Hash.VerifyHash(password, user.Password);
            //TypeResolver.Release(repo);
        }
    }
}
