using BookStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.SchemaDefenitions
{
    class CategoriesSchema : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            /* 1 - Define the table */
            builder.ToTable<Category>("Categories", BookStoreContext.DEFAULT_SCHEMA);
            /* 2 - Set the primary key of the table */
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasDefaultValueSql("NEWID()");
            /* 3 - Seeding with some initial data */
            builder.HasData(
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Machine Learning",
                    TenantId = new Guid("D704C4F3-0EA7-4B2F-8C58-D7D0F10E6416")
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Literature",
                    TenantId = new Guid("D704C4F3-0EA7-4B2F-8C58-D7D0F10E6416")
                }
                ,
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Quality of Control",
                    TenantId = new Guid("D704C4F3-0EA7-4B2F-8C58-D7D0F10E6416")
                }
                ,
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Data Structure",
                    TenantId = new Guid("D704C4F3-0EA7-4B2F-8C58-D7D0F10E6416")
                }
                );

            /* 3 - Set properties' (columns') constraints */
            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);
            builder
                .HasOne(category => category.Tenant)
                .WithMany()
                .HasForeignKey(category => category.TenantId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
