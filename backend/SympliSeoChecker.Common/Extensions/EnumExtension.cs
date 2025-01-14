using SympliSeoChecker.Common.Enums;
using System.ComponentModel;

namespace SympliSeoChecker.Common.Extensions
{
    public static class EnumExtension
    {
        public static string ToNumberString(this ErrorCode code) => ((long)code).ToString();

        public static string GetEnumDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }

            return string.Empty;
        }
    }
}
