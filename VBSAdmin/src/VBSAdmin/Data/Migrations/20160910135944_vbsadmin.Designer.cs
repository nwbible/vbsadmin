﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using VBSAdmin.Data;

namespace VBSAdmin.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160910135944_vbsadmin")]
    partial class vbsadmin
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("VBSAdmin.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("VBSAdmin.Models.VBSAdminModels.Child", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AllergiesDescription");

                    b.Property<int>("ClassId");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<bool>("DecisionMade");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int>("Gender");

                    b.Property<int>("GradeCompleted");

                    b.Property<int>("GuardianId");

                    b.Property<bool>("HasAllergies");

                    b.Property<bool>("HasMedicalCondition");

                    b.Property<bool>("HasMedications");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("MedicalConditionDescription");

                    b.Property<string>("MedicationDescription");

                    b.Property<string>("PlaceChildWithRequest");

                    b.Property<int>("SessionId");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("VBSId");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("GuardianId");

                    b.HasIndex("SessionId");

                    b.HasIndex("VBSId");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("VBSAdmin.Models.VBSAdminModels.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Gender");

                    b.Property<int>("Grade");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("SessionId");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("VBSId");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("VBSAdmin.Models.VBSAdminModels.Guardian", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1")
                        .IsRequired();

                    b.Property<string>("Address2");

                    b.Property<string>("AttendHostChurch");

                    b.Property<string>("ChildRelationship")
                        .IsRequired();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("EmergencyContact")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("HomeChurch");

                    b.Property<string>("InvitedBy");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PrimaryPhone")
                        .IsRequired();

                    b.Property<string>("SecondaryPhone");

                    b.Property<int>("SessionId");

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("VBSId");

                    b.Property<string>("Zip")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("VBSId");

                    b.ToTable("Guardians");
                });

            modelBuilder.Entity("VBSAdmin.Models.VBSAdminModels.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndTime");

                    b.Property<int>("MaxChildren");

                    b.Property<int>("Period");

                    b.Property<DateTime>("StartTime");

                    b.Property<int?>("VBSId");

                    b.HasKey("Id");

                    b.HasIndex("VBSId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("VBSAdmin.Models.VBSAdminModels.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ChurchName")
                        .IsRequired();

                    b.Property<string>("ContactEmail")
                        .IsRequired();

                    b.Property<string>("ContactName")
                        .IsRequired();

                    b.Property<string>("ContactPhone")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("VBSAdmin.Models.VBSAdminModels.VBS", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("TenantId");

                    b.Property<string>("ThemeName")
                        .IsRequired();

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("VBS");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("VBSAdmin.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("VBSAdmin.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VBSAdmin.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VBSAdmin.Models.VBSAdminModels.Child", b =>
                {
                    b.HasOne("VBSAdmin.Models.VBSAdminModels.Class", "Class")
                        .WithMany("Children")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VBSAdmin.Models.VBSAdminModels.Guardian", "Guardian")
                        .WithMany("Children")
                        .HasForeignKey("GuardianId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VBSAdmin.Models.VBSAdminModels.Session", "Session")
                        .WithMany("Children")
                        .HasForeignKey("SessionId");

                    b.HasOne("VBSAdmin.Models.VBSAdminModels.VBS", "VBS")
                        .WithMany("Children")
                        .HasForeignKey("VBSId");
                });

            modelBuilder.Entity("VBSAdmin.Models.VBSAdminModels.Class", b =>
                {
                    b.HasOne("VBSAdmin.Models.VBSAdminModels.Session")
                        .WithMany("Classes")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VBSAdmin.Models.VBSAdminModels.Guardian", b =>
                {
                    b.HasOne("VBSAdmin.Models.VBSAdminModels.VBS")
                        .WithMany("Guardians")
                        .HasForeignKey("VBSId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VBSAdmin.Models.VBSAdminModels.Session", b =>
                {
                    b.HasOne("VBSAdmin.Models.VBSAdminModels.VBS")
                        .WithMany("Sessions")
                        .HasForeignKey("VBSId");
                });

            modelBuilder.Entity("VBSAdmin.Models.VBSAdminModels.VBS", b =>
                {
                    b.HasOne("VBSAdmin.Models.VBSAdminModels.Tenant", "Tenant")
                        .WithMany("VBSList")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
