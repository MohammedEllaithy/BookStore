using BookStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.SchemaDefenitions
{
    class TenantsSchema : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tenant> builder)
        {
            /* 1 - Define the table */
            builder.ToTable<Tenant>("Tenants", BookStoreContext.DEFAULT_SCHEMA);
            /* 2 - Set the primary key of the table */
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasDefaultValueSql("NEWID()");
            builder.HasData(
                new Tenant
                {
                    Id = new Guid("D704C4F3-0EA7-4B2F-8C58-D7D0F10E6416"),
                    Domain = "Ellaithy.ALEFbookstores.net",
                    Name = "Default Domain",
                    Description = "This is the default landing domain for Ellaithy Book stores.",
                    // Egypt time
                    CreatedAt = DateTime.UtcNow.Add(new TimeSpan(2, 0, 0)),
                    UpdatedAt = DateTime.UtcNow.Add(new TimeSpan(2, 0, 0))
                });

            /* 3 - Set properties' (columns') constraints */
            builder.Property(p => p.Domain)
                .IsRequired()
                .HasMaxLength(800);
            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);
            builder
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(600);
        }
    }
}
