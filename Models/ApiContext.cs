using Microsoft.EntityFrameworkCore;

namespace CRUD.Models
{
    public partial class ApiContext : DbContext
    {
        public ApiContext() {}

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) {}

        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("ConnectionStringPrincipal");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
