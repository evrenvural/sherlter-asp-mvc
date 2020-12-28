using Shelter.Models.Base;
using Shelter.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shelter.Models.Entities
{
    public class Requested : BaseEntity
    {
        [Required]
        public Dog Dog { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public RequestStatusEnum Status { get; set; } = RequestStatusEnum.IS_WAITING;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
