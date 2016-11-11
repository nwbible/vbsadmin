using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VBSAdmin.Migrations
{
    public partial class vbsadmin2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ClassroomId",
                table: "Children",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ClassroomId",
                table: "Children",
                nullable: false);
        }
    }
}
