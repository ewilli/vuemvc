using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewsId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.NewsId);
                });

            migrationBuilder.CreateTable(
                name: "Source",
                columns: table => new
                {
                    SourceId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Source", x => x.SourceId);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NewsId = table.Column<int>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PublishedAt = table.Column<string>(nullable: true),
                    SourceId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    UrlToImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Articles_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "NewsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Source_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Source",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "NewsId", "Status" },
                values: new object[] { 1, "AKTUELL" });

            migrationBuilder.InsertData(
                table: "Source",
                columns: new[] { "SourceId", "Name" },
                values: new object[] { 1, "Test1" });

            migrationBuilder.InsertData(
                table: "Source",
                columns: new[] { "SourceId", "Name" },
                values: new object[] { 2, "Test2" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "ArticleId", "Author", "Content", "Description", "NewsId", "PublishedAt", "SourceId", "Title", "Url", "UrlToImage" },
                values: new object[] { 2, "Musterfrau", "Irure voluptate ullamco elit do irure sunt enim cillum.", "Do aliquip voluptate irure labore culpa Lorem esse deserunt ullamco.", 1, "Mawa", 1, "Amet ex pariatur nostrud minim consectetur fugiat labore laborum ullamco exercitation magna ut ut aliqua.", "http://www.google.at", "" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "ArticleId", "Author", "Content", "Description", "NewsId", "PublishedAt", "SourceId", "Title", "Url", "UrlToImage" },
                values: new object[] { 1, "Mustermann", "Laborum veniam dolore esse commodo.", "Labore eu reprehenderit laboris sint mollit elit officia.", 1, "Mawa", 2, "Do anim veniam excepteur ex sit aliquip eiusmod fugiat culpa.", "http://www.google.at", "" });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_NewsId",
                table: "Articles",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_SourceId",
                table: "Articles",
                column: "SourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Source");
        }
    }
}
