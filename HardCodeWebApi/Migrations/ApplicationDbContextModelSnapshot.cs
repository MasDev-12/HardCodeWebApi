﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using HardCodeWebApi.ApplicationDbContext;

#nullable disable

namespace HardCodeWebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext.ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HardCodeWebApi.Features.Categories.Domain.AdditionalField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("AdditionalFields", (string)null);
                });

            modelBuilder.Entity("HardCodeWebApi.Features.Categories.Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("HardCodeWebApi.Features.Products.Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("Price");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("HardCodeWebApi.Features.Products.Domain.ProductField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("AdditionalFieldId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Value");

                    b.HasKey("Id");

                    b.HasIndex("AdditionalFieldId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductField", (string)null);
                });

            modelBuilder.Entity("HardCodeWebApi.Features.Categories.Domain.AdditionalField", b =>
                {
                    b.HasOne("HardCodeWebApi.Features.Categories.Domain.Category", "Category")
                        .WithMany("AdditionalFields")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("HardCodeWebApi.Features.Products.Domain.Product", b =>
                {
                    b.HasOne("HardCodeWebApi.Features.Categories.Domain.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("HardCodeWebApi.Features.Products.Domain.ProductField", b =>
                {
                    b.HasOne("HardCodeWebApi.Features.Categories.Domain.AdditionalField", "AdditionalField")
                        .WithMany()
                        .HasForeignKey("AdditionalFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HardCodeWebApi.Features.Products.Domain.Product", "Product")
                        .WithMany("ProductFields")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdditionalField");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("HardCodeWebApi.Features.Categories.Domain.Category", b =>
                {
                    b.Navigation("AdditionalFields");
                });

            modelBuilder.Entity("HardCodeWebApi.Features.Products.Domain.Product", b =>
                {
                    b.Navigation("ProductFields");
                });
#pragma warning restore 612, 618
        }
    }
}
