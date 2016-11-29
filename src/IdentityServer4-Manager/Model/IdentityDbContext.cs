using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Model
{
    public class IdentityDbContext
        : IdentityDbContext<
            Model.IdentityUser>
    {

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            #region what happened
            //what happened ?
            //builder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser").ToTable("User");
            //builder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole").ToTable("Role");
            //builder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim").ToTable("RoleClaim");
            //builder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim").ToTable("UserClaim");
            //builder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin").ToTable("UserLogin");
            //builder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole").ToTable("UserRole");
            //builder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken").ToTable("UserToken");

            //builder.Entity<IdentityUser<int>>().ToTable("User").Property(p => p.Id).HasColumnName("UserId");
            //builder.Entity<IdentityRole<int>>().ToTable("Role").Property(p => p.Id).HasColumnName("UserId");
            //builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim");
            //builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaim");
            //builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogin");
            //builder.Entity<IdentityUserRole<int>>().ToTable("UserRole");
            //builder.Entity<IdentityUserToken<int>>().ToTable("UserToken"); 
            #endregion

            builder.Entity<Model.IdentityUser>(b =>
            {
                b.HasKey(u => u.Id);
                b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
                b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");
                b.ToTable("Users");
                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

                b.Property(u => u.UserName).HasMaxLength(256);
                b.Property(u => u.NormalizedUserName).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);
                b.Property(u => u.NormalizedEmail).HasMaxLength(256);
                b.HasMany(u => u.Claims).WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
                b.HasMany(u => u.Logins).WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
                b.HasMany(u => u.Roles).WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            });

            builder.Entity<Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole>(b =>
            {
                b.HasKey(r => r.Id);
                b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex");
                b.ToTable("Roles");
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                b.Property(u => u.Name).HasMaxLength(256);
                b.Property(u => u.NormalizedName).HasMaxLength(256);

                b.HasMany(r => r.Users).WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();
                b.HasMany(r => r.Claims).WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            });

            builder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.HasKey(uc => uc.Id);
                b.ToTable("UserClaims");
            });

            builder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.HasKey(rc => rc.Id);
                b.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserRole<string>>(b =>
            {
                b.HasKey(r => new { r.UserId, r.RoleId });
                b.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
                b.ToTable("UserLogins");
            });

            builder.Entity<IdentityUserToken<string>>(b =>
            {
                b.HasKey(l => new { l.UserId, l.LoginProvider, l.Name });
                b.ToTable("UserTokens");
            });

            // base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

    }
}
