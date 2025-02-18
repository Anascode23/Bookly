using Bookly.DataAccess.Data;
using Bookly.Models.Models;
using Bookly.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookly.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly BooklyDbContext _booklyDb;

        public DbInitializer(
             UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, BooklyDbContext booklyDb)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _booklyDb = booklyDb;
        }
        public void Initialize()
        {
            try
            {
                if (_booklyDb.Database.GetPendingMigrations().Count() > 0)
                {
                    _booklyDb.Database.Migrate();
                }
            }
            catch (Exception ex) { }

            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@bookly.com",
                    Email = "admin@bookly.com",
                    Name = "Anas Ali",
                    PhoneNumber = "1234567890",
                    StreetAddress = "Test 123 Ave",
                    State = "IL",
                    PostalCode = "12345",
                    City = "Chicago"
                }, "Admin123!").GetAwaiter().GetResult();


                ApplicationUser user = _booklyDb.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@bookly.com");

                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }
            return;

        }

    }
}
