namespace SympliSeoChecker.Common.Constants
{
    public class Constants
    {
        public static int TotalSearchResultItems = 100;
        public static int BingSearchMaxItemPerPage = 10;

        public const int StringMinLength = 3;
        public const int StringMaxLength = 100;

        public const int CacheExpirationInSecond = 900;

        public const string GoogleSearchUrl = "https://www.google.com.au";
        public const string BingSearchUrl = "https://www.bing.com";

        // Pattern
        public const string GoogleSearchHrefsPattern =
            $"<div[^>]*data-rpos[\\s\\S]*?>[\\s\\S]*?<span[^>]*jscontroller=[\\s\\S]*?<a[^>]*jsname=[^>]*href=\"([^\"]*)\"";
        public const string BingSearchHrefsPattern =
            $"<li[^>^nk][\\s\\S]*?data-id iid[\\s\\S]*?<a[^>]*href=\"([^\"]+)\"";
        public const string UrlPattern =
            $"^(https?://)?((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{{2,}})$";

        public const string UserAgent =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36";
    }
}
