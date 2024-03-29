﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolApi.Models;

#nullable disable

namespace SchoolApi.Migrations
{
    [DbContext(typeof(StudentContext))]
    partial class StudentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SchoolApi.Models.Professor", b =>
                {
                    b.Property<int>("ProfId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProfId"), 1L, 1);

                    b.Property<string>("ProfLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProfId");

                    b.ToTable("Professors", (string)null);
                });

            modelBuilder.Entity("SchoolApi.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProfessorProfId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfessorProfId");

                    b.ToTable("Students", (string)null);
                });

            modelBuilder.Entity("SchoolApi.Models.Subjects", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectId"), 1L, 1);

                    b.Property<int>("HoursToComplete")
                        .HasColumnType("int");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("SubjectName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubjectId");

                    b.HasIndex("StudentId");

                    b.ToTable("Subjects", (string)null);
                });

            modelBuilder.Entity("SchoolApi.Models.Student", b =>
                {
                    b.HasOne("SchoolApi.Models.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorProfId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("SchoolApi.Models.Subjects", b =>
                {
                    b.HasOne("SchoolApi.Models.Student", null)
                        .WithMany("Subjects")
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("SchoolApi.Models.Student", b =>
                {
                    b.Navigation("Subjects");
                });
#pragma warning restore 612, 618
        }
    }
}
