using izBackend.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace izBackend.Models.DB
{
    public class Group : izBaseDbObject
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Name { get; set; }

         public string JFields { get; set; }
        public ICollection<User> Users { get; set; }


    }
}
