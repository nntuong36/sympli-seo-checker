using Newtonsoft.Json;
using SympliSeoChecker.Common.Enums;

namespace SympliSeoChecker.Domain.Models.Validators
{
    public class ValidateErrorModel
    {
        [JsonProperty("error_code", Order = 0)]
        public ErrorCode ErrorCode { get; set; }
        [JsonProperty("error_message", Order = 1)]
        public string ErrorMessage { get; set; }
        [JsonProperty("property_name", Order = 2)]
        public string PropertyName { get; set; }
    }
}
