using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ApplicationName.Common.Enumerations.YahooFinance
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Quote
    {
        NasdaqComposite,
        DaxPerformanceIndex
    }
}
