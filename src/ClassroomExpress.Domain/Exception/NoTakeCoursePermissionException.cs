using System;
using System.Runtime.Serialization;

namespace ClassroomExpress.Domain
{
    [Serializable]
    public class NoTakeCoursePermissionException : Exception
    {
        public NoTakeCoursePermissionException()
        {
        }

        public NoTakeCoursePermissionException(string message)
            : base(message)
        {
        }

        public NoTakeCoursePermissionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected NoTakeCoursePermissionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
