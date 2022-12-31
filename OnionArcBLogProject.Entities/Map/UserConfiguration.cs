using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArcBLogProject.Core.Map;
using OnionArcBLogProject.Entities.Entities;

namespace OnionArcBLogProject.Entities.Map
{
    public class UserConfiguration : CoreConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("AppUsers");
            builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.Title).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.UserEmail).IsRequired(true).HasMaxLength(255);
            builder.Property(x => x.ImageUrl).IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.UserPassword).IsRequired(true).HasMaxLength(1000);
            builder.Property(x => x.LastLogin).IsRequired(false);
            builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.LastLogin).IsRequired(false);
            builder.Property(x => x.LastIpAdress).IsRequired(false).HasMaxLength(24);
            base.Configure(builder);

        }
    }
}
