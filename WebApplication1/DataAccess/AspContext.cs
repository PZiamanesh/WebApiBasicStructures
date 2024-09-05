using Microsoft.EntityFrameworkCore;
using WebApplication1.DomainModels;

namespace WebApplication1.DataAccess
{
    public class AspContext : DbContext
    {
        public AspContext(DbContextOptions<AspContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Seed Data

            modelBuilder.Entity<Author>().HasData(new Author[]
            {
                new Author()
                {
                    Id = 1,
                    AuthorName = "forouzan",
                },
                new Author()
                {
                    Id = 2,
                    AuthorName = "wendel",
                },
                new Author()
                {
                    Id = 3,
                    AuthorName = "santos",
                },
            });

            modelBuilder.Entity<Book>().HasData(new Book[]
            {
                new Book()
                {
                    Id = 1,
                    Title = "tcp/ip",
                    AuthorId = 1,
                    Price = 150.33m,
                    Description = "best book on network theory"
                },
                new Book()
                {
                    Id = 2,
                    Title = "security",
                    AuthorId = 1,
                    Price = 56.20m,
                    Description = "describes cryptography and network security concepts"
                },
                new Book()
                {
                    Id = 3,
                    Title = "ccna",
                    AuthorId = 2,
                    Price = 170,
                    Description = "best book on practical networking concepts and configurations"
                }
            });

            #endregion

            modelBuilder.Entity<Book>().HasOne(a => a.Author).WithMany(a => a.Books)
                .HasForeignKey(a => a.AuthorId)
                .HasConstraintName("FK_Books_Authors_AuthorId");
        }
    }
}
