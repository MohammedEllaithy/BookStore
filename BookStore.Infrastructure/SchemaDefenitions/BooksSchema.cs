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
    class BooksSchema : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            /* 1 - Define the table */
            builder.ToTable<Book>("Books", BookStoreContext.DEFAULT_SCHEMA);
            /* 2 - Set the primary key of the table */
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasDefaultValueSql("NEWID()");

            /* 3 - Set properties' (columns') constraints */
            // Name column
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            /* 4 - Set relationships between tables */
            // Book - author : Many-to-one
            builder
                .HasOne(book => book.Author)
                .WithMany(author => author.Books)
                .HasForeignKey(book => book.AuthorId);
            // Book - category : Many-to-One
            builder
                .HasOne(book => book.Category)
                .WithMany(author => author.Books)
                .HasForeignKey(book => book.CategoryId);

            builder
                .HasOne(book => book.Tenant)
                .WithMany()
                .HasForeignKey(book => book.TenantId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
