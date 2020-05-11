using System;
using System.Runtime.Serialization;

namespace YouVote.Common.Exceptions
{
    [Serializable]
    public class DbException : UiException
    {
        public DbException()
        { }

        public DbException(string message)
            : base(message)
        { }

        public DbException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected DbException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

    }
}
