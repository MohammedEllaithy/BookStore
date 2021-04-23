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
    class AuthorsSchema : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            /* 1 - Define the table */
            builder.ToTable<Author>("Authors", BookStoreContext.DEFAULT_SCHEMA);
            /* 2 - Set the primary key of the table */
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasDefaultValueSql("NEWID()");
            builder.HasData(
                new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "Taha Hussein",
                    Nationality = "Egypt",
                    TenantId = new Guid("D704C4F3-0EA7-4B2F-8C58-D7D0F10E6416")
                },
                new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "Naguib Mahfouz",
                    Nationality = "Egypt",
                    TenantId = new Guid("D704C4F3-0EA7-4B2F-8C58-D7D0F10E6416")
                });

            /* 3 - Set properties' (columns') constraints */
            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .Property(p => p.Nationality)
                .HasMaxLength(30);

            /* 4 - Set relationships between tables */

            builder
                .HasOne(author => author.Tenant)
                .WithMany()
                .HasForeignKey(author => author.TenantId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

