using System.Runtime.Serialization;

namespace YouVote.ServicesInterfece
{
    [DataContract]
    public class BaseResponse
    {
        public BaseResponse(bool success, string messageText  = "")
        {
            Success = success;
            MessageText = messageText;
        }

        [DataMember]
        public bool Success { get; set; }
        [DataMember]
        public string MessageText { get; set; }
    }
}
