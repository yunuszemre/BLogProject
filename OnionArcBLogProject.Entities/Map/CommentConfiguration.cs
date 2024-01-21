using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArcBLogProject.Entities.Entities;

namespace OnionArcBLogProject.Entities.Map
{
    public class CommentConfiguration : CoreConfiguration<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.Property(x => x.CommentText).IsRequired(true).HasMaxLength(1000);
            base.Configure(builder);

        }
    }
}
