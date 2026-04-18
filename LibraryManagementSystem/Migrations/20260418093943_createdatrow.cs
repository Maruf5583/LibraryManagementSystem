using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class createdatrow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Author__3214EC0753AF0893", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Genre__3214EC076AFCCFAF", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Salt = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Member__3214EC07D59BC419", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__3214EC075F1FFD60", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YearlyFee = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    BookingCount = table.Column<int>(type: "int", nullable: true),
                    BookingTime = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subscrip__3214EC07962F81D5", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ShortDesc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GenreId = table.Column<int>(type: "int", nullable: true),
                    Publication = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PublishedYear = table.Column<int>(type: "int", nullable: false),
                    Pages = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Width = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Book__3214EC07BE16D460", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Book__GenreId__4CA06362",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MemberRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MemberRo__3214EC07A3AE7948", x => x.Id);
                    table.ForeignKey(
                        name: "FK__MemberRol__Membe__5EBF139D",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__MemberRol__RoleI__5FB337D6",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MemberSubscription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    SubscriptionId = table.Column<int>(type: "int", nullable: true),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: true),
                    AmountPaid = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    PaymentMode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PayDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MemberSu__3214EC0782C38811", x => x.Id);
                    table.ForeignKey(
                        name: "FK__MemberSub__Membe__628FA481",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__MemberSub__Subsc__6383C8BA",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscription",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookAuthor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: true),
                    AuthorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookAuth__3214EC072FB208F3", x => x.Id);
                    table.ForeignKey(
                        name: "FK__BookAutho__Autho__52593CB8",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__BookAutho__BookI__5165187F",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookCopy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    Condition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookCopy__3214EC07A51FBECC", x => x.Id);
                    table.ForeignKey(
                        name: "FK__BookCopy__BookId__6754599E",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    BookId = table.Column<int>(type: "int", nullable: true),
                    Ratings = table.Column<int>(type: "int", nullable: true),
                    IsRecommended = table.Column<bool>(type: "bit", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    ReviewedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookRevi__3214EC070C56E857", x => x.Id);
                    table.ForeignKey(
                        name: "FK__BookRevie__BookI__71D1E811",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__BookRevie__Membe__70DDC3D8",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookReservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    BookCopyId = table.Column<int>(type: "int", nullable: true),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IsComplete = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    IsViolation = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    ViolationReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ViolationRemarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookRese__3214EC0785EB59BE", x => x.Id);
                    table.ForeignKey(
                        name: "FK__BookReser__BookC__6D0D32F4",
                        column: x => x.BookCopyId,
                        principalTable: "BookCopy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__BookReser__Membe__6C190EBB",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_GenreId",
                table: "Book",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_AuthorId",
                table: "BookAuthor",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_BookId",
                table: "BookAuthor",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCopy_BookId",
                table: "BookCopy",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReservation_BookCopyId",
                table: "BookReservation",
                column: "BookCopyId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReservation_MemberId",
                table: "BookReservation",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReview_BookId",
                table: "BookReview",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReview_MemberId",
                table: "BookReview",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "UQ__Member__A9D10534159AF88B",
                table: "Member",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MemberRole_MemberId",
                table: "MemberRole",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberRole_RoleId",
                table: "MemberRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberSubscription_MemberId",
                table: "MemberSubscription",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberSubscription_SubscriptionId",
                table: "MemberSubscription",
                column: "SubscriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthor");

            migrationBuilder.DropTable(
                name: "BookReservation");

            migrationBuilder.DropTable(
                name: "BookReview");

            migrationBuilder.DropTable(
                name: "MemberRole");

            migrationBuilder.DropTable(
                name: "MemberSubscription");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "BookCopy");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Genre");
        }
    }
}
