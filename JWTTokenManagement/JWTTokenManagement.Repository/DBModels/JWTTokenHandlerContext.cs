using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JWTTokenManagement.Repository.DBModels
{
    public partial class JWTTokenHandlerContext : DbContext
    {
        public JWTTokenHandlerContext()
        {
        }

        public JWTTokenHandlerContext(DbContextOptions<JWTTokenHandlerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
              //  optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=JWTTokenHandler;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.Property(e => e.RefreshTokenId).HasColumnName("RefreshTokenID");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshToken)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("RefreshToken_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(30);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30);
            });
        }
    }
}
