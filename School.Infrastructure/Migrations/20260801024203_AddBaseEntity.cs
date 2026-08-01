using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Teachers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Teachers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Teachers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Subjects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Subjects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Subjects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Subjects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Students",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Grades",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Grades",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Grades",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Grades",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ClassRooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "ClassRooms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ClassRooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "ClassRooms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Attendances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Attendances",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Attendances",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Attendances",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ClassRooms");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "ClassRooms");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ClassRooms");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "ClassRooms");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Attendances");
        }
    }
}
