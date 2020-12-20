using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shelter.Models;
using Shelter.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shelter.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Dog> Dogs { get; set; }
        public DbSet<User> MyUsers { get; set; }
        public DbSet<Requested> Requesteds { get; set; }
    }
}
