using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentityServer4.Manager.Data.DbContexts
{
    public class IdentityServerDbContext : IdentityDbContext<Model.IdentityUser>,
        IConfigurationDbContext,
        IPersistedGrantDbContext
    {
        public IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options) : base(options)
        { }

        public DbSet<ApiResource> ApiResources { set; get; }
        public DbSet<ApiResourceClaim> ApiResourceClaims { set; get; }
        public DbSet<ApiScope> ApiScopes { set; get; }
        public DbSet<ApiScopeClaim> ApiScopeClaims { set; get; }
        public DbSet<ApiSecret> ApiSecrets { set; get; }
        public DbSet<Client> Clients { set; get; }
        public DbSet<ClientClaim> ClientClaims { set; get; }
        public DbSet<ClientCorsOrigin> ClientCorsOrigins { set; get; }
        public DbSet<ClientGrantType> ClientGrantTypes { set; get; }
        public DbSet<ClientIdPRestriction> ClientIdPRestrictions { set; get; }
        public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { set; get; }
        public DbSet<ClientRedirectUri> ClientRedirectUris { set; get; }
        public DbSet<ClientScope> ClientScopes { set; get; }
        public DbSet<ClientSecret> ClientSecrets { set; get; }
        public DbSet<IdentityClaim> IdentityClaims { set; get; }
        public DbSet<IdentityResource> IdentityResources { set; get; }
        public DbSet<PersistedGrant> PersistedGrants { set; get; }
        public DbSet<Secret> Secrets { set; get; }

        //public DbSet<UserClaim> UserClaims { set; get; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
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
