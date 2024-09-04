﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.DataAccess;


#nullable disable

namespace WebApplication1.Models.Data.Migrations
{
    [DbContext(typeof(AspContext))]
    partial class AspContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication1.Models.Domain.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorName = "forouzan"
                        },
                        new
                        {
                            Id = 2,
                            AuthorName = "wendel"
                        },
                        new
                        {
                            Id = 3,
                            AuthorName = "santos"
                        });
                });

            modelBuilder.Entity("WebApplication1.Models.Domain.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            Price = 150.33m,
                            Title = "tcp/ip"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 1,
                            Price = 56.20m,
                            Title = "security"
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 2,
                            Price = 170m,
                            Title = "ccna"
                        });
                });

            modelBuilder.Entity("WebApplication1.Models.Domain.Book", b =>
                {
                    b.HasOne("WebApplication1.Models.Domain.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Books_Authors_AuthorId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("WebApplication1.Models.Domain.Author", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
