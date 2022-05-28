﻿// <auto-generated />
using System;
using AspireOverflow.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AspireOverflow.Migrations
{
    [DbContext(typeof(AspireOverflowContext))]
    [Migration("20220528043920_CreatedPrivateArticleTable")]
    partial class CreatedPrivateArticleTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AspireOverflow.Models.Article", b =>
                {
                    b.Property<int>("ArtileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArtileId"), 1L, 1);

                    b.Property<int>("ArticleStatusID")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("bit");

                    b.Property<int?>("ReviewerId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ArtileId");

                    b.HasIndex("ArticleStatusID");

                    b.HasIndex("CreatedBy");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("AspireOverflow.Models.ArticleComment", b =>
                {
                    b.Property<int>("ArticleCommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArticleCommentId"), 1L, 1);

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ArticleCommentId");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("ArticleComments");
                });

            modelBuilder.Entity("AspireOverflow.Models.ArticleLike", b =>
                {
                    b.Property<int>("LikeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LikeId"), 1L, 1);

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LikeId");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("ArticleLikes");
                });

            modelBuilder.Entity("AspireOverflow.Models.ArticleStatus", b =>
                {
                    b.Property<int>("ArticleStatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArticleStatusID"), 1L, 1);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArticleStatusID");

                    b.ToTable("ArticleStatus");

                    b.HasData(
                        new
                        {
                            ArticleStatusID = 1,
                            Status = "InDraft"
                        },
                        new
                        {
                            ArticleStatusID = 2,
                            Status = "To Be Reviewed"
                        },
                        new
                        {
                            ArticleStatusID = 3,
                            Status = "Under Review"
                        },
                        new
                        {
                            ArticleStatusID = 4,
                            Status = "Published"
                        });
                });

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

            modelBuilder.Entity("AspireOverflow.Models.PrivateArticle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("PrivateArticles");
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

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("AspireOverflow.Models.Article", b =>
                {
                    b.HasOne("AspireOverflow.Models.ArticleStatus", "ArticleStatus")
                        .WithMany("Articles")
                        .HasForeignKey("ArticleStatusID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AspireOverflow.Models.User", "User")
                        .WithMany("Articles")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ArticleStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AspireOverflow.Models.ArticleComment", b =>
                {
                    b.HasOne("AspireOverflow.Models.Article", "Article")
                        .WithMany("ArticleComments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AspireOverflow.Models.User", "User")
                        .WithMany("ArticleComments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AspireOverflow.Models.ArticleLike", b =>
                {
                    b.HasOne("AspireOverflow.Models.Article", "Article")
                        .WithMany("ArticleLikes")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AspireOverflow.Models.User", "LikedUser")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("LikedUser");
                });

            modelBuilder.Entity("AspireOverflow.Models.Designation", b =>
                {
                    b.HasOne("AspireOverflow.Models.Department", "Department")
                        .WithMany("Designations")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("AspireOverflow.Models.PrivateArticle", b =>
                {
                    b.HasOne("AspireOverflow.Models.Article", "article")
                        .WithMany("PrivateArticles")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AspireOverflow.Models.User", "user")
                        .WithMany("PrivateArticles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("article");

                    b.Navigation("user");
                });

            modelBuilder.Entity("AspireOverflow.Models.Query", b =>
                {
                    b.HasOne("AspireOverflow.Models.User", "User")
                        .WithMany("Queries")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AspireOverflow.Models.QueryComment", b =>
                {
                    b.HasOne("AspireOverflow.Models.User", "User")
                        .WithMany("QueryComments")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AspireOverflow.Models.Query", "Query")
                        .WithMany("QueryComments")
                        .HasForeignKey("QueryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Query");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AspireOverflow.Models.Spam", b =>
                {
                    b.HasOne("AspireOverflow.Models.Query", "Query")
                        .WithMany()
                        .HasForeignKey("QueryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AspireOverflow.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AspireOverflow.Models.VerifyStatus", "VerifyStatus")
                        .WithMany()
                        .HasForeignKey("VerifyStatusID")
                        .OnDelete(DeleteBehavior.NoAction)
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
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AspireOverflow.Models.Gender", "Gender")
                        .WithMany("Users")
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AspireOverflow.Models.UserRole", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AspireOverflow.Models.VerifyStatus", "VerifyStatus")
                        .WithMany("Users")
                        .HasForeignKey("VerifyStatusID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Designation");

                    b.Navigation("Gender");

                    b.Navigation("UserRole");

                    b.Navigation("VerifyStatus");
                });

            modelBuilder.Entity("AspireOverflow.Models.Article", b =>
                {
                    b.Navigation("ArticleComments");

                    b.Navigation("ArticleLikes");

                    b.Navigation("PrivateArticles");
                });

            modelBuilder.Entity("AspireOverflow.Models.ArticleStatus", b =>
                {
                    b.Navigation("Articles");
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
                    b.Navigation("ArticleComments");

                    b.Navigation("Articles");

                    b.Navigation("Likes");

                    b.Navigation("PrivateArticles");

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
