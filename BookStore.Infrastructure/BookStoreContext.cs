using Microsoft.EntityFrameworkCore;
using BookStore.Core.Entities;
using BookStore.Infrastructure.SchemaDefenitions;

namespace BookStore.Infrastructure
{
    public class BookStoreContext : DbContext
    {
        /// <summary>
        /// Constructor of BookStoreContext class to pass options to the DbContext base class
        /// <param name="options">Options of database to be passed to the ORM</param>
        /// </summary>

        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {

        }
        public const string DEFAULT_SCHEMA = "BookStore";
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Overriden modification: Create configurations of schemas
            builder.ApplyConfiguration(new TenantsSchema());
            builder.ApplyConfiguration(new AuthorsSchema());
            builder.ApplyConfiguration(new ReviewsSchema());
            builder.ApplyConfiguration(new CategoriesSchema());
            builder.ApplyConfiguration(new BooksSchema());

            // Call the base(parent) version of OnModelCreating
            base.OnModelCreating(builder);
        }

    }
}
