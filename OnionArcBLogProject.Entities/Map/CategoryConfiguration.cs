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
    public class CategoryConfiguration : CoreConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.Property(x => x.CategoryName).IsRequired(true).HasMaxLength(50);

            builder.Property(x => x.CategoryDescription).IsRequired(false).HasMaxLength(250);

            base.Configure(builder);
        }
    }
}
