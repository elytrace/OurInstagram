using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Pinsta.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    dateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    gender = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    avatarPath = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    biography = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    displayedName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "follows",
                columns: table => new
                {
                    followersuserId = table.Column<int>(type: "int", nullable: false),
                    followingsuserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_follows", x => new { x.followersuserId, x.followingsuserId });
                    table.ForeignKey(
                        name: "FK_follows_users_followersuserId",
                        column: x => x.followersuserId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_follows_users_followingsuserId",
                        column: x => x.followingsuserId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    imageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    imagePath = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    caption = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    uploadTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.imageId);
                    table.ForeignKey(
                        name: "FK_images_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "searchs",
                columns: table => new
                {
                    searchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(type: "int", nullable: false),
                    resultId = table.Column<int>(type: "int", nullable: false),
                    timeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_searchs", x => x.searchId);
                    table.ForeignKey(
                        name: "FK_searchs_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    commentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    comment = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    userId = table.Column<int>(type: "int", nullable: false),
                    imageId = table.Column<int>(type: "int", nullable: false),
                    timeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.commentId);
                    table.ForeignKey(
                        name: "FK_comments_images_imageId",
                        column: x => x.imageId,
                        principalTable: "images",
                        principalColumn: "imageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comments_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "likes",
                columns: table => new
                {
                    likeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(type: "int", nullable: false),
                    imageId = table.Column<int>(type: "int", nullable: false),
                    timeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_likes", x => x.likeId);
                    table.ForeignKey(
                        name: "FK_likes_images_imageId",
                        column: x => x.imageId,
                        principalTable: "images",
                        principalColumn: "imageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_likes_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_comments_imageId",
                table: "comments",
                column: "imageId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_userId",
                table: "comments",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_follows_followingsuserId",
                table: "follows",
                column: "followingsuserId");

            migrationBuilder.CreateIndex(
                name: "IX_images_userId",
                table: "images",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_likes_imageId",
                table: "likes",
                column: "imageId");

            migrationBuilder.CreateIndex(
                name: "IX_likes_userId",
                table: "likes",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_searchs_userId",
                table: "searchs",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "follows");

            migrationBuilder.DropTable(
                name: "likes");

            migrationBuilder.DropTable(
                name: "searchs");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
