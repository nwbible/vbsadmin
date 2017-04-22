using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using VBSAdmin.Data;

namespace VBSAdmin.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170415104615_emergency contact relationship")]
    partial class emergencycontactrelationship
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

            modelBuilder.Entity("VBSAdmin.Data.VBSAdminModels.Child", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1")
                        .IsRequired();

                    b.Property<string>("Address2");

                    b.Property<string>("AllergiesDescription");

                    b.Property<bool>("AttendHostChurch");

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<int?>("ClassroomId");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("Date");

                    b.Property<DateTime>("DateOfRegistration");

                    b.Property<bool>("DecisionMade");

                    b.Property<string>("EmergencyContactChildRelationship");

                    b.Property<string>("EmergencyContactFirstName")
                        .IsRequired();

                    b.Property<string>("EmergencyContactLastName")
                        .IsRequired();

                    b.Property<string>("EmergencyContactPhone")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int>("Gender");

                    b.Property<int>("GradeCompleted");

                    b.Property<string>("GuardianChildRelationship");

                    b.Property<string>("GuardianEmail")
                        .IsRequired();

                    b.Property<string>("GuardianFirstName")
                        .IsRequired();

                    b.Property<string>("GuardianLastName")
                        .IsRequired();

                    b.Property<string>("GuardianPhone")
                        .IsRequired();

                    b.Property<bool>("HasAllergies");

                    b.Property<bool>("HasMedicalCondition");

                    b.Property<bool>("HasMedications");

                    b.Property<string>("HomeChurch");

                    b.Property<string>("InvitedBy");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("MedicalConditionDescription");

                    b.Property<string>("MedicationDescription");

                    b.Property<string>("PlaceChildWithRequest");

                    b.Property<int>("SessionId");

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<int>("VBSId");

                    b.Property<string>("Zip")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ClassroomId");

                    b.HasIndex("SessionId");

                    b.HasIndex("VBSId");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("VBSAdmin.Data.VBSAdminModels.Classroom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Gender");

                    b.Property<int>("Grade");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("SessionId");

                    b.Property<int>("VBSId");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.HasIndex("VBSId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("VBSAdmin.Data.VBSAdminModels.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndTime");

                    b.Property<int>("MaxChildren");

                    b.Property<int>("Period");

                    b.Property<DateTime>("StartTime");

                    b.Property<int>("VBSId");

                    b.HasKey("Id");

                    b.HasIndex("VBSId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("VBSAdmin.Data.VBSAdminModels.Tenant", b =>
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

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("VBSAdmin.Data.VBSAdminModels.VBS", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("Date");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("Date");

                    b.Property<int>("TenantId");

                    b.Property<string>("ThemeName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("VBS");
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

                    b.Property<string>("Name");

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

            modelBuilder.Entity("VBSAdmin.Data.VBSAdminModels.Child", b =>
                {
                    b.HasOne("VBSAdmin.Data.VBSAdminModels.Classroom", "Classroom")
                        .WithMany("Children")
                        .HasForeignKey("ClassroomId");

                    b.HasOne("VBSAdmin.Data.VBSAdminModels.Session", "Session")
                        .WithMany("Children")
                        .HasForeignKey("SessionId");

                    b.HasOne("VBSAdmin.Data.VBSAdminModels.VBS", "VBS")
                        .WithMany("Children")
                        .HasForeignKey("VBSId");
                });

            modelBuilder.Entity("VBSAdmin.Data.VBSAdminModels.Classroom", b =>
                {
                    b.HasOne("VBSAdmin.Data.VBSAdminModels.Session", "Session")
                        .WithMany("Classes")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VBSAdmin.Data.VBSAdminModels.VBS", "VBS")
                        .WithMany("Classrooms")
                        .HasForeignKey("VBSId");
                });

            modelBuilder.Entity("VBSAdmin.Data.VBSAdminModels.Session", b =>
                {
                    b.HasOne("VBSAdmin.Data.VBSAdminModels.VBS", "VBS")
                        .WithMany("Sessions")
                        .HasForeignKey("VBSId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VBSAdmin.Data.VBSAdminModels.VBS", b =>
                {
                    b.HasOne("VBSAdmin.Data.VBSAdminModels.Tenant", "Tenant")
                        .WithMany("VBSList")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
