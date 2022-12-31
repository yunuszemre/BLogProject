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
    public class PostConfiguration : CoreConfiguration<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");

            builder.Property(x=>x.Title).IsRequired(false).HasMaxLength(50);

            builder.Property(x => x.PostDetail).IsRequired(true);

            builder.Property(x=>x.ImagePath).IsRequired(false);

            builder.Property(x=>x.Tags).IsRequired(false);

            base.Configure(builder);
        }
    }
}
