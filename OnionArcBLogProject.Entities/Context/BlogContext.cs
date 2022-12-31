using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnionArcBLogProject.Core.Entity;
using OnionArcBLogProject.Entities.Entities;
using OnionArcBLogProject.Entities.Map;

namespace OnionArcBLogProject.Entities.Context
{
    public class BlogContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-BODOH2U\\SA; Database=BlogDB; uid=sa; pwd=1234");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            modelBuilder.ApplyConfiguration(new PostConfiguration());

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
