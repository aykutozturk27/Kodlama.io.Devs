using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<ProgrammingTechnology> ProgrammingTechnologies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<UserSocialMedia> UserSocialMedias { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(a =>
            {
                a.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasMany(p => p.ProgrammingTechnologies);
            });

            modelBuilder.Entity<ProgrammingTechnology>(a =>
            {
                a.ToTable("ProgrammingTechnologies").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasOne(p => p.ProgrammingLanguage);
            });

            modelBuilder.Entity<User>(a =>
            {
                a.ToTable("Users").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.FirstName).HasColumnName("FirstName");
                a.Property(p => p.LastName).HasColumnName("LastName");
                a.Property(p => p.Email).HasColumnName("Email");
                a.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
                a.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
                a.Property(p => p.Status).HasColumnName("Status");
                a.HasMany(p => p.UserOperationClaims);
                a.HasMany(p => p.RefreshTokens);
                a.HasMany(p => p.UserSocialMedias);
            });

            modelBuilder.Entity<OperationClaim>(a =>
            {
                a.ToTable("OperationClaims").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<UserOperationClaim>(a =>
            {
                a.ToTable("UserOperationClaims").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.OperationClaimId).HasColumnName("OperationClaimId");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.HasOne(p => p.User);
                a.HasOne(p => p.OperationClaim);
            });

            modelBuilder.Entity<UserSocialMedia>(a =>
            {
                a.ToTable("UserSocialMedias").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.Property(p => p.Url).HasColumnName("Url");
                a.HasOne(p => p.User);
            });

            ProgrammingLanguage[] programmingLanguageEntitySeeds = { new(1, "C#"), new(2, "Java") };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageEntitySeeds);

            ProgrammingTechnology[] programmingTechnologyEntitySeeds = { new(1, 1, "WPF"), new(2, 1, "ASP.NET"), new(3, 2, "Spring") };
            modelBuilder.Entity<ProgrammingTechnology>().HasData(programmingTechnologyEntitySeeds);

            User[] userEntitySeeds = { new(1, "ADMINISTRATOR", "ADMINISTRATOR",
             "administrator@administrator.com", Encoding.ASCII.GetBytes("administrator"),
             Encoding.ASCII.GetBytes("administrator"), true, Core.Security.Enums.AuthenticatorType.Email),
             new(2, "USER", "USER", "user@user.com", Encoding.ASCII.GetBytes("user"), Encoding.ASCII.GetBytes("user"),
             true, Core.Security.Enums.AuthenticatorType.Email)};
            modelBuilder.Entity<User>().HasData(userEntitySeeds);

            OperationClaim[] operationClaimEntitySeeds = { new(1, "Administrator"),
                new(2, "User") };
            modelBuilder.Entity<OperationClaim>().HasData(operationClaimEntitySeeds);

            UserOperationClaim[] userOperationClaimEntitySeeds = { new(1, 1, 1),
                new(2, 1, 1) };
            modelBuilder.Entity<UserOperationClaim>().HasData(userOperationClaimEntitySeeds);

            UserSocialMedia[] userSocialMediaEntitySeeds = { new(1, 1, "https://github.com/aykutozturk27"),
                new(2, 2, "https://github.com/engindemirog") };
            modelBuilder.Entity<UserSocialMedia>().HasData(userSocialMediaEntitySeeds);

        }
    }
}
