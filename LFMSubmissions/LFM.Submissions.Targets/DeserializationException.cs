using System;
using System.Runtime.Serialization;

namespace LFM.Submissions.AgentServices
{
    [Serializable]
    public class DeserializationException : Exception
    {
        public DeserializationException()
            : base() { }

        public DeserializationException(string message)
            : base(message) { }

        public DeserializationException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public DeserializationException(string message, Exception innerException)
            : base(message, innerException) { }

        public DeserializationException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected DeserializationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}