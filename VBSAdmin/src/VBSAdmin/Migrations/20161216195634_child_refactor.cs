using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VBSAdmin.Migrations
{
    public partial class child_refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_Guardians_GuardianId",
                table: "Children");

            migrationBuilder.DropIndex(
                name: "IX_Children_GuardianId",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "GuardianId",
                table: "Children");

            migrationBuilder.DropTable(
                name: "Guardians");

            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "Children",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "Children",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AttendHostChurch",
                table: "Children",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Children",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfRegistration",
                table: "Children",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactFirstName",
                table: "Children",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactLastName",
                table: "Children",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactPhone",
                table: "Children",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuardianChildRelationship",
                table: "Children",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuardianEmail",
                table: "Children",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuardianFirstName",
                table: "Children",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuardianLastName",
                table: "Children",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuardianPhone",
                table: "Children",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeChurch",
                table: "Children",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvitedBy",
                table: "Children",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Children",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "Children",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address1",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "AttendHostChurch",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "DateOfRegistration",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "EmergencyContactFirstName",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "EmergencyContactLastName",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "EmergencyContactPhone",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "GuardianChildRelationship",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "GuardianEmail",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "GuardianFirstName",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "GuardianLastName",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "GuardianPhone",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "HomeChurch",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "InvitedBy",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "Children");

            migrationBuilder.CreateTable(
                name: "Guardians",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address1 = table.Column<string>(nullable: false),
                    Address2 = table.Column<string>(nullable: true),
                    AttendHostChurch = table.Column<bool>(nullable: false),
                    ChildRelationship = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    EmergencyContact = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    HomeChurch = table.Column<string>(nullable: true),
                    InvitedBy = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    PrimaryPhone = table.Column<string>(nullable: false),
                    SecondaryPhone = table.Column<string>(nullable: true),
                    SessionId = table.Column<int>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    VBSId = table.Column<int>(nullable: false),
                    Zip = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guardians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guardians_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Guardians_VBS_VBSId",
                        column: x => x.VBSId,
                        principalTable: "VBS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<int>(
                name: "GuardianId",
                table: "Children",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Children_GuardianId",
                table: "Children",
                column: "GuardianId");

            migrationBuilder.CreateIndex(
                name: "IX_Guardians_SessionId",
                table: "Guardians",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Guardians_VBSId",
                table: "Guardians",
                column: "VBSId");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Guardians_GuardianId",
                table: "Children",
                column: "GuardianId",
                principalTable: "Guardians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
