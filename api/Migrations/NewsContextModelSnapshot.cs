﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api;

namespace api.Migrations
{
    [DbContext(typeof(NewsContext))]
    partial class NewsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity("api.Article", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<string>("Content");

                    b.Property<string>("Description");

                    b.Property<int>("NewsId");

                    b.Property<string>("PublishedAt");

                    b.Property<int>("SourceId");

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.Property<string>("UrlToImage");

                    b.HasKey("ArticleId");

                    b.HasIndex("NewsId");

                    b.HasIndex("SourceId");

                    b.ToTable("Articles");

                    b.HasData(
                        new
                        {
                            ArticleId = 1,
                            Author = "Mustermann",
                            Content = "Laborum veniam dolore esse commodo.",
                            Description = "Labore eu reprehenderit laboris sint mollit elit officia.",
                            NewsId = 1,
                            PublishedAt = "Mawa",
                            SourceId = 2,
                            Title = "Do anim veniam excepteur ex sit aliquip eiusmod fugiat culpa.",
                            Url = "http://www.google.at",
                            UrlToImage = ""
                        },
                        new
                        {
                            ArticleId = 2,
                            Author = "Musterfrau",
                            Content = "Irure voluptate ullamco elit do irure sunt enim cillum.",
                            Description = "Do aliquip voluptate irure labore culpa Lorem esse deserunt ullamco.",
                            NewsId = 1,
                            PublishedAt = "Mawa",
                            SourceId = 1,
                            Title = "Amet ex pariatur nostrud minim consectetur fugiat labore laborum ullamco exercitation magna ut ut aliqua.",
                            Url = "http://www.google.at",
                            UrlToImage = ""
                        });
                });

            modelBuilder.Entity("api.News", b =>
                {
                    b.Property<int>("NewsId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Status");

                    b.HasKey("NewsId");

                    b.ToTable("News");

                    b.HasData(
                        new
                        {
                            NewsId = 1,
                            Status = "AKTUELL"
                        });
                });

            modelBuilder.Entity("api.Source", b =>
                {
                    b.Property<int>("SourceId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("SourceId");

                    b.ToTable("Source");

                    b.HasData(
                        new
                        {
                            SourceId = 1,
                            Name = "Test1"
                        },
                        new
                        {
                            SourceId = 2,
                            Name = "Test2"
                        });
                });

            modelBuilder.Entity("api.Article", b =>
                {
                    b.HasOne("api.News", "News")
                        .WithMany("Articles")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("api.Source", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
