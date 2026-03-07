using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VBSAdmin.Migrations
{
    public partial class AddChildPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Children",
                maxLength: 64,
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Children");
        }
    }
}