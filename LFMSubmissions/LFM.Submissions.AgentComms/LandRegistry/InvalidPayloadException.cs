using System;
using System.Runtime.Serialization;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    [Serializable]
    public class InvalidPayloadException : Exception
    {
        public InvalidPayloadException()
            : base() { }

        public InvalidPayloadException(string message)
            : base(message) { }

        public InvalidPayloadException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public InvalidPayloadException(string message, Exception innerException)
            : base(message, innerException) { }

        public InvalidPayloadException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected InvalidPayloadException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
