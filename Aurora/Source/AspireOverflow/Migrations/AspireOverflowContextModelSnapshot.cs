﻿// <auto-generated />
using System;
using AspireOverflow.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AspireOverflow.Migrations
{
    [DbContext(typeof(AspireOverflowContext))]
    partial class AspireOverflowContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AspireOverflow.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"), 1L, 1);

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("AspireOverflow.Models.Designation", b =>
                {
                    b.Property<int>("DesignationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DesignationId"), 1L, 1);

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("DesignationName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DesignationId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Designations");
                });

            modelBuilder.Entity("AspireOverflow.Models.Gender", b =>
                {
                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("GenderId");

                    b.ToTable("Gender");

                    b.HasData(
                        new
                        {
                            GenderId = 1,
                            Name = "Male"
                        },
                        new
                        {
                            GenderId = 2,
                            Name = "Female"
                        });
                });

            modelBuilder.Entity("AspireOverflow.Models.Query", b =>
                {
                    b.Property<int>("QueryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QueryId"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSolved")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QueryId");

                    b.HasIndex("CreatedBy");

                    b.ToTable("Queries");
                });

            modelBuilder.Entity("AspireOverflow.Models.QueryComment", b =>
                {
                    b.Property<int>("QueryCommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QueryCommentId"), 1L, 1);

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("QueryId")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("QueryCommentId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("QueryId");

                    b.ToTable("QueryComments");
                });

            modelBuilder.Entity("AspireOverflow.Models.Spam", b =>
                {
                    b.Property<int>("SpamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpamId"), 1L, 1);

                    b.Property<int>("QueryId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("VerifyStatusID")
                        .HasColumnType("int");

                    b.HasKey("SpamId");

                    b.HasIndex("QueryId");

                    b.HasIndex("UserId");

                    b.HasIndex("VerifyStatusID");

                    b.ToTable("Spams");
                });

            modelBuilder.Entity("AspireOverflow.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("AceNumber")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int>("DesignationId")
                        .HasColumnType("int");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<bool>("IsReviewer")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("int");

                    b.Property<int>("VerifyStatusID")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("DesignationId");

                    b.HasIndex("GenderId");

                    b.HasIndex("UserRoleId");

                    b.HasIndex("VerifyStatusID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AspireOverflow.Models.UserRole", b =>
                {
                    b.Property<int>("UserRoleId")
                        .HasColumnType("int");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserRoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserRoleId = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            UserRoleId = 2,
                            RoleName = "User"
                        });
                });

            modelBuilder.Entity("AspireOverflow.Models.VerifyStatus", b =>
                {
                    b.Property<int>("VerifyStatusID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VerifyStatusID");

                    b.ToTable("VerifyStatus");

                    b.HasData(
                        new
                        {
                            VerifyStatusID = 1,
                            Name = "Approved"
                        },
                        new
                        {
                            VerifyStatusID = 2,
                            Name = "Rejected"
                        },
                        new
                        {
                            VerifyStatusID = 3,
                            Name = "NotVerified"
                        });
                });

            modelBuilder.Entity("AspireOverflow.Models.Designation", b =>
                {
                    b.HasOne("AspireOverflow.Models.Department", "Department")
                        .WithMany("Designations")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("AspireOverflow.Models.Query", b =>
                {
                    b.HasOne("AspireOverflow.Models.User", "User")
                        .WithMany("Queries")
                        .HasForeignKey("CreatedBy")
                        .IsRequired()
                        .HasConstraintName("FK_Query_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AspireOverflow.Models.QueryComment", b =>
                {
                    b.HasOne("AspireOverflow.Models.User", "User")
                        .WithMany("QueryComments")
                        .HasForeignKey("CreatedBy")
                        .IsRequired()
                        .HasConstraintName("FK_QueryComment_User");

                    b.HasOne("AspireOverflow.Models.Query", "Query")
                        .WithMany("QueryComments")
                        .HasForeignKey("QueryId")
                        .IsRequired()
                        .HasConstraintName("FK_QueryComment_Query");

                    b.Navigation("Query");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AspireOverflow.Models.Spam", b =>
                {
                    b.HasOne("AspireOverflow.Models.Query", "Query")
                        .WithMany()
                        .HasForeignKey("QueryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AspireOverflow.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AspireOverflow.Models.VerifyStatus", "VerifyStatus")
                        .WithMany()
                        .HasForeignKey("VerifyStatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Query");

                    b.Navigation("User");

                    b.Navigation("VerifyStatus");
                });

            modelBuilder.Entity("AspireOverflow.Models.User", b =>
                {
                    b.HasOne("AspireOverflow.Models.Designation", "Designation")
                        .WithMany("Users")
                        .HasForeignKey("DesignationId")
                        .IsRequired()
                        .HasConstraintName("FK_User_Department");

                    b.HasOne("AspireOverflow.Models.Gender", "Gender")
                        .WithMany("Users")
                        .HasForeignKey("GenderId")
                        .IsRequired()
                        .HasConstraintName("FK_User_Gender");

                    b.HasOne("AspireOverflow.Models.UserRole", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("UserRoleId")
                        .IsRequired()
                        .HasConstraintName("FK_User_UserRole");

                    b.HasOne("AspireOverflow.Models.VerifyStatus", "VerifyStatus")
                        .WithMany("Users")
                        .HasForeignKey("VerifyStatusID")
                        .IsRequired()
                        .HasConstraintName("FK_User_VerifyStatus");

                    b.Navigation("Designation");

                    b.Navigation("Gender");

                    b.Navigation("UserRole");

                    b.Navigation("VerifyStatus");
                });

            modelBuilder.Entity("AspireOverflow.Models.Department", b =>
                {
                    b.Navigation("Designations");
                });

            modelBuilder.Entity("AspireOverflow.Models.Designation", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("AspireOverflow.Models.Gender", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("AspireOverflow.Models.Query", b =>
                {
                    b.Navigation("QueryComments");
                });

            modelBuilder.Entity("AspireOverflow.Models.User", b =>
                {
                    b.Navigation("Queries");

                    b.Navigation("QueryComments");
                });

            modelBuilder.Entity("AspireOverflow.Models.UserRole", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("AspireOverflow.Models.VerifyStatus", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
