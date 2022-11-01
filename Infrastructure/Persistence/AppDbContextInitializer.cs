
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence
{
    public class AppDbContextInitializer
    {
        private readonly ILogger<AppDbContextInitializer> _logger;
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppDbContextInitializer(ILogger<AppDbContextInitializer> logger, AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {

            // Default user
          
            ApplicationUser administrator = new ApplicationUser
            {
                Email = "admin@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "admin@gmail.com",
                FullName = "admin admin",
                GroupId = "adminGroup"
            };

            //adding default user
            var defaultPassword = "pass123";
            if (_userManager.Users.All(u => u.Email != administrator.Email))
            {
                var result = await _userManager.CreateAsync(administrator, defaultPassword);
                if (!result.Succeeded)
                {
                    _logger.LogError($"could not create user : {administrator} ");
                }
                await _context.SaveChangesAsync();

               //adding default user roles
                UserRole[] defaultRoles = {
                new UserRole {
                    title ="dashboard",
                    page = "dashboard",
                    ApplicationUserId =administrator.Id
                } ,
                new UserRole
                {
                    title = "bill of loading",
                    page = "bill_of_loading",
                    ApplicationUserId =administrator.Id
                } ,
                new UserRole
                {
                    title = "operation",
                    page = "operation",
                    ApplicationUserId =administrator.Id
                } ,
                new UserRole
                {
                    title = "shipping agent payment",
                    page = "shipping_agent_payment",
                    ApplicationUserId =administrator.Id
                } ,
                new UserRole
                {
                    title = "terminal port handling",
                    page = "terminal_port_handling",
                    ApplicationUserId =administrator.Id
                } ,
                new UserRole
                {
                    title = "container",
                    page = "container",
                    ApplicationUserId =administrator.Id
                },
                new UserRole
                {
                    title = "shipping agent",
                    page = "shipping_agent",
                    ApplicationUserId =administrator.Id
                } ,
                new UserRole
                {
                    title = "driver",
                    page = "driver",
                    ApplicationUserId =administrator.Id
                } ,
                new UserRole
                {
                    title = "truck",
                    page = "truck",
                    ApplicationUserId =administrator.Id
                } ,
                new UserRole
                {
                    title = "port",
                    page = "port",
                    ApplicationUserId =administrator.Id
                } ,
                new UserRole
                {
                    title = "company",
                    page = "company",
                    ApplicationUserId =administrator.Id
                } ,
                new UserRole
                {
                    title = "operation followup",
                    page = "operation_followup",
                    ApplicationUserId =administrator.Id
                } ,
                new UserRole
                {
                    title = "assign goods to container",
                    page = "assign_goods_to_container",
                    ApplicationUserId =administrator.Id
                } ,
                new UserRole
                {
                    title = "documentation",
                    page = "documentation",
                    ApplicationUserId =administrator.Id
                } ,
                new UserRole
                {
                    title = "users",
                    page = "users",
                    ApplicationUserId =administrator.Id
                } ,
                new UserRole
                {
                    title = "lookup",
                    page = "lookup",
                    ApplicationUserId =administrator.Id
                } ,
                new UserRole
                {
                    title = "settings",
                    page = "settings",
                    ApplicationUserId =administrator.Id
                } ,
            };
                try
                {
                    await _context.AddRangeAsync(defaultRoles);
                    _logger.LogInformation("successfully added roles for the default user");

                }catch(Exception e)
                {
                    _logger.LogError("could not add roles ");
                }
 
            }
            await _context.SaveChangesAsync();
        }
    }
}