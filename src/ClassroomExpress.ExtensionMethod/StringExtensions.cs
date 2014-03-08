using System;

namespace ClassroomExpress.ExtensionMethod
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static void ThrowIfNullOrEmpty(this string value, string parameterName)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}