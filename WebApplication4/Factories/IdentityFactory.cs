using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Core.Models;
using WebApplication4.Data;

namespace WebApplication4.Factories
{
    public static class IdentityFactory
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<DataAccess>();

            return services;
        }

        public static  IServiceCollection ConfigureIdentityOptions (this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                //Not dublicate email 
                options.User.RequireUniqueEmail = true;
            });
            return services;
        }

        public static IServiceCollection ConfigureWeekPassword(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            });
            return services;
        }
    }
}



