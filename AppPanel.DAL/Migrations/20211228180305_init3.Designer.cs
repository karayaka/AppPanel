﻿// <auto-generated />
using System;
using AppPanel.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppPanel.DAL.Migrations
{
    [DbContext(typeof(ServiceContext))]
    [Migration("20211228180305_init3")]
    partial class init3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("AppPanel.DAL.Classes.AdminClasses.AdminUser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<byte>("ObjectStatus")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.Property<string>("UserImage")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("AdminUsers");
                });

            modelBuilder.Entity("AppPanel.DAL.Classes.AppsClasses.AppCardColor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Desc")
                        .HasColumnType("text");

                    b.Property<int>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<byte>("ObjectStatus")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int>("PanelAppID")
                        .HasColumnType("int");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint unsigned");

                    b.HasKey("ID");

                    b.HasIndex("PanelAppID");

                    b.ToTable("AppCardColors");
                });

            modelBuilder.Entity("AppPanel.DAL.Classes.AppsClasses.AppImages", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("ImageName")
                        .HasColumnType("text");

                    b.Property<string>("ImageUr")
                        .HasColumnType("text");

                    b.Property<int>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<byte>("ObjectStatus")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int>("PanelAppID")
                        .HasColumnType("int");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint unsigned");

                    b.HasKey("ID");

                    b.HasIndex("PanelAppID");

                    b.ToTable("AppImages");
                });

            modelBuilder.Entity("AppPanel.DAL.Classes.AppsClasses.PanelApp", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AppDesc")
                        .HasColumnType("text");

                    b.Property<string>("AppKey")
                        .HasColumnType("text");

                    b.Property<string>("AppName")
                        .HasColumnType("text");

                    b.Property<string>("AppSubDesc")
                        .HasColumnType("text");

                    b.Property<string>("AppUrl")
                        .HasColumnType("text");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("GitHubUrl")
                        .HasColumnType("text");

                    b.Property<int>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<byte>("ObjectStatus")
                        .HasColumnType("tinyint unsigned");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint unsigned");

                    b.HasKey("ID");

                    b.ToTable("PanelApps");
                });

            modelBuilder.Entity("AppPanel.DAL.Classes.EnglishQuizerClasses.Level", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<int>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("LevelDesc")
                        .HasColumnType("text");

                    b.Property<string>("LevelName")
                        .HasColumnType("text");

                    b.Property<byte>("ObjectStatus")
                        .HasColumnType("tinyint unsigned");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint unsigned");

                    b.HasKey("ID");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("AppPanel.DAL.Classes.EnglishQuizerClasses.Question", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AnsverA")
                        .HasColumnType("text");

                    b.Property<string>("AnsverB")
                        .HasColumnType("text");

                    b.Property<string>("AnsverC")
                        .HasColumnType("text");

                    b.Property<string>("AnsverD")
                        .HasColumnType("text");

                    b.Property<string>("AnswerDesc")
                        .HasColumnType("text");

                    b.Property<int>("CorrectAnswer")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<int>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<byte>("ObjectStatus")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("QuestionDesc")
                        .HasColumnType("text");

                    b.Property<int>("QuestionNumber")
                        .HasColumnType("int");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int>("TestID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TestID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("AppPanel.DAL.Classes.EnglishQuizerClasses.Test", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte>("AdsStatus")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<int>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<byte>("ObjectStatus")
                        .HasColumnType("tinyint unsigned");

                    b.Property<bool>("ShowTestStartDesc")
                        .HasColumnType("tinyint(1)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("TestDesc")
                        .HasColumnType("text");

                    b.Property<string>("TestName")
                        .HasColumnType("text");

                    b.Property<string>("TestStartDesc")
                        .HasColumnType("text");

                    b.Property<int>("TestStatus")
                        .HasColumnType("int");

                    b.Property<int>("TopicID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TopicID");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("AppPanel.DAL.Classes.EnglishQuizerClasses.Topic", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<int>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<int>("LevelID")
                        .HasColumnType("int");

                    b.Property<byte>("ObjectStatus")
                        .HasColumnType("tinyint unsigned");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("TopicDesc")
                        .HasColumnType("text");

                    b.Property<string>("TopicName")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("LevelID");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("AppPanel.DAL.Classes.AppsClasses.AppCardColor", b =>
                {
                    b.HasOne("AppPanel.DAL.Classes.AppsClasses.PanelApp", "PanelApp")
                        .WithMany("AppCardColors")
                        .HasForeignKey("PanelAppID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PanelApp");
                });

            modelBuilder.Entity("AppPanel.DAL.Classes.AppsClasses.AppImages", b =>
                {
                    b.HasOne("AppPanel.DAL.Classes.AppsClasses.PanelApp", "PanelApp")
                        .WithMany("AppImages")
                        .HasForeignKey("PanelAppID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PanelApp");
                });

            modelBuilder.Entity("AppPanel.DAL.Classes.EnglishQuizerClasses.Question", b =>
                {
                    b.HasOne("AppPanel.DAL.Classes.EnglishQuizerClasses.Test", "Test")
                        .WithMany("Questions")
                        .HasForeignKey("TestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("AppPanel.DAL.Classes.EnglishQuizerClasses.Test", b =>
                {
                    b.HasOne("AppPanel.DAL.Classes.EnglishQuizerClasses.Topic", "Topic")
                        .WithMany("Tests")
                        .HasForeignKey("TopicID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("AppPanel.DAL.Classes.EnglishQuizerClasses.Topic", b =>
                {
                    b.HasOne("AppPanel.DAL.Classes.EnglishQuizerClasses.Level", "Level")
                        .WithMany("Topics")
                        .HasForeignKey("LevelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Level");
                });

            modelBuilder.Entity("AppPanel.DAL.Classes.AppsClasses.PanelApp", b =>
                {
                    b.Navigation("AppCardColors");

                    b.Navigation("AppImages");
                });

            modelBuilder.Entity("AppPanel.DAL.Classes.EnglishQuizerClasses.Level", b =>
                {
                    b.Navigation("Topics");
                });

            modelBuilder.Entity("AppPanel.DAL.Classes.EnglishQuizerClasses.Test", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("AppPanel.DAL.Classes.EnglishQuizerClasses.Topic", b =>
                {
                    b.Navigation("Tests");
                });
#pragma warning restore 612, 618
        }
    }
}
