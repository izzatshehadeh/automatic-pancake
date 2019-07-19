using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace izBackend.Models.Auth
{
    public class RefreshToken
    {
       

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        [Required]
        [StringLength(5000)]
        public string Token { get; set; }
        [Required]
        [StringLength(5000)]
        public string OriginalToken { get; set; }

        [Required]
        [StringLength(450)]
        public string UserID { get; set; }



    }


}
