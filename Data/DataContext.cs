using demo.WebApiDotNetCore8.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace demo.WebApiDotNetCore8.Data
{
   public class DataContext : IdentityDbContext<IdentityUser>
   {
      public DataContext(DbContextOptions<DataContext> options) : base(options) { }

      public DbSet<Court> Courts { get; set; }

   }
}
