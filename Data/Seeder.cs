using API_WEB_Ejercicio3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API_WEB_Ejercicio3.Data
{
    public static class Seeder
    {
        public static PasswordHasher<IdentityUser> PasswordHasher = new PasswordHasher<IdentityUser>();
        private static List<Genre> genres = new List<Genre>
        {
            new Genre
            {
                Id = 1,
                Title = "MOBA"
            },
            new Genre
            {
                Id = 2,
                Title = "MMORPG",
            },
            new Genre
            {
                Id = 3,
                Title = "RPG",
            }
        };
        private static List<Game> games = new List<Game>
        {
            new Game
            {
                Id = 1,
                Title = "League of Legends",
                Description = "ASJPASJ0",
                GenreId = genres[0].Id
            },
            new Game
            {
                Id = 2,
                Title = "World of Warcraft",
                Description = "ASJPASJ0",
                GenreId = genres[1].Id
            },
            new Game
            {
                Id = 3,
                Title = "Super Mario RPG, The Legend of the Seven Stars",
                Description = "ASJPASJ0",
                GenreId = genres[2].Id
            }
        };
        public static void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(genres);
            modelBuilder.Entity<Game>().HasData(games);
        }

        public static void SeedUsersRoles(ModelBuilder modelBuilder)
        {
            //Code
            List<IdentityRole> roles = new List<IdentityRole>();
            roles.Add(new IdentityRole { Name = "Basic", NormalizedName = "BASIC" });
            roles.Add(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });

            modelBuilder.Entity<IdentityRole>().HasData(
                    roles[0],
                    roles[1]
                );
            //Creating Users
            List<IdentityUser> users = new List<IdentityUser>();

            users.Add(new IdentityUser
            {
                UserName = "basic@ejercicio4.com",
                NormalizedUserName = "BASIC@EJERCICIO4.COM",
                Email = "basic@ejercicio4.com",
                NormalizedEmail = "BASIC@EJERCICIO4.COM",
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            }
            );

            users.Add(new IdentityUser
            {
                UserName = "admin@ejercicio4.com",
                NormalizedUserName = "ADMIN@EJERCICIO4.COM",
                Email = "admin@ejercicio4.com",
                NormalizedEmail = "ADMIN@EJERCICIO4.COM",
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            }
            );

            users[0].PasswordHash = PasswordHasher.HashPassword(users[0], "Basic123!");
            users[1].PasswordHash = PasswordHasher.HashPassword(users[1], "Admin123!");

            modelBuilder.Entity<IdentityUser>().HasData(
                users[0],
                users[1]
            );
            //Assigning User Roles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = users[0].Id,
                    RoleId = roles[0].Id
                },
                new IdentityUserRole<string>
                {
                    UserId = users[1].Id,
                    RoleId = roles[1].Id
                }
            );
        }
        
    }
}
