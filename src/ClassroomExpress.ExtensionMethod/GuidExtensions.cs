using System;

namespace ClassroomExpress.ExtensionMethod
{
    public static class GuidExtensions
    {
        public static bool IsEmpty(this Guid value)
        {
            return value == Guid.Empty;
        }

        public static bool IsNotEmpty(this Guid value)
        {
            return value != Guid.Empty;
        }

        public static void ThrowIfEmpty(this Guid value, string parameterName)
        {
            if (value.IsEmpty())
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}