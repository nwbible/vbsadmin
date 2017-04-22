using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VBSAdmin.Migrations
{
    public partial class formstackimportsupport2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormStackFormName",
                table: "VBS");

            migrationBuilder.DropColumn(
                name: "FormStackLastImportTimestamp",
                table: "VBS");

            migrationBuilder.DropColumn(
                name: "ImportPageKey",
                table: "VBS");

            migrationBuilder.AddColumn<int>(
                name: "FormStackFormId",
                table: "VBS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStackImportPageKey",
                table: "VBS",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FormStackLastImportDateTime",
                table: "VBS",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormStackFormId",
                table: "VBS");

            migrationBuilder.DropColumn(
                name: "FormStackImportPageKey",
                table: "VBS");

            migrationBuilder.DropColumn(
                name: "FormStackLastImportDateTime",
                table: "VBS");

            migrationBuilder.AddColumn<string>(
                name: "FormStackFormName",
                table: "VBS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStackLastImportTimestamp",
                table: "VBS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImportPageKey",
                table: "VBS",
                nullable: true);
        }
    }
}
