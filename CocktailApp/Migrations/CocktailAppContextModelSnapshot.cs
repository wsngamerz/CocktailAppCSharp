﻿// <auto-generated />
using System;
using CocktailApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CocktailApp.Migrations
{
    [DbContext(typeof(CocktailAppContext))]
    partial class CocktailAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CocktailApp.Models.BarItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("IngredientId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("UserId");

                    b.ToTable("BarItems");
                });

            modelBuilder.Entity("CocktailApp.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CocktailApp.Models.Cocktail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Abv")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<int>("GlassType")
                        .HasColumnType("integer");

                    b.Property<string>("LiquidColor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("LiquidOpacity")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("Privacy")
                        .HasColumnType("integer");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Cocktails");
                });

            modelBuilder.Entity("CocktailApp.Models.CocktailIngredient", b =>
                {
                    b.Property<Guid>("CocktailId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IngredientId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.Property<int>("Unit")
                        .HasColumnType("integer");

                    b.HasKey("CocktailId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("CocktailIngredients");
                });

            modelBuilder.Entity("CocktailApp.Models.CocktailInstruction", b =>
                {
                    b.Property<Guid>("CocktailId")
                        .HasColumnType("uuid");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CocktailId", "Position");

                    b.ToTable("CocktailInstructions");
                });

            modelBuilder.Entity("CocktailApp.Models.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Abv")
                        .HasColumnType("numeric");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("CocktailApp.Models.List", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Lists");
                });

            modelBuilder.Entity("CocktailApp.Models.ListItem", b =>
                {
                    b.Property<Guid>("ListId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CocktailId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.HasKey("ListId", "CocktailId");

                    b.HasIndex("CocktailId");

                    b.ToTable("ListItems");
                });

            modelBuilder.Entity("CocktailApp.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccentColor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<string>("ClerkId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("Privacy")
                        .HasColumnType("integer");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClerkId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CocktailApp.Models.UserFavourite", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CocktailId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("UserId", "CocktailId");

                    b.HasIndex("CocktailId");

                    b.ToTable("UserFavourites");
                });

            modelBuilder.Entity("CocktailApp.Models.BarItem", b =>
                {
                    b.HasOne("CocktailApp.Models.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CocktailApp.Models.User", "User")
                        .WithMany("BarItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CocktailApp.Models.Category", b =>
                {
                    b.HasOne("CocktailApp.Models.Category", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("CocktailApp.Models.Cocktail", b =>
                {
                    b.HasOne("CocktailApp.Models.User", "User")
                        .WithMany("Cocktails")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CocktailApp.Models.CocktailIngredient", b =>
                {
                    b.HasOne("CocktailApp.Models.Cocktail", "Cocktail")
                        .WithMany("Ingredients")
                        .HasForeignKey("CocktailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CocktailApp.Models.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cocktail");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("CocktailApp.Models.CocktailInstruction", b =>
                {
                    b.HasOne("CocktailApp.Models.Cocktail", "Cocktail")
                        .WithMany("Instructions")
                        .HasForeignKey("CocktailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cocktail");
                });

            modelBuilder.Entity("CocktailApp.Models.Ingredient", b =>
                {
                    b.HasOne("CocktailApp.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CocktailApp.Models.List", b =>
                {
                    b.HasOne("CocktailApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CocktailApp.Models.ListItem", b =>
                {
                    b.HasOne("CocktailApp.Models.Cocktail", "Cocktail")
                        .WithMany()
                        .HasForeignKey("CocktailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CocktailApp.Models.List", "List")
                        .WithMany("Items")
                        .HasForeignKey("ListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cocktail");

                    b.Navigation("List");
                });

            modelBuilder.Entity("CocktailApp.Models.UserFavourite", b =>
                {
                    b.HasOne("CocktailApp.Models.Cocktail", "Cocktail")
                        .WithMany()
                        .HasForeignKey("CocktailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CocktailApp.Models.User", "User")
                        .WithMany("Favourites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cocktail");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CocktailApp.Models.Cocktail", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Instructions");
                });

            modelBuilder.Entity("CocktailApp.Models.List", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("CocktailApp.Models.User", b =>
                {
                    b.Navigation("BarItems");

                    b.Navigation("Cocktails");

                    b.Navigation("Favourites");
                });
#pragma warning restore 612, 618
        }
    }
}
