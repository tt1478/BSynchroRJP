using BSynchroRJP.ActionFilters;
using Contracts;
using Entities;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSynchroRJP.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCore(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            });
        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => { });
        }
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddScoped<IloggerManager, LoggerManager>();
        }
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default"), b => b.MigrationsAssembly(nameof(BSynchroRJP))));
        }
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<AccountRepository>();
            services.AddScoped<AccountStatusRepository>();
            services.AddScoped<AccountSubTypeRepository>();
            services.AddScoped<AccountTypeRepository>();
            services.AddScoped<TransactionRepository>();
        }
        public static void ConfigureActionFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidateAccountExistsAttribute>();
            services.AddScoped<ValidateAccountStatusExistsAttribute>();
            services.AddScoped<ValidateAccountSubTypeExistsAttribute>();
            services.AddScoped<ValidateAccountTypeExistsAttribute>();
            services.AddScoped<ValidateTransactionForAccountExistsAttribute>();
            services.AddScoped<ValidationFilterAttribute>();
        }
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<Customer>(o => 
            { 
                o.Password.RequireDigit = true; 
                o.Password.RequireLowercase = false; 
                o.Password.RequireUppercase = false; 
                o.Password.RequireNonAlphanumeric = false; 
                o.Password.RequiredLength = 10; 
                o.User.RequireUniqueEmail = true; 
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services); 
            builder.AddEntityFrameworkStores<RepositoryContext>().AddDefaultTokenProviders();
        }
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration) 
        { 
            var jwtSettings = configuration.GetSection("JwtSettings"); 
            var secretKey = Environment.GetEnvironmentVariable("SECRET"); 
            services.AddAuthentication(opt => 
            { 
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
            }).AddJwtBearer(options => 
            { 
                options.TokenValidationParameters = new TokenValidationParameters 
                { 
                    ValidateIssuer = true, 
                    ValidateAudience = true, 
                    ValidateLifetime = true, 
                    ValidateIssuerSigningKey = true, 
                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value, 
                    ValidAudience = jwtSettings.GetSection("validAudience").Value, 
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)) 
                }; 
            }); 
        }
        public static void ConfigureAuthenticationManager(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
        }
    }
}
