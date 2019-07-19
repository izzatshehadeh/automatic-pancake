using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace izBackend.Models.Auth
{
    public class AuthResponse
    {
        private object expiery;

        public AuthResponse()
        {
        }

        public AuthResponse(string token, string refreshToken, object expiery)
        {
            OriginalToken = token;
            RefreshToken = refreshToken;
            this.expiery = expiery;
        }

        public AuthResponse(string token, string refreshToken, object expiery, string userId) : this(token, refreshToken, expiery)
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
