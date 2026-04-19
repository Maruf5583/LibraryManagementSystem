using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Book__GenreId__4CA06362",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK__BookCopy__BookId__6754599E",
                table: "BookCopy");

            migrationBuilder.DropForeignKey(
                name: "FK__BookReser__BookC__6D0D32F4",
                table: "BookReservation");

            migrationBuilder.DropForeignKey(
                name: "FK__BookReser__Membe__6C190EBB",
                table: "BookReservation");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ToDate",
                table: "BookReservation",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "BookReservation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "BookReservation",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LastUpdatedBy",
                table: "BookReservation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsViolation",
                table: "BookReservation",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsComplete",
                table: "BookReservation",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FromDate",
                table: "BookReservation",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookCopyId",
                table: "BookReservation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsAvailable",
                table: "BookCopy",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "BookCopy",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Book__GenreId__4CA06362",
                table: "Book",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__BookCopy__BookId__6754599E",
                table: "BookCopy",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__BookReser__BookC__6D0D32F4",
                table: "BookReservation",
                column: "BookCopyId",
                principalTable: "BookCopy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__BookReser__Membe__6C190EBB",
                table: "BookReservation",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Book__GenreId__4CA06362",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK__BookCopy__BookId__6754599E",
                table: "BookCopy");

            migrationBuilder.DropForeignKey(
                name: "FK__BookReser__BookC__6D0D32F4",
                table: "BookReservation");

            migrationBuilder.DropForeignKey(
                name: "FK__BookReser__Membe__6C190EBB",
                table: "BookReservation");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ToDate",
                table: "BookReservation",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "BookReservation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "BookReservation",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<int>(
                name: "LastUpdatedBy",
                table: "BookReservation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsViolation",
                table: "BookReservation",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsComplete",
                table: "BookReservation",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FromDate",
                table: "BookReservation",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "BookCopyId",
                table: "BookReservation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAvailable",
                table: "BookCopy",
                type: "bit",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "BookCopy",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "Book",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK__Book__GenreId__4CA06362",
                table: "Book",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__BookCopy__BookId__6754599E",
                table: "BookCopy",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__BookReser__BookC__6D0D32F4",
                table: "BookReservation",
                column: "BookCopyId",
                principalTable: "BookCopy",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__BookReser__Membe__6C190EBB",
                table: "BookReservation",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id");
        }
    }
}
