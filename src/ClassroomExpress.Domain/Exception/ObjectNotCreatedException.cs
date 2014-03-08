using System;
using System.Runtime.Serialization;

namespace ClassroomExpress.Domain
{
    [Serializable]
    public class ObjectNotCreatedException : Exception
    {
        public ObjectNotCreatedException()
        {
        }

        public ObjectNotCreatedException(string message)
            : base(message)
        {
        }

        public ObjectNotCreatedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ObjectNotCreatedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}