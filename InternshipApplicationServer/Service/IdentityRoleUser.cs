using DataAcess.Data;
using InternshipApplicationServer.ServiceInterface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipApplicationServer.Service
{
    public class IdentityRoleUser : IdentityRoleUserInterface
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityRoleUser(AppDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void AuthenticationRole()
        {
            if(_dbContext.Database.GetPendingMigrations().Count() > 0)
            {
                _dbContext.Database.Migrate();
            }

            if (_dbContext.Roles.Any(x => x.Name == Utils.AuthenticationData.AdminRole)) return;

            _roleManager.CreateAsync(new IdentityRole(Utils.AuthenticationData.AdminRole)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Utils.AuthenticationData.CompanyRole)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Utils.AuthenticationData.KeeperRole)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Utils.AuthenticationData.StudentRole)).GetAwaiter().GetResult();


            _userManager.CreateAsync(new User
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            }, "zaq1@WSX").GetAwaiter().GetResult();

            User identityUser = _dbContext.User.FirstOrDefault(u => u.Email == "admin@gmail.com");
            _userManager.AddToRoleAsync(identityUser, Utils.AuthenticationData.AdminRole).GetAwaiter().GetResult();
        }
    }
}
