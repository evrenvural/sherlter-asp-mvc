using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shelter.Models.Base
{
    public abstract class BaseEntity
    {
        [Key]
        [StringLength(450)]
        public string Id { get; set; }
    }
}
