using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VBSAdmin.Migrations
{
    public partial class formstackimportsupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormStackAPIKey",
                table: "VBS",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormStackAPIKey",
                table: "VBS");

            migrationBuilder.DropColumn(
                name: "FormStackFormName",
                table: "VBS");

            migrationBuilder.DropColumn(
                name: "FormStackLastImportTimestamp",
                table: "VBS");

            migrationBuilder.DropColumn(
                name: "ImportPageKey",
                table: "VBS");
        }
    }
}
