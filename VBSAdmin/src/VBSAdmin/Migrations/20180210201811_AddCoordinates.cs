using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBSAdmin.Migrations
{
    public partial class AddCoordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_Classes_ClassroomId",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_Sessions_SessionId",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_VBS_VBSId",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_VBS_VBSId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Children",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Children",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Classes_ClassroomId",
                table: "Children",
                column: "ClassroomId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Sessions_SessionId",
                table: "Children",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_VBS_VBSId",
                table: "Children",
                column: "VBSId",
                principalTable: "VBS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_VBS_VBSId",
                table: "Classes",
                column: "VBSId",
                principalTable: "VBS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_Classes_ClassroomId",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_Sessions_SessionId",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_VBS_VBSId",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_VBS_VBSId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Children");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Classes_ClassroomId",
                table: "Children",
                column: "ClassroomId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Sessions_SessionId",
                table: "Children",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_VBS_VBSId",
                table: "Children",
                column: "VBSId",
                principalTable: "VBS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_VBS_VBSId",
                table: "Classes",
                column: "VBSId",
                principalTable: "VBS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
