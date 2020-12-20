using Shelter.Models.Base;
using Shelter.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shelter.Models
{
    public class Dog : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public GenderEnum Gender { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public bool IsSterilized { get; set; }
        [Required]
        [StringLength(100)]
        public string ShelterName { get; set; }
        public bool IsOwned { get; set; } = false;
        public User User { get; set; }
    }
}
