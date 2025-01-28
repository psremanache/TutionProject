﻿// <auto-generated />
using System;
using EntityFrameworkCore.DBConnection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EntityFrameworkCore.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250126064514_addedUniqueConstraints")]
    partial class addedUniqueConstraints
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EntityFrameworkCore.Entities.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CourseId");

                    b.HasIndex("InstructorId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            CourseId = 1,
                            InstructorId = 1,
                            Title = "Mathematics 101"
                        },
                        new
                        {
                            CourseId = 2,
                            InstructorId = 2,
                            Title = "Physics 101"
                        },
                        new
                        {
                            CourseId = 3,
                            InstructorId = 3,
                            Title = "Chemistry 101"
                        },
                        new
                        {
                            CourseId = 4,
                            InstructorId = 4,
                            Title = "Biology 101"
                        });
                });

            modelBuilder.Entity("EntityFrameworkCore.Entities.Enrollment", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime2");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("Enrollments");

                    b.HasData(
                        new
                        {
                            StudentId = 1,
                            CourseId = 1,
                            EnrollmentDate = new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            StudentId = 2,
                            CourseId = 2,
                            EnrollmentDate = new DateTime(2023, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            StudentId = 3,
                            CourseId = 3,
                            EnrollmentDate = new DateTime(2023, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            StudentId = 4,
                            CourseId = 4,
                            EnrollmentDate = new DateTime(2023, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("EntityFrameworkCore.Entities.Instructor", b =>
                {
                    b.Property<int>("InstructorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstructorId"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("InstructorId");

                    b.HasIndex("FullName")
                        .IsUnique();

                    b.ToTable("Instructors");

                    b.HasData(
                        new
                        {
                            InstructorId = 1,
                            FullName = "Dr. John Smith"
                        },
                        new
                        {
                            InstructorId = 2,
                            FullName = "Dr. Emily Johnson"
                        },
                        new
                        {
                            InstructorId = 3,
                            FullName = "Prof. Michael Brown"
                        },
                        new
                        {
                            InstructorId = 4,
                            FullName = "Dr. Sarah Davis"
                        });
                });

            modelBuilder.Entity("EntityFrameworkCore.Entities.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StudentId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            StudentId = 1,
                            Name = "Alice Johnson"
                        },
                        new
                        {
                            StudentId = 2,
                            Name = "Bob Smith"
                        },
                        new
                        {
                            StudentId = 3,
                            Name = "Charlie Brown"
                        },
                        new
                        {
                            StudentId = 4,
                            Name = "Diana Ross"
                        });
                });

            modelBuilder.Entity("EntityFrameworkCore.Entities.Course", b =>
                {
                    b.HasOne("EntityFrameworkCore.Entities.Instructor", "Instructor")
                        .WithMany("Courses")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("EntityFrameworkCore.Entities.Enrollment", b =>
                {
                    b.HasOne("EntityFrameworkCore.Entities.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFrameworkCore.Entities.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("EntityFrameworkCore.Entities.Course", b =>
                {
                    b.Navigation("Enrollments");
                });

            modelBuilder.Entity("EntityFrameworkCore.Entities.Instructor", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("EntityFrameworkCore.Entities.Student", b =>
                {
                    b.Navigation("Enrollments");
                });
#pragma warning restore 612, 618
        }
    }
}
