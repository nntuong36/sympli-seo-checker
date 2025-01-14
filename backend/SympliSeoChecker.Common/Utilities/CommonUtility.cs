using SympliSeoChecker.Common.Constants;
using SympliSeoChecker.Common.Enums;
using System.Text.RegularExpressions;

namespace SympliSeoChecker.Common.Utilities
{
    public static class CommonUtility
    {
        public static bool IsValidUrl(string? url)
        {
            if (url is null) return false;

            Regex rgx = new Regex(Constants.Constants.UrlPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return rgx.IsMatch(url);
        }

        public static bool IsValidEnum<T>(int? value) => value is not null ? Enum.IsDefined(typeof(T), value) : false;

        public static string GetErrorMessage(ErrorCode code)
        {
            string? errorMessage;
            ErrorCodeMessage.Messages.TryGetValue(code, out errorMessage);
            return errorMessage ?? code.ToString();
        }

        public static int CalcPagesCount(int totalItem, int pageSize) => (int)Math.Ceiling((double)totalItem / pageSize);
    }
}
