// <auto-generated />
using System;
using DataAggregator.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAggregator.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220227191026_Migration_1")]
    partial class Migration_1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataAggregator.Entities.FilterEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StreamEntityId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StreamEntityId");

                    b.ToTable("Filters");
                });

            modelBuilder.Entity("DataAggregator.Entities.SourceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StreamEntityId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StreamEntityId");

                    b.ToTable("Sources");
                });

            modelBuilder.Entity("DataAggregator.Entities.StreamEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Streams");
                });

            modelBuilder.Entity("DataAggregator.Entities.FilterEntity", b =>
                {
                    b.HasOne("DataAggregator.Entities.StreamEntity", null)
                        .WithMany("Filters")
                        .HasForeignKey("StreamEntityId");
                });

            modelBuilder.Entity("DataAggregator.Entities.SourceEntity", b =>
                {
                    b.HasOne("DataAggregator.Entities.StreamEntity", null)
                        .WithMany("Sources")
                        .HasForeignKey("StreamEntityId");
                });

            modelBuilder.Entity("DataAggregator.Entities.StreamEntity", b =>
                {
                    b.Navigation("Filters");

                    b.Navigation("Sources");
                });
#pragma warning restore 612, 618
        }
    }
}
