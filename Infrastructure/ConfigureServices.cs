﻿using Application.Common.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Db config
            services.AddDbContext<AppDbContext>(options =>
            {
                // options.UseSqlServer(configuration.GetConnectionString("appDbConnectionString"),
                //     builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));

                // options.UseMySql(configuration.GetConnectionString("appDbConnectionString"),
                //   Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"),
                // builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));

                options.UseMySql(configuration.GetConnectionString("appDbConnectionString"),
                  Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"),
                  mySqlOptions => mySqlOptions.EnableRetryOnFailure());



            });
            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());


            //     --- mail service config
            //     var emailConfig = configuration
            //             .GetSection("EmailConfiguration")
            //             .Get<EmailConfiguration>();
            //    services.AddSingleton(emailConfig);



            //local config
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddScoped<AppDbContextInitializer>();


            // auth config
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<AppDbContext>()
                  .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));

            services.AddAuthentication(ops =>
            {
                ops.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                ops.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                ops.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))


                };

            });
            return services;
        }
    }
}
