using System.Drawing;
using Domain.Common.PaymentTypes;
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
                // if (_context.Database.IsSqlServer())
                // {
                    
                // }

                await _context.Database.MigrateAsync();
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
                // await TrySeedLookup();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            // Adding Default userGroup
            int groupId  =0 ;
            UserGroup defaultGroup = new UserGroup
            {
                Name = "AdminGroup",
                Responsiblity = "adminstration",

            };
            var found_group = _context.UserGroups.Where(ug=>ug.Name == defaultGroup.Name).FirstOrDefault();
            if (found_group == null) {
        
                try
                {
                    await _context.UserGroups.AddAsync(defaultGroup);
                    await _context.SaveChangesAsync();
                    groupId = defaultGroup.Id;
                }
                catch (Exception e)
                {
                    _logger.LogError($"error creating default userGroup: {e}");
                }

            }else{
                groupId = found_group.Id;
            }



            Address defaultAddress = new Address
            {
                Email = "admin@gmail.com",
                Phone = "0987654321",
                Region = "Addis Ababa",
                City = "Addis Ababa",
                Subcity = "Bole",
                Country = "Ethiopia",
                POBOX = ""
            };

            try {
                await _context.Addresses.AddAsync(defaultAddress);
                await _context.SaveChangesAsync();
            } catch (Exception e) {
                _logger.LogError($"error creating default address: {e}");
            }


            // Default user

            ApplicationUser administrator = new ApplicationUser
            {
                Email = "admin@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "admin@gmail.com",
                FullName = "admin admin",
                UserGroupId = (found_group == null) ? defaultGroup.Id : found_group.Id,
                AddressId = defaultAddress.Id
            };

            //adding default user
            var defaultPassword = "pass123#A";
            if (_userManager.Users.All(u => u.Email != administrator.Email))
            {
                var result = await _userManager.CreateAsync(administrator, defaultPassword);
                if (!result.Succeeded)
                {
                    _logger.LogError($"could not create user : {administrator} ");
                }
                await _context.SaveChangesAsync();

                //adding default user roles
               List<AppUserRole> defaultRoles = AppUserRole.createDefaultRoles(groupId);
                try
                {
                    await _context.AddRangeAsync(defaultRoles);
                    _logger.LogInformation("successfully added roles for the default user");

                }catch(Exception)
                {
                    _logger.LogError("could not add roles ");
                }
 
            }
            await _context.SaveChangesAsync();
        }

        public async Task TrySeedLookup() {

            Lookup[] paymentTypes = {
                new Lookup {
                    Key = "key",
                    Value = "Payment"
                },
                new Lookup {
                    Key = "Payment",
                    Value = ShippingAgentPaymentType.Name
                },
                new Lookup {
                    Key = "Payment",
                    Value = TerminalPortPaymentType.Name
                },
                //document lookup
                new Lookup {
                    Key = "key",
                    Value = "Document"
                },
                new Lookup {
                    Key = "Document",
                    Value = "ImportNumber9"
                },
                new Lookup {
                    Key = "Document",
                    Value = "TransferNumber9"
                },
                new Lookup {
                    Key = "Document",
                    Value = "T1"
                },
                new Lookup {
                    Key = "Document",
                    Value = "Number4"
                },
                new Lookup {
                    Key = "Document",
                    Value = "Number1"
                },
                new Lookup {
                    Key = "Document",
                    Value = "PackageList"
                },
                new Lookup {
                    Key = "Document",
                    Value = "TruckWayBill"
                },
                new Lookup {
                    Key = "Document",
                    Value = "CommercialInvoice"
                }
            };
            _context.Lookups.AddRange(paymentTypes);

            var shippingAgentPaymentNames = from type in ShippingAgentPaymentType.Types select new Lookup {Key = ShippingAgentPaymentType.Name, Value = type};
            _context.Lookups.AddRange(shippingAgentPaymentNames);

            var terminalPortPaymentNames = from type in TerminalPortPaymentType.Types select new Lookup {Key = TerminalPortPaymentType.Name, Value = type};
            _context.Lookups.AddRange(terminalPortPaymentNames);

            await _context.SaveChangesAsync();

        }

    }
}