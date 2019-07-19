using izBackend.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace izBackend.Models.DB
{
    public class RefreshToken : izBaseDbObject
    {
       

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        [Required]
        [StringLength(5000)]
        public string Token { get; set; }
        [Required]
        [StringLength(5000)]
        public string OriginalToken { get; set; }


        public User User { get; set; }
    }


}
