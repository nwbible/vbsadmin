using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VBSAdmin.Data.Migrations
{
    public partial class vbsadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChurchName = table.Column<string>(nullable: false),
                    ContactEmail = table.Column<string>(nullable: false),
                    ContactName = table.Column<string>(nullable: false),
                    ContactPhone = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VBS",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: false),
                    ThemeName = table.Column<string>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VBS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VBS_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Guardians",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address1 = table.Column<string>(nullable: false),
                    Address2 = table.Column<string>(nullable: true),
                    AttendHostChurch = table.Column<string>(nullable: true),
                    ChildRelationship = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
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
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    VBSId = table.Column<int>(nullable: false),
                    Zip = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guardians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guardians_VBS_VBSId",
                        column: x => x.VBSId,
                        principalTable: "VBS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndTime = table.Column<DateTime>(nullable: false),
                    MaxChildren = table.Column<int>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    VBSId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_VBS_VBSId",
                        column: x => x.VBSId,
                        principalTable: "VBS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Grade = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    SessionId = table.Column<int>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    VBSId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Children",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllergiesDescription = table.Column<string>(nullable: true),
                    ClassId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    DecisionMade = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    GradeCompleted = table.Column<int>(nullable: false),
                    GuardianId = table.Column<int>(nullable: false),
                    HasAllergies = table.Column<bool>(nullable: false),
                    HasMedicalCondition = table.Column<bool>(nullable: false),
                    HasMedications = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    MedicalConditionDescription = table.Column<string>(nullable: true),
                    MedicationDescription = table.Column<string>(nullable: true),
                    PlaceChildWithRequest = table.Column<string>(nullable: true),
                    SessionId = table.Column<int>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    VBSId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Children", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Children_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Children_Guardians_GuardianId",
                        column: x => x.GuardianId,
                        principalTable: "Guardians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Children_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Children_VBS_VBSId",
                        column: x => x.VBSId,
                        principalTable: "VBS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Children_ClassId",
                table: "Children",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Children_GuardianId",
                table: "Children",
                column: "GuardianId");

            migrationBuilder.CreateIndex(
                name: "IX_Children_SessionId",
                table: "Children",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Children_VBSId",
                table: "Children",
                column: "VBSId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SessionId",
                table: "Classes",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Guardians_VBSId",
                table: "Guardians",
                column: "VBSId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_VBSId",
                table: "Sessions",
                column: "VBSId");

            migrationBuilder.CreateIndex(
                name: "IX_VBS_TenantId",
                table: "VBS",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Children");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Guardians");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "VBS");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
