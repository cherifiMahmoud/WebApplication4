using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.Core.Models;

namespace WebApplication4.Data
{
    public class DataAccess : IdentityDbContext<AppUser,IdentityRole,string>
    {
        public DataAccess(DbContextOptions options) : base(options)
        {

        }
    }
}
