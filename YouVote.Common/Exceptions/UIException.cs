using System;
using System.Runtime.Serialization;

namespace YouVote.Common.Exceptions
{
    [Serializable]
    public class UiException : Exception
    {
        public UiException()
        { }

        public UiException(string message)
            : base(message)
        { }

        public UiException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected UiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

    }
}
