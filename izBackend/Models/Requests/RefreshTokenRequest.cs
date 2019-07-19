using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace izBackend.Models.Requests
{
    public class RefreshTokenRequest
    {
        [Required]
        [JsonProperty("userid")]
        public string UserID { get; set; }


        [Required]
        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
