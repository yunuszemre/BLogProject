using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnionArcBLogProject.Entities.Entities;
using OnionArcBLogProject.Entities.Enum;
using OnionArcBLogProject.Entities.Map;

namespace OnionArcBLogProject.Entities.Context
{
    public class BlogContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(BlogConfiguration.GetConStr());
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    LastName = "Admin",
                    FirstName = "Admin",
                    Status = Status.Active,
                    UserEmail = "admin@blog.com",
                    UserPassword = "Asdf_1234",
                    UserName = "Admin",
                    
                }
                );

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var modifiedentities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added).ToList();
            string ComputerName = Environment.MachineName;
            string ipIdress = "127.0.0.1";
            DateTime date = DateTime.Now;

            foreach (var item in modifiedentities)
            {
                CoreEntity entity = item.Entity as CoreEntity;
                
                if (entity != null)
                {
                    switch (item.State)
                    {
                        case EntityState.Modified:
                            entity.ModifiedComputerName = ComputerName;
                            entity.ModifiedDate = date;
                            entity.ModifiedIp = ipIdress;
                            break;
                        case EntityState.Added:
                            entity.CreatedComputerName = ComputerName;
                            entity.CreatedDate = date;
                            entity.CreatedIp = ipIdress;
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }
        
    }

}
