using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StartProject.Entities;

namespace StartProject.Config
{
    public class ContextBase : IdentityDbContext<AppUser>
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        { }

        public DbSet<Product> Product { get; set; }

        public DbSet<AppUser> AppUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetStringConnection());
                base.OnConfiguring(optionsBuilder);
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
            base.OnModelCreating(builder);
        }


        //connection string database
        private static string GetStringConnection()
        {
            return "DATA SOURCE=DESKTOP-2GB4NEA\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=StartProject";
        }

    }
}