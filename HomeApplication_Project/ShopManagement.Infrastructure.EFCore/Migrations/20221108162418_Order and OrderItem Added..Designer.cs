﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopManagement.Infrastructure.EFCore;

namespace ShopManagement.Infrastructure.EFCore.Migrations
{
    [DbContext(typeof(At_HomeApplicationContext))]
    [Migration("20221108162418_Order and OrderItem Added.")]
    partial class OrderandOrderItemAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ShopManagement.Domain.OrderAgg.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("DiscountAmount")
                        .HasColumnType("float");

                    b.Property<bool>("IsCanceled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<string>("IssueTrackingNo")
                        .HasColumnType("nvarchar(8)")
                        .HasMaxLength(8);

                    b.Property<double>("PayAmount")
                        .HasColumnType("float");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<long>("RefId")
                        .HasColumnType("bigint");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductAgg.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductCategoryAgg.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductPictureSliderAgg.ProductPictureSlider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Alt")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2")
                        .HasMaxLength(500);

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductPicturesSlider");
                });

            modelBuilder.Entity("ShopManagement.Domain.SlideAgg.Slide", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ButtonText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Heading")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("PictureAlt")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("PictureTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Slides");
                });

            modelBuilder.Entity("ShopManagement.Domain.OrderAgg.Order", b =>
                {
                    b.OwnsMany("ShopManagement.Domain.OrderAgg.OrderItem", "Items", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("Count")
                                .HasColumnType("int");

                            b1.Property<DateTime>("CreationDate")
                                .HasColumnType("datetime2");

                            b1.Property<int>("DiscountRate")
                                .HasColumnType("int");

                            b1.Property<int>("OrderId")
                                .HasColumnType("int");

                            b1.Property<int>("ProductId")
                                .HasColumnType("int");

                            b1.Property<double>("UnitPrice")
                                .HasColumnType("float");

                            b1.HasKey("Id");

                            b1.HasIndex("OrderId");

                            b1.ToTable("OrderItems");

                            b1.WithOwner("Order")
                                .HasForeignKey("OrderId");
                        });
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductAgg.Product", b =>
                {
                    b.HasOne("ShopManagement.Domain.ProductCategoryAgg.ProductCategory", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ShopManagement.Domain.ProductAgg.ProductPageMetas", "Metas", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime>("CreationDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Keywords")
                                .HasColumnType("nvarchar(80)")
                                .HasMaxLength(80);

                            b1.Property<string>("MetaDescription")
                                .HasColumnType("nvarchar(150)")
                                .HasMaxLength(150);

                            b1.Property<int>("ProductId")
                                .HasColumnType("int");

                            b1.Property<string>("Slug")
                                .HasColumnType("nvarchar(300)")
                                .HasMaxLength(300);

                            b1.HasKey("Id");

                            b1.HasIndex("ProductId")
                                .IsUnique();

                            b1.ToTable("ProductsPageMetas");

                            b1.WithOwner("Product")
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("ShopManagement.Domain.ProductAgg.ProductPicture", "Picture", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Alt")
                                .HasColumnType("nvarchar(255)")
                                .HasMaxLength(255);

                            b1.Property<DateTime>("CreationDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Path")
                                .HasColumnType("nvarchar(100)")
                                .HasMaxLength(100);

                            b1.Property<int>("ProductId")
                                .HasColumnType("int");

                            b1.Property<string>("Title")
                                .HasColumnType("nvarchar(500)")
                                .HasMaxLength(500);

                            b1.HasKey("Id");

                            b1.HasIndex("ProductId")
                                .IsUnique();

                            b1.ToTable("ProductsPictures");

                            b1.WithOwner("Product")
                                .HasForeignKey("ProductId");
                        });
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductCategoryAgg.ProductCategory", b =>
                {
                    b.OwnsOne("ShopManagement.Domain.ProductCategoryAgg.ProductCategoryPageMetas", "Metas", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime>("CreationDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Keywords")
                                .HasColumnType("nvarchar(80)")
                                .HasMaxLength(80);

                            b1.Property<string>("MetaDescription")
                                .HasColumnType("nvarchar(150)")
                                .HasMaxLength(150);

                            b1.Property<int>("ProductCategoryID")
                                .HasColumnType("int");

                            b1.Property<string>("Slug")
                                .HasColumnType("nvarchar(300)")
                                .HasMaxLength(300);

                            b1.HasKey("Id");

                            b1.HasIndex("ProductCategoryID")
                                .IsUnique();

                            b1.ToTable("ProductCategoriesPageMetas");

                            b1.WithOwner("ProductCategory")
                                .HasForeignKey("ProductCategoryID");
                        });

                    b.OwnsOne("ShopManagement.Domain.ProductCategoryAgg.ProductCategoryPicture", "Picture", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Alt")
                                .HasColumnType("nvarchar(255)")
                                .HasMaxLength(255);

                            b1.Property<DateTime>("CreationDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Path")
                                .HasColumnType("nvarchar(100)")
                                .HasMaxLength(100);

                            b1.Property<int>("ProductCategoryId")
                                .HasColumnType("int");

                            b1.Property<string>("Title")
                                .HasColumnType("nvarchar(500)")
                                .HasMaxLength(500);

                            b1.HasKey("Id");

                            b1.HasIndex("ProductCategoryId")
                                .IsUnique();

                            b1.ToTable("ProductCategoriesPictures");

                            b1.WithOwner("ProductCategory")
                                .HasForeignKey("ProductCategoryId");
                        });
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductPictureSliderAgg.ProductPictureSlider", b =>
                {
                    b.HasOne("ShopManagement.Domain.ProductAgg.Product", "Product")
                        .WithMany("ProductPicturesSlider")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
