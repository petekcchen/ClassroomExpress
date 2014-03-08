using System;
using System.Runtime.Serialization;

namespace ClassroomExpress.Domain
{
    [Serializable]
    public class NoOpenCoursePermissionException : Exception
    {
        public NoOpenCoursePermissionException()
        {
        }

        public NoOpenCoursePermissionException(string message)
            : base(message)
        {
        }

        public NoOpenCoursePermissionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected NoOpenCoursePermissionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
