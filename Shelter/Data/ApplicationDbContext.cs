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
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ShelterDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            base.OnConfiguring(builder);
        }

        public DbSet<Dog> Dogs { get; set; }
        public DbSet<User> MyUsers { get; set; }
        public DbSet<Requested> Requesteds { get; set; }
    }
}
