using System;

namespace ClassroomExpress.ExtensionMethod
{
    public static class ObjectExtensions
    {
        public static bool IsNull<T>(this T value) where T : class
        {
            return value == null;
        }

        public static void ThrowIfNull<T>(this T value, string parameterName) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}