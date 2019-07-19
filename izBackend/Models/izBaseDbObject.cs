using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace izBackend.Models.DB
{
    public class izBaseDbObject
    {
       public DateTime CreationTime { get; set; }


        public DateTime? ModifiedDate { get; set; }
    }
}
