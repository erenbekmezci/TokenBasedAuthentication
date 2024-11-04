using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.Products;
using Repositories.Users;
using Repositories.UserRefreshTokens;
using System.Reflection;

namespace Repositories
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole , string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);//bu proje klasörü yani assembly (repository)  deki IEntityConfigurationı implemente eden tüm sınıfları al demek
            //Console.WriteLine("asdasdasdasd",Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
