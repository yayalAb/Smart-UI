﻿using Domain.Common.DestinationTypes;
using Domain.Common.DocumentType;
using Domain.Common.PaymentTypes;
using Domain.Common.Units;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Domain.Common.DocumentType.DocumentType;

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
                // await TrySeedAsync();
                // await TrySeedLookup();
                // await TrySeedSettings();
                // await removeLookups();
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
            int groupId = 0;
            UserGroup defaultGroup = new UserGroup
            {
                Name = "AdminGroup",
                Responsiblity = "adminstration",

            };
            var found_group = _context.UserGroups.Where(ug => ug.Name == defaultGroup.Name).FirstOrDefault();
            if (found_group == null)
            {

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

            }
            else
            {
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

            try
            {
                await _context.Addresses.AddAsync(defaultAddress);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
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

                }
                catch (Exception)
                {
                    _logger.LogError("could not add roles ");
                }

            }
            await _context.SaveChangesAsync();
        }

        public async Task TrySeedLookup()
        {

            var allLookups = await _context.Lookups.ToListAsync();

            List<Lookup> lookupKeyList = new List<Lookup>();
            
            if(!allLookups.Any(l => (l.Key == "key" && l.Value == "Payment"))) {
                lookupKeyList.Add(new Lookup {
                    Key = "key",
                    Value = "Payment"
                });
            }

            if(!allLookups.Any(l => (l.Key == "Payment" && l.Value == ShippingAgentPaymentType.Name))) {
                lookupKeyList.Add(new Lookup {
                    Key = "Payment",
                    Value = ShippingAgentPaymentType.Name
                });
            }

            if(!allLookups.Any(l => (l.Key == "Payment" && l.Value == TerminalPortPaymentType.Name))) {
                lookupKeyList.Add(new Lookup {
                    Key = "Payment",
                    Value = TerminalPortPaymentType.Name
                });
            }

            if(!allLookups.Any(l => (l.Key == "key" && l.Value == "Document"))) {
                lookupKeyList.Add(new Lookup {
                    Key = "key",
                    Value = "Document"
                });
            }

            if(!allLookups.Any(l => (l.Key == "key" && l.Value == "Documentation"))) {
                lookupKeyList.Add(new Lookup {
                    Key = "key",
                    Value = "Documentation"
                });
            }

            if(!allLookups.Any(l => (l.Key == "key" && l.Value == "DestinationType"))) {
                lookupKeyList.Add(new Lookup {
                    Key = "key",
                    Value = "DestinationType"
                });
            }

            if(!allLookups.Any(l => (l.Key == "key" && l.Value == "Currency"))) {
                lookupKeyList.Add(new Lookup {
                    Key = "key",
                    Value = "Currency"
                });
            }
            
            if(!allLookups.Any(l => (l.Key == "key" && l.Value == "WeightUnit"))) {
                lookupKeyList.Add(new Lookup {
                    Key = "key",
                    Value = "WeightUnit"
                });
            }

            foreach(var type in DocumentType.Types){
                if(!allLookups.Any(l => (l.Key == "Document" && l.Value == type))) {
                    lookupKeyList.Add(new Lookup {
                                         Key = "Document",
                                         Value = type
                                     });
                }
            }

            foreach(var type in DocumentationType.Types){
                if(!allLookups.Any(l => (l.Key == "Documentation" && l.Value == type))) {
                    lookupKeyList.Add(new Lookup {
                                        Key = "Documentation",
                                        Value = type
                                    });
                }
            }

            foreach(var type in DestinationType.Types){
                if(!allLookups.Any(l => (l.Key == "DestinationType" && l.Value == type))) {
                    lookupKeyList.Add(new Lookup {
                                        Key = "DestinationType",
                                        Value = type
                                    });
                }
            }

            foreach(var type in WeightUnits.Units) {
                if(!allLookups.Any(l => (l.Key == "WeightUnit" && l.Value == type))) {
                    lookupKeyList.Add(new Lookup {
                                        Key = "WeightUnit",
                                        Value = type
                                    });
                }
            }

            foreach(var type in Currency.Currencies) {
                if(!allLookups.Any(l => (l.Key == "Currency" && l.Value == type))) {
                    lookupKeyList.Add(new Lookup {
                                            Key = "Currency",
                                            Value = type
                                        });
                }
            }

            foreach(var type in ShippingAgentPaymentType.Types) {
                if(!allLookups.Any(l => (l.Key == ShippingAgentPaymentType.Name && l.Value == type))) {
                    lookupKeyList.Add(new Lookup { 
                            Key = ShippingAgentPaymentType.Name, 
                            Value = type 
                        });
                }
            }

            foreach(var type in TerminalPortPaymentType.Types) {
                if(!allLookups.Any(l => (l.Key == TerminalPortPaymentType.Name && l.Value == type))) {
                    lookupKeyList.Add(new Lookup {
                            Key = TerminalPortPaymentType.Name,
                            Value = type
                        });
                }
            }

            _context.Lookups.AddRange(lookupKeyList);
            await _context.SaveChangesAsync();

        }

        public async Task TrySeedSettings()
        {

            if (_context.Settings.Any())
            {
                return;
            }

            var executionStrategy = _context.database.CreateExecutionStrategy();
            await executionStrategy.ExecuteAsync(async () =>
            {
                using (var transaction = _context.database.BeginTransaction())
                {
                    try
                    {

                        var addressInfo = new Address
                        {
                            Email = "ghioninternationalfzco@gmail.com",
                            Phone = "+25321353730",
                            Region = "EAST AFRIC",
                            City = "Djibouti",
                            Subcity = "Djibouti",
                            Country = "REPUBLIC DE DJIBOUTI",
                            POBOX = "0000"
                        };
                        _context.Addresses.Add(addressInfo);
                        await _context.SaveChangesAsync();
                        var defaultCompany = new Company
                        {
                            Name = "Ghion International",
                            TinNumber = "111",
                            CodeNIF = "code_nif",
                            // ContactPersonId = contactPerson.Id,
                            AddressId = addressInfo.Id,
                        };
                        _context.Companies.Add(defaultCompany);
                        await _context.SaveChangesAsync();

                        var contactPerson = new ContactPerson
                        {
                            Name = "Abnet Kebede",
                            Email = "ab@absoft.net",
                            Phone = "0987654321",
                            TinNumber = "3478568",
                            Country = "DEMOCRATIC REPUBLIC OF ETHIOPIA",
                            City = "ADDIS ABABA",
                            CompanyId = defaultCompany.Id

                        };
                        _context.ContactPeople.Add(contactPerson);
                        await _context.SaveChangesAsync();





                        var bankInfo = new BankInformation
                        {
                            AccountHolderName = "GHION INTERNATIONAL DJIBUTI",
                            BankName = "CAC INTERNATIONAL BANK",
                            AccountNumber = "1003499041",
                            SwiftCode = "CACDDJJD",
                            BankAddress = "DJIBOUTI, REPUBLIC DE DJIBOUTI",
                            CompanyId = defaultCompany.Id,
                            CurrencyType = "USD"
                        };

                        _context.BankInformation.Add(bankInfo);
                        await _context.SaveChangesAsync();

                        var setting = new Setting
                        {
                            Email = "tihitnatsegaye7@gmail.com",
                            Password = "tiucpqdxigzogxco",
                            Port = 456,
                            Host = "smtp.gmail.com",
                            Protocol = "SMTP",
                            Username = "tihitnatsegaye7@gmail.com",
                            CompanyId = defaultCompany.Id
                        };

                        _context.Settings.Add(setting);
                        await _context.SaveChangesAsync();

                        transaction.Commit();

                    }
                    catch (System.Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }

                }
            });

        }

        public async Task removeLookups()
        {
            string[] types = { "Payment", "Document", "Documentation", "DestinationType" };
            var key_list = _context.Lookups.Where(l => (l.Key == "key" && types.Contains(l.Value)) || types.Contains(l.Key) || l.Key == ShippingAgentPaymentType.Name || l.Key == TerminalPortPaymentType.Name || ShippingAgentPaymentType.Types.Contains(l.Key) || TerminalPortPaymentType.Types.Contains(l.Key)).ToList();
            _context.RemoveRange(key_list);
            await _context.SaveChangesAsync();
        }
    }
}