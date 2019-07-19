using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace izBackend.Models.Common.Auth
{
    public class AuthResponse
    {
      
        public AuthResponse()
        {
        }

        public AuthResponse(string token, string refreshToken, DateTime expiery)
        {
            OriginalToken = token;
            RefreshToken = refreshToken;
            this.Expiery = expiery;
        }

        public AuthResponse(string token, string refreshToken, DateTime expiery, string userId) : this(token, refreshToken, expiery)
        {
            UserID = userId;
        }

        [JsonProperty("Original")]
        public string OriginalToken { get; set; }

        [JsonProperty("Refresh")]
        public string RefreshToken { get; set; }

        [JsonProperty("Expiery")]
        public DateTime Expiery { get; set; }

     
        public string UserID { get; set; }
    }
}
