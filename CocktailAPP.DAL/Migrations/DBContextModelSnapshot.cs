﻿// <auto-generated />
using CocktailAPP.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CocktailAPP.DAL.Migrations
{
    [DbContext(typeof(CocktailAPP.DAL.DBContext.DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CocktailAPP.Entity.Favoritos", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("idDrink")
                        .HasColumnType("int");

                    b.Property<string>("strDrink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Favoritos");
                });
#pragma warning restore 612, 618
        }
    }
}
