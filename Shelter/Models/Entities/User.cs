using Microsoft.AspNetCore.Identity;
using Shelter.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shelter.Models.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public UserTypeEnum MyUserType { get; set; } = UserTypeEnum.ADMIN;
    }
}
