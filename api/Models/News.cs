using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace api
{
    public class NewsContext : DbContext
    {

        public NewsContext(DbContextOptions<NewsContext> options) : base(options)
        { } // Notwendig für DI -> Überschreibt dann im Betrieb die Connection

        public DbSet<News> News { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Source> Source { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=News.db"); // Legt die InitDB an
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>().HasData(new News() { NewsId = 1, Status = "AKTUELL" });
            modelBuilder.Entity<Article>().HasData(
                new Article()
                {
                    NewsId = 1,
                    ArticleId = 1,
                    Author = "Mustermann",
                    Content = "Laborum veniam dolore esse commodo.",
                    Description = "Labore eu reprehenderit laboris sint mollit elit officia.",
                    PublishedAt = "Mawa",
                    Title = "Do anim veniam excepteur ex sit aliquip eiusmod fugiat culpa.",
                    Url = "http://www.google.at",
                    UrlToImage = "",
                    SourceId = 2
                },
                new Article()
                {
                    NewsId = 1,
                    ArticleId = 2,
                    Author = "Musterfrau",
                    Content = "Irure voluptate ullamco elit do irure sunt enim cillum.",
                    Description = "Do aliquip voluptate irure labore culpa Lorem esse deserunt ullamco.",
                    PublishedAt = "Mawa",
                    Title = "Amet ex pariatur nostrud minim consectetur fugiat labore laborum ullamco exercitation magna ut ut aliqua.",
                    Url = "http://www.google.at",
                    UrlToImage = "",
                    SourceId = 1
                }
                );
            modelBuilder.Entity<Source>().HasData(
                new Source { SourceId = 1, Name = "Test1" },
                new Source { SourceId = 2, Name = "Test2" }
            );
        }
    }

    public partial class News
    {
        [JsonProperty("id")]
        public int NewsId { get; set; }

        [JsonProperty("articles")]
        public ICollection<Article> Articles { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("totalResults")]
        public int TotalResults { get; }
    }

    public partial class Article
    {
        [JsonProperty("id")]
        public int ArticleId { get; set; }

        [JsonIgnore]
        public int NewsId { get; set; }

        [JsonProperty("author", NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }

        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("publishedAt")]
        public string PublishedAt { get; set; }

        [JsonIgnore]
        public int SourceId { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("urlToImage", NullValueHandling = NullValueHandling.Ignore)]
        public string UrlToImage { get; set; }

        [JsonIgnore]
        public News News { get; set; }

    }

    public partial class Source
    {
        [JsonIgnore]
        public int SourceId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

    }

}