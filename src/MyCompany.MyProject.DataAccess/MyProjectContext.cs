using Microsoft.EntityFrameworkCore;
using MyCompany.MyProject.DataBase.Models;

namespace DataBase
{
    public partial class MyProjectContext : DbContext
    {
        public MyProjectContext(DbContextOptions<MyProjectContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Users> Users { get; set; }
        // Unable to generate entity type for table 'queue_status'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("ReplaceUrl");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users");
                entity.HasIndex(e => e.Id)
                    .HasName("INDEX");

                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(255)");

                entity.Property(e => e.UserName)
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.UserAge)
                    .HasColumnName("age")
                    .HasColumnType("int(3)");    
            });
        }
    }
}
