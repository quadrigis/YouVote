using System.ServiceModel;

namespace YouVote.ServicesInterfece
{
    [ServiceContract(SessionMode = SessionMode.Allowed, Namespace = Namespaces.Services.YouVote)]
    public interface IService
    {
        [OperationContract]
        BaseResponse SaveUser(BaseRequest request);
    }
}
