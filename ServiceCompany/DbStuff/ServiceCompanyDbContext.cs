using ManagementCompany.DbStuff.ExternalEntities;
using Microsoft.EntityFrameworkCore;
using ServiceCompany.DbStuff.Models;
using System.Reflection.Emit;

namespace ServiceCompany.DbStuff
{
    public class ServiceCompanyDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<CompanyProfile> CompanyProfiles { get; set; }

        public DbSet<Alert> Alerts { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<MemberRole> MemberRoles { get; set; }

        public DbSet<UserTask> UserTasks { get; set; }

        public DbSet<UserTaskStatus> TaskStatuses { get; set; }

        public DbSet<ViewEntity> Views { get; set; }

        public DbSet<DocumentEntity> Documents { get; set; }

        public DbSet<Warning> Warnings { get; set; }

        public DbSet<UserWorkSet> WorkSets { get; set; }

        public DbSet<DesignOptionEntity> DesignOptions { get; set; }

        public DbSet<GridEntity> Grids { get; set; }

        public DbSet<LevelEntity> Levels { get; set; }

        public DbSet<CategoryEntity> Categories { get; set; }

        public DbSet<RevitCategoryEntity> RevitCategoryEntities { get; set; }

        public DbSet<LocationEntity> Locations { get; set; }

        public DbSet<BasePointEntity> BasePoints { get; set; }

        public DbSet<WarningElementEntity> WarningElements { get; set; }

        public DbSet<ElementTypeEntity> Types { get; set; }

        public DbSet<ElementEntity> Elements { get; set; }

        public DbSet<MaterialEntity> Materials { get; set; }

        public DbSet<ParameterEntity> Parameters { get; set; }

        public ServiceCompanyDbContext(DbContextOptions<ServiceCompanyDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<DesignOptionEntity>()
                .HasOne(x => x.Document)
                .WithMany(x => x.DesignOptions)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ViewEntity>()
                .HasOne(x => x.Document)
                .WithMany(x => x.Views)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ParameterEntity>()
                .HasOne(x => x.Document)
                .WithMany(x => x.Parameters)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ElementTypeEntity>()
                .HasOne(x => x.Document)
                .WithMany(x => x.ElementTypes)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ElementEntity>()
                .HasOne(x => x.Document)
                .WithMany(x => x.Elements)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ElementEntity>()
                .HasOne(x => x.Level)
                .WithMany(x => x.Elements)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ElementEntity>()
                .HasOne(x => x.Type)
                .WithMany(x => x.Elements)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<MaterialEntity>()
                .HasOne(x => x.Document)
                .WithMany(x => x.Materials)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<GridEntity>()
                .HasOne(x => x.Document)
                .WithMany(x => x.Grids)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<LevelEntity>()
                .HasOne(x => x.Document)
                .WithMany(x => x.Levels)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<LevelEntity>()
                .HasOne(x => x.ElementType)
                .WithMany(x => x.Levels)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<GridEntity>()
                .HasOne(x => x.ElementType)
                .WithMany(x => x.Grids)
                .OnDelete(DeleteBehavior.NoAction);
            #region Company

            builder
                .Entity<Company>()
                .HasMany(c => c.Projects)
                .WithOne(p => p.Company)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Company>()
                .HasMany(c => c.Users)
                .WithOne(e => e.Company)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region Permission
            #endregion
            #region Project

            builder
                .Entity<Project>()
                .HasOne(p => p.Company)
                .WithMany(c => c.Projects)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Project>()
                .HasMany(p => p.Users)
                .WithMany(c => c.Projects);
            #endregion
            #region User
            builder
                .Entity<User>()
                .HasMany(u => u.Projects)
                .WithMany(p => p.Users);

            builder
                .Entity<User>()
                .HasOne(u => u.Company)
                .WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<User>()
                .HasMany(u => u.CreatedTasks)
                .WithOne(p => p.Author)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<User>()
                .HasMany(u => u.ExecutedTasks)
                .WithOne(p => p.Executor)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region Task
            builder
                .Entity<UserTask>()
                .HasOne(user => user.Author)
                .WithMany(task => task.CreatedTasks)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<UserTask>()
                .HasOne(user => user.Executor)
                .WithMany(task => task.ExecutedTasks)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region TaskStatus
            #endregion
            #region Article
            builder
                .Entity<Article>()
                .HasOne(a => a.Author)
                .WithMany(u => u.Articles)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region Alerts
            builder
                .Entity<Alert>()
                .HasMany(a => a.NotifiedUsers)
                .WithMany(u => u.SeenAlerts);

            builder
                .Entity<Alert>()
                .HasOne(a => a.Author)
                .WithMany(u => u.CreatedAlerts)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            builder.Entity<User>()
                .HasOne(x => x.Profile)
                .WithOne(x => x.User)
                .HasForeignKey<UserProfile>(x=>x.Id)
                .HasConstraintName("ProfileId")
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserProfile>()
                .HasOne(x => x.User)
                .WithOne(x => x.Profile)
                .HasForeignKey<User>(x => x.Id)
                .HasConstraintName("UserId")
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Company>()
                .HasOne(x => x.Profile)
                .WithOne(x => x.Company)
                .HasForeignKey<CompanyProfile>(x => x.Id)
                .HasConstraintName("ProfileId")
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CompanyProfile>()
                .HasOne(x => x.Company)
                .WithOne(x => x.Profile)
                .HasForeignKey<Company>(x => x.Id)
                .HasConstraintName("CompanyId")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
