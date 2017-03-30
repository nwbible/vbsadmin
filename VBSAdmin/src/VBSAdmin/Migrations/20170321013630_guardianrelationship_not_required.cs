using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VBSAdmin.Migrations
{
    public partial class guardianrelationship_not_required : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "VBS",
                type: "Date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "VBS",
                type: "Date",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "GuardianChildRelationship",
                table: "Children",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "VBS",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "VBS",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "GuardianChildRelationship",
                table: "Children",
                nullable: false);
        }
    }
}
