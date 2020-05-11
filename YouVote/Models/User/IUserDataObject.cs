using YouVote.ServicesInterfece;

namespace YouVote.Models.User
{
    public interface IUserDataObject
    {
        BaseResponse Save(UserModel dto);
    }
}
