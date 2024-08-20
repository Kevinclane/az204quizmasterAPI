﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using az204quizmasterAPI.Data;

#nullable disable

namespace az204quizmasterAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("az204quizmasterAPI.Models.Entities.ActiveQA", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("QAId")
                        .HasColumnType("int");

                    b.Property<int>("QuizId")
                        .HasColumnType("int");

                    b.Property<string>("SubmittedAnswers")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("QAId");

                    b.HasIndex("QuizId");

                    b.ToTable("ActiveQAs");
                });

            modelBuilder.Entity("az204quizmasterAPI.Models.Entities.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("IsCorrect")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LeftDisplay")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("QAId")
                        .HasColumnType("int");

                    b.Property<string>("RightDisplay")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("QAId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("az204quizmasterAPI.Models.Entities.QA", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("longtext");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("QuestionType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("QAs");
                });

            modelBuilder.Entity("az204quizmasterAPI.Models.Entities.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("az204quizmasterAPI.Models.Entities.ResourceLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("QAId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("QAId");

                    b.ToTable("ResourceLink");
                });

            modelBuilder.Entity("az204quizmasterAPI.Models.Entities.ActiveQA", b =>
                {
                    b.HasOne("az204quizmasterAPI.Models.Entities.QA", "QA")
                        .WithMany()
                        .HasForeignKey("QAId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("az204quizmasterAPI.Models.Entities.Quiz", "Quiz")
                        .WithMany("ActiveQAs")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QA");

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("az204quizmasterAPI.Models.Entities.Option", b =>
                {
                    b.HasOne("az204quizmasterAPI.Models.Entities.QA", "QA")
                        .WithMany("Options")
                        .HasForeignKey("QAId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QA");
                });

            modelBuilder.Entity("az204quizmasterAPI.Models.Entities.ResourceLink", b =>
                {
                    b.HasOne("az204quizmasterAPI.Models.Entities.QA", "QA")
                        .WithMany("Links")
                        .HasForeignKey("QAId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QA");
                });

            modelBuilder.Entity("az204quizmasterAPI.Models.Entities.QA", b =>
                {
                    b.Navigation("Links");

                    b.Navigation("Options");
                });

            modelBuilder.Entity("az204quizmasterAPI.Models.Entities.Quiz", b =>
                {
                    b.Navigation("ActiveQAs");
                });
#pragma warning restore 612, 618
        }
    }
}
