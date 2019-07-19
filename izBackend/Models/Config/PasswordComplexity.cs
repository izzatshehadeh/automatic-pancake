using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace izBackend.Models
{
    [JsonObject("passwordComplexity")]
    public class PasswordComplexity
    {
        [JsonProperty("hasNumber")]
        public bool HasNumber { get; set; }

        [JsonProperty("hasUpperChar")]
        public bool HasUpperChar { get; set; }

        [JsonProperty("hasLowerChar")]
        public bool HasLowerChar { get; set; }

        [JsonProperty("hasSymbols")]
        public bool HasSymbols { get; set; }

        [JsonProperty("minCharacters")]
        public int MinCharacters { get; set; }

        [JsonProperty("maxCharacters")]
        public int MaxCharacters { get; set; }
    }
}
