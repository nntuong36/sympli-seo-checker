using SympliSeoChecker.Common.Enums;

namespace SympliSeoChecker.Common.Constants
{
    public class ErrorCodeMessage
    {
        public static readonly Dictionary<ErrorCode, string> Messages =
            new Dictionary<ErrorCode, string>
            {
                // common error message
                { ErrorCode.Invalid, "The request value is invalid." },

                // search engine error message
                { ErrorCode.SearchEngineIsRequired, "Search engine is required." },
                { ErrorCode.SearchEngineTypeIsRequired, "Search engine item is required." },
                { ErrorCode.SearchEngineTypeIsInRange, "Search engine item must be in range of SearchEngineType." },

                // keyword error message
                { ErrorCode.KeywordIsRequired, "Keyword field is required." },
                { ErrorCode.KeywordIsNotEmpty, "Keyword field is not empty." },
                { ErrorCode.KeywordIsGreaterThan, $"Keyword field must have at least {Constants.StringMinLength} characters." },
                { ErrorCode.KeywordIsLessThan, $"Keyword field have maximum {Constants.StringMaxLength} characters." },

                // url error message
                { ErrorCode.UrlIsRequired, "Url field is required." },
                { ErrorCode.UrlIsNotEmpty, "Url field is not empty." },
                { ErrorCode.UrlIsInvalid, "Url field is invalid format." },
            };
    }
}
