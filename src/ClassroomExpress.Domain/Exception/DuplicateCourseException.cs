using System;
using System.Runtime.Serialization;

namespace ClassroomExpress.Domain
{
    [Serializable]
    public class DuplicateCourseException : Exception
    {
        public DuplicateCourseException()
        {
        }

        public DuplicateCourseException(string message)
            : base(message)
        {
        }

        public DuplicateCourseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected DuplicateCourseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
