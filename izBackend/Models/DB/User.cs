using izBackend.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace izBackend.Models.DB
{
    public class User : izBaseDbObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Username { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [Required]
        [StringLength(500)]
        public string Password { get; set; }

        [Required]
        [StringLength(500)]
        public string Salt { get; set; }
         public string JFields { get; set; }

        [Required]
        public Enums.UserSources Source { get; set; }  = Enums.UserSources.Local;

        public Group Group { get; set; }
    }
}
