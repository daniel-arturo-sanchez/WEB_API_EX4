using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API_WEB_Ejercicio3.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace API_WEB_Ejercicio3.Data
{
    public class WebAPIContext : IdentityDbContext<IdentityUser>
    {
        public WebAPIContext (DbContextOptions<WebAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Game { get; set; } = default!;
        public DbSet<Genre> Genre { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Seeder.SeedDatabase(modelBuilder);
            Seeder.SeedUsersRoles(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
