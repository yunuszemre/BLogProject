using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using OnionArcBLogProject.Entities.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArcBLogProject.Entities
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BlogContext>
    {

        public BlogContext CreateDbContext(string[] args)
        {
           
            DbContextOptionsBuilder<BlogContext> optionsBuilder = new();
            optionsBuilder.UseSqlServer(BlogConfiguration.GetConStr());

            return new BlogContext(optionsBuilder.Options);
        }
    }
}
