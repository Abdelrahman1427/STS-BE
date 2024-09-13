using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PathFinder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MailServer = table.Column<int>(type: "int", nullable: false),
                    ServerName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    Encryption = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HubConnection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectionId = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ReciverName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HubConnection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ReciverName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MessageType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NotificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Module = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermissionPlatform = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_UserType_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccessToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccurringDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assessment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assessment_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assessment_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Certification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certification_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Certification_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyCategory_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyCategory_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanySector",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySector", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanySector_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanySector_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanySize",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySize", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanySize_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanySize_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyStatuse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyStatuse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyStatuse_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyStatuse_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Disability",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disability_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Disability_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documentation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documentation_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documentation_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EducationLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationLevel_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationLevel_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Engagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    OccurDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engagement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Engagement_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Engagement_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EngagementLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngagementLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngagementLevel_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EngagementLevel_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EngagementModality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngagementModality", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngagementModality_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EngagementModality_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnterpriseOrigin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterpriseOrigin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnterpriseOrigin_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnterpriseOrigin_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnterpriseType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterpriseType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnterpriseType_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnterpriseType_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Governorate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governorate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Governorate_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Governorate_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Logger",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logger_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Logger_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageType_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageType_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "New",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title_En = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_En = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_New", x => x.Id);
                    table.ForeignKey(
                        name: "FK_New_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_New_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Objective",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objective", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Objective_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Objective_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Partner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partner_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partner_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpiredOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RevokedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Score_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Score_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuccessStory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title_Ar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Title_En = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description_En = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    VideoURL = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Description_Ar = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuccessStory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuccessStory_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuccessStory_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    GovernorateId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Governorate_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "Governorate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_City_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_City_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinancialServiceProvider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    MOUSigingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActivationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EngagementLevelId = table.Column<int>(type: "int", nullable: false),
                    MOUId = table.Column<int>(type: "int", nullable: false),
                    EnterpriseOriginId = table.Column<int>(type: "int", nullable: false),
                    EnterpriseTypeId = table.Column<int>(type: "int", nullable: false),
                    CompanySizeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CompanyStatusId = table.Column<int>(type: "int", nullable: false),
                    CompanySectorId = table.Column<int>(type: "int", nullable: false),
                    GovernorateId = table.Column<int>(type: "int", nullable: false),
                    CompanyCategoryId = table.Column<int>(type: "int", nullable: false),
                    EducationLevelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialServiceProvider", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialServiceProvider_CompanyCategory_CompanyCategoryId",
                        column: x => x.CompanyCategoryId,
                        principalTable: "CompanyCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialServiceProvider_CompanySector_CompanySectorId",
                        column: x => x.CompanySectorId,
                        principalTable: "CompanySector",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialServiceProvider_CompanySize_CompanySizeId",
                        column: x => x.CompanySizeId,
                        principalTable: "CompanySize",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialServiceProvider_CompanyStatuse_CompanyStatusId",
                        column: x => x.CompanyStatusId,
                        principalTable: "CompanyStatuse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialServiceProvider_Documentation_MOUId",
                        column: x => x.MOUId,
                        principalTable: "Documentation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialServiceProvider_EducationLevel_EducationLevelId",
                        column: x => x.EducationLevelId,
                        principalTable: "EducationLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialServiceProvider_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialServiceProvider_EngagementLevel_EngagementLevelId",
                        column: x => x.EngagementLevelId,
                        principalTable: "EngagementLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialServiceProvider_EnterpriseOrigin_EnterpriseOriginId",
                        column: x => x.EnterpriseOriginId,
                        principalTable: "EnterpriseOrigin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialServiceProvider_EnterpriseType_EnterpriseTypeId",
                        column: x => x.EnterpriseTypeId,
                        principalTable: "EnterpriseType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialServiceProvider_Governorate_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "Governorate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NonGovermntalOrgniszation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Center = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdvertisementNumber = table.Column<int>(type: "int", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPersonTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NGOEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Target = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Achieved = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GovernorateId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonGovermntalOrgniszation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NonGovermntalOrgniszation_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NonGovermntalOrgniszation_Governorate_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "Governorate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialsOfInterest = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactUs_MessageType_MessageTypeId",
                        column: x => x.MessageTypeId,
                        principalTable: "MessageType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactUs_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactUs_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Indicator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IndicatorTypeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: true),
                    Target = table.Column<double>(type: "float", nullable: true),
                    ObjectiveId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Indicator_Objective_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "Objective",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Intervention",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    ObjectiveId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intervention", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Intervention_Objective_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "Objective",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Intervention_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Intervention_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EngagementFinancialServiceProvider",
                columns: table => new
                {
                    EngagementsId = table.Column<int>(type: "int", nullable: false),
                    FinancialServiceProvidersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngagementFinancialServiceProvider", x => new { x.EngagementsId, x.FinancialServiceProvidersId });
                    table.ForeignKey(
                        name: "FK_EngagementFinancialServiceProvider_Engagement_EngagementsId",
                        column: x => x.EngagementsId,
                        principalTable: "Engagement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EngagementFinancialServiceProvider_FinancialServiceProvider_FinancialServiceProvidersId",
                        column: x => x.FinancialServiceProvidersId,
                        principalTable: "FinancialServiceProvider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommunityDevlopmentAssosition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CDACenter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CDALatitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CDALongitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NonGovermntalOrgniszationId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityDevlopmentAssosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunityDevlopmentAssosition_NonGovermntalOrgniszation_NonGovermntalOrgniszationId",
                        column: x => x.NonGovermntalOrgniszationId,
                        principalTable: "NonGovermntalOrgniszation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommunityDevlopmentAssosition_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommunityDevlopmentAssosition_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IndicatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndicatorTransaction_Indicator_IndicatorId",
                        column: x => x.IndicatorId,
                        principalTable: "Indicator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Beneficiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnotherPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workplace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDisability = table.Column<bool>(type: "bit", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    DisabilityId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    EducationLevelId = table.Column<int>(type: "int", nullable: false),
                    IndicatorTransactionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beneficiary_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Beneficiary_Disability_DisabilityId",
                        column: x => x.DisabilityId,
                        principalTable: "Disability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Beneficiary_EducationLevel_EducationLevelId",
                        column: x => x.EducationLevelId,
                        principalTable: "EducationLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Beneficiary_IndicatorTransaction_IndicatorTransactionId",
                        column: x => x.IndicatorTransactionId,
                        principalTable: "IndicatorTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BeneficiarieEngagement",
                columns: table => new
                {
                    BeneficiariesId = table.Column<int>(type: "int", nullable: false),
                    EngagementsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeneficiarieEngagement", x => new { x.BeneficiariesId, x.EngagementsId });
                    table.ForeignKey(
                        name: "FK_BeneficiarieEngagement_Beneficiary_BeneficiariesId",
                        column: x => x.BeneficiariesId,
                        principalTable: "Beneficiary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BeneficiarieEngagement_Engagement_EngagementsId",
                        column: x => x.EngagementsId,
                        principalTable: "Engagement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BeneficiarieCourseTransaction",
                columns: table => new
                {
                    BeneficiariesId = table.Column<int>(type: "int", nullable: false),
                    CourseTransactionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeneficiarieCourseTransaction", x => new { x.BeneficiariesId, x.CourseTransactionsId });
                    table.ForeignKey(
                        name: "FK_BeneficiarieCourseTransaction_Beneficiary_BeneficiariesId",
                        column: x => x.BeneficiariesId,
                        principalTable: "Beneficiary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BeneficiarieWorkplaceEnvironment",
                columns: table => new
                {
                    BeneficiariesId = table.Column<int>(type: "int", nullable: false),
                    WorkplaceEnvironmentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeneficiarieWorkplaceEnvironment", x => new { x.BeneficiariesId, x.WorkplaceEnvironmentsId });
                    table.ForeignKey(
                        name: "FK_BeneficiarieWorkplaceEnvironment_Beneficiary_BeneficiariesId",
                        column: x => x.BeneficiariesId,
                        principalTable: "Beneficiary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainerName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    TrainingDays = table.Column<int>(type: "int", nullable: false),
                    CourseTransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MELCoordinator = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    PlaceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CDACoordinator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterventionId = table.Column<int>(type: "int", nullable: true),
                    GovernorateId = table.Column<int>(type: "int", nullable: true),
                    NonGovermntalOrgniszationId = table.Column<int>(type: "int", nullable: true),
                    PartnerId = table.Column<int>(type: "int", nullable: true),
                    CommunityDevlopmentAssositionId = table.Column<int>(type: "int", nullable: true),
                    PrivateSectorId = table.Column<int>(type: "int", nullable: true),
                    IndicatorTransactionId = table.Column<int>(type: "int", nullable: true),
                    UserTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseTransaction_CommunityDevlopmentAssosition_CommunityDevlopmentAssositionId",
                        column: x => x.CommunityDevlopmentAssositionId,
                        principalTable: "CommunityDevlopmentAssosition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseTransaction_Governorate_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "Governorate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseTransaction_IndicatorTransaction_IndicatorTransactionId",
                        column: x => x.IndicatorTransactionId,
                        principalTable: "IndicatorTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseTransaction_Intervention_InterventionId",
                        column: x => x.InterventionId,
                        principalTable: "Intervention",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseTransaction_NonGovermntalOrgniszation_NonGovermntalOrgniszationId",
                        column: x => x.NonGovermntalOrgniszationId,
                        principalTable: "NonGovermntalOrgniszation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseTransaction_Partner_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseTransaction_UserType_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "privateSector",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MOUSigingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActivationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssessmentStatusId = table.Column<int>(type: "int", nullable: false),
                    CertificationStatusId = table.Column<int>(type: "int", nullable: false),
                    ScoreIntervention = table.Column<int>(type: "int", nullable: false),
                    ScoreStatusId = table.Column<bool>(type: "bit", nullable: false),
                    HiringCount = table.Column<int>(type: "int", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    ScoreId = table.Column<int>(type: "int", nullable: false),
                    EngagementModalityId = table.Column<int>(type: "int", nullable: false),
                    CertificationId = table.Column<int>(type: "int", nullable: false),
                    AssessmentId = table.Column<int>(type: "int", nullable: false),
                    CourseTransaction = table.Column<int>(type: "int", nullable: false),
                    MOUId = table.Column<int>(type: "int", nullable: false),
                    EnterpriseOriginId = table.Column<int>(type: "int", nullable: false),
                    EnterpriseTypeId = table.Column<int>(type: "int", nullable: false),
                    CompanySizeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CompanyStatusId = table.Column<int>(type: "int", nullable: false),
                    CompanySectorId = table.Column<int>(type: "int", nullable: false),
                    GovernorateId = table.Column<int>(type: "int", nullable: false),
                    CompanyCategoryId = table.Column<int>(type: "int", nullable: false),
                    UserTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_privateSector", x => x.Id);
                    table.ForeignKey(
                        name: "FK_privateSector_Assessment_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_privateSector_Certification_CertificationId",
                        column: x => x.CertificationId,
                        principalTable: "Certification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_privateSector_CompanyCategory_CompanyCategoryId",
                        column: x => x.CompanyCategoryId,
                        principalTable: "CompanyCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_privateSector_CompanySector_CompanySectorId",
                        column: x => x.CompanySectorId,
                        principalTable: "CompanySector",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_privateSector_CompanySize_CompanySizeId",
                        column: x => x.CompanySizeId,
                        principalTable: "CompanySize",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_privateSector_CompanyStatuse_CompanyStatusId",
                        column: x => x.CompanyStatusId,
                        principalTable: "CompanyStatuse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_privateSector_CourseTransaction_CourseTransaction",
                        column: x => x.CourseTransaction,
                        principalTable: "CourseTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_privateSector_Documentation_MOUId",
                        column: x => x.MOUId,
                        principalTable: "Documentation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_privateSector_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_privateSector_EngagementModality_EngagementModalityId",
                        column: x => x.EngagementModalityId,
                        principalTable: "EngagementModality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_privateSector_EnterpriseOrigin_EnterpriseOriginId",
                        column: x => x.EnterpriseOriginId,
                        principalTable: "EnterpriseOrigin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_privateSector_EnterpriseType_EnterpriseTypeId",
                        column: x => x.EnterpriseTypeId,
                        principalTable: "EnterpriseType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_privateSector_Governorate_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "Governorate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_privateSector_Score_ScoreId",
                        column: x => x.ScoreId,
                        principalTable: "Score",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_privateSector_UserType_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EngagementPrivateSector",
                columns: table => new
                {
                    EngagementsId = table.Column<int>(type: "int", nullable: false),
                    PrivateSectorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngagementPrivateSector", x => new { x.EngagementsId, x.PrivateSectorsId });
                    table.ForeignKey(
                        name: "FK_EngagementPrivateSector_Engagement_EngagementsId",
                        column: x => x.EngagementsId,
                        principalTable: "Engagement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EngagementPrivateSector_privateSector_PrivateSectorsId",
                        column: x => x.PrivateSectorsId,
                        principalTable: "privateSector",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Policy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PSId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Policy_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policy_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policy_privateSector_PSId",
                        column: x => x.PSId,
                        principalTable: "privateSector",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkplaceEnvironment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    PrivateSectorId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkplaceEnvironment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkplaceEnvironment_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkplaceEnvironment_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkplaceEnvironment_privateSector_PrivateSectorId",
                        column: x => x.PrivateSectorId,
                        principalTable: "privateSector",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessToken_UserId",
                table: "AccessToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_CreatedBy",
                table: "Assessment",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_UpdatedBy",
                table: "Assessment",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiarieCourseTransaction_CourseTransactionsId",
                table: "BeneficiarieCourseTransaction",
                column: "CourseTransactionsId");

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiarieEngagement_EngagementsId",
                table: "BeneficiarieEngagement",
                column: "EngagementsId");

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiarieWorkplaceEnvironment_WorkplaceEnvironmentsId",
                table: "BeneficiarieWorkplaceEnvironment",
                column: "WorkplaceEnvironmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiary_CityId",
                table: "Beneficiary",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiary_DisabilityId",
                table: "Beneficiary",
                column: "DisabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiary_EducationLevelId",
                table: "Beneficiary",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiary_IndicatorTransactionId",
                table: "Beneficiary",
                column: "IndicatorTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Certification_CreatedBy",
                table: "Certification",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Certification_UpdatedBy",
                table: "Certification",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_City_CreatedBy",
                table: "City",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_City_GovernorateId",
                table: "City",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_City_UpdatedBy",
                table: "City",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityDevlopmentAssosition_CreatedBy",
                table: "CommunityDevlopmentAssosition",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityDevlopmentAssosition_NonGovermntalOrgniszationId",
                table: "CommunityDevlopmentAssosition",
                column: "NonGovermntalOrgniszationId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityDevlopmentAssosition_UpdatedBy",
                table: "CommunityDevlopmentAssosition",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCategory_CreatedBy",
                table: "CompanyCategory",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCategory_UpdatedBy",
                table: "CompanyCategory",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySector_CreatedBy",
                table: "CompanySector",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySector_UpdatedBy",
                table: "CompanySector",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySize_CreatedBy",
                table: "CompanySize",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySize_UpdatedBy",
                table: "CompanySize",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStatuse_CreatedBy",
                table: "CompanyStatuse",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStatuse_UpdatedBy",
                table: "CompanyStatuse",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_CreatedBy",
                table: "ContactUs",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_MessageTypeId",
                table: "ContactUs",
                column: "MessageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_UpdatedBy",
                table: "ContactUs",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTransaction_CommunityDevlopmentAssositionId",
                table: "CourseTransaction",
                column: "CommunityDevlopmentAssositionId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTransaction_GovernorateId",
                table: "CourseTransaction",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTransaction_IndicatorTransactionId",
                table: "CourseTransaction",
                column: "IndicatorTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTransaction_InterventionId",
                table: "CourseTransaction",
                column: "InterventionId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTransaction_NonGovermntalOrgniszationId",
                table: "CourseTransaction",
                column: "NonGovermntalOrgniszationId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTransaction_PartnerId",
                table: "CourseTransaction",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTransaction_PrivateSectorId",
                table: "CourseTransaction",
                column: "PrivateSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTransaction_UserTypeId",
                table: "CourseTransaction",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Disability_CreatedBy",
                table: "Disability",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Disability_UpdatedBy",
                table: "Disability",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Documentation_CreatedBy",
                table: "Documentation",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Documentation_UpdatedBy",
                table: "Documentation",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EducationLevel_CreatedBy",
                table: "EducationLevel",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EducationLevel_UpdatedBy",
                table: "EducationLevel",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CreatedBy",
                table: "Employee",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UpdatedBy",
                table: "Employee",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Engagement_CreatedBy",
                table: "Engagement",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Engagement_UpdatedBy",
                table: "Engagement",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EngagementFinancialServiceProvider_FinancialServiceProvidersId",
                table: "EngagementFinancialServiceProvider",
                column: "FinancialServiceProvidersId");

            migrationBuilder.CreateIndex(
                name: "IX_EngagementLevel_CreatedBy",
                table: "EngagementLevel",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EngagementLevel_UpdatedBy",
                table: "EngagementLevel",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EngagementModality_CreatedBy",
                table: "EngagementModality",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EngagementModality_UpdatedBy",
                table: "EngagementModality",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EngagementPrivateSector_PrivateSectorsId",
                table: "EngagementPrivateSector",
                column: "PrivateSectorsId");

            migrationBuilder.CreateIndex(
                name: "IX_EnterpriseOrigin_CreatedBy",
                table: "EnterpriseOrigin",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EnterpriseOrigin_UpdatedBy",
                table: "EnterpriseOrigin",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EnterpriseType_CreatedBy",
                table: "EnterpriseType",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EnterpriseType_UpdatedBy",
                table: "EnterpriseType",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialServiceProvider_CompanyCategoryId",
                table: "FinancialServiceProvider",
                column: "CompanyCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialServiceProvider_CompanySectorId",
                table: "FinancialServiceProvider",
                column: "CompanySectorId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialServiceProvider_CompanySizeId",
                table: "FinancialServiceProvider",
                column: "CompanySizeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialServiceProvider_CompanyStatusId",
                table: "FinancialServiceProvider",
                column: "CompanyStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialServiceProvider_EducationLevelId",
                table: "FinancialServiceProvider",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialServiceProvider_EmployeeId",
                table: "FinancialServiceProvider",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialServiceProvider_EngagementLevelId",
                table: "FinancialServiceProvider",
                column: "EngagementLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialServiceProvider_EnterpriseOriginId",
                table: "FinancialServiceProvider",
                column: "EnterpriseOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialServiceProvider_EnterpriseTypeId",
                table: "FinancialServiceProvider",
                column: "EnterpriseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialServiceProvider_GovernorateId",
                table: "FinancialServiceProvider",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialServiceProvider_MOUId",
                table: "FinancialServiceProvider",
                column: "MOUId");

            migrationBuilder.CreateIndex(
                name: "IX_Governorate_CreatedBy",
                table: "Governorate",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Governorate_UpdatedBy",
                table: "Governorate",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Indicator_ObjectiveId",
                table: "Indicator",
                column: "ObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicatorTransaction_IndicatorId",
                table: "IndicatorTransaction",
                column: "IndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Intervention_CreatedBy",
                table: "Intervention",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Intervention_ObjectiveId",
                table: "Intervention",
                column: "ObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Intervention_UpdatedBy",
                table: "Intervention",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Logger_CreatedBy",
                table: "Logger",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Logger_UpdatedBy",
                table: "Logger",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MessageType_CreatedBy",
                table: "MessageType",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MessageType_UpdatedBy",
                table: "MessageType",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_New_CreatedBy",
                table: "New",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_New_UpdatedBy",
                table: "New",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_NonGovermntalOrgniszation_EmployeeId",
                table: "NonGovermntalOrgniszation",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_NonGovermntalOrgniszation_GovernorateId",
                table: "NonGovermntalOrgniszation",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_Objective_CreatedBy",
                table: "Objective",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Objective_UpdatedBy",
                table: "Objective",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Partner_CreatedBy",
                table: "Partner",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Partner_UpdatedBy",
                table: "Partner",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_CreatedBy",
                table: "Policy",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_PSId",
                table: "Policy",
                column: "PSId");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_UpdatedBy",
                table: "Policy",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_privateSector_AssessmentId",
                table: "privateSector",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_privateSector_CertificationId",
                table: "privateSector",
                column: "CertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_privateSector_CompanyCategoryId",
                table: "privateSector",
                column: "CompanyCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_privateSector_CompanySectorId",
                table: "privateSector",
                column: "CompanySectorId");

            migrationBuilder.CreateIndex(
                name: "IX_privateSector_CompanySizeId",
                table: "privateSector",
                column: "CompanySizeId");

            migrationBuilder.CreateIndex(
                name: "IX_privateSector_CompanyStatusId",
                table: "privateSector",
                column: "CompanyStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_privateSector_CourseTransaction",
                table: "privateSector",
                column: "CourseTransaction",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_privateSector_EmployeeId",
                table: "privateSector",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_privateSector_EngagementModalityId",
                table: "privateSector",
                column: "EngagementModalityId");

            migrationBuilder.CreateIndex(
                name: "IX_privateSector_EnterpriseOriginId",
                table: "privateSector",
                column: "EnterpriseOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_privateSector_EnterpriseTypeId",
                table: "privateSector",
                column: "EnterpriseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_privateSector_GovernorateId",
                table: "privateSector",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_privateSector_MOUId",
                table: "privateSector",
                column: "MOUId");

            migrationBuilder.CreateIndex(
                name: "IX_privateSector_ScoreId",
                table: "privateSector",
                column: "ScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_privateSector_UserTypeId",
                table: "privateSector",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_CreatedBy",
                table: "Score",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Score_UpdatedBy",
                table: "Score",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SuccessStory_CreatedBy",
                table: "SuccessStory",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SuccessStory_UpdatedBy",
                table: "SuccessStory",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserTypeId",
                table: "User",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkplaceEnvironment_CreatedBy",
                table: "WorkplaceEnvironment",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_WorkplaceEnvironment_PrivateSectorId",
                table: "WorkplaceEnvironment",
                column: "PrivateSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkplaceEnvironment_UpdatedBy",
                table: "WorkplaceEnvironment",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_BeneficiarieCourseTransaction_CourseTransaction_CourseTransactionsId",
                table: "BeneficiarieCourseTransaction",
                column: "CourseTransactionsId",
                principalTable: "CourseTransaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BeneficiarieWorkplaceEnvironment_WorkplaceEnvironment_WorkplaceEnvironmentsId",
                table: "BeneficiarieWorkplaceEnvironment",
                column: "WorkplaceEnvironmentsId",
                principalTable: "WorkplaceEnvironment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTransaction_privateSector_PrivateSectorId",
                table: "CourseTransaction",
                column: "PrivateSectorId",
                principalTable: "privateSector",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessment_User_CreatedBy",
                table: "Assessment");

            migrationBuilder.DropForeignKey(
                name: "FK_Assessment_User_UpdatedBy",
                table: "Assessment");

            migrationBuilder.DropForeignKey(
                name: "FK_Certification_User_CreatedBy",
                table: "Certification");

            migrationBuilder.DropForeignKey(
                name: "FK_Certification_User_UpdatedBy",
                table: "Certification");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityDevlopmentAssosition_User_CreatedBy",
                table: "CommunityDevlopmentAssosition");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityDevlopmentAssosition_User_UpdatedBy",
                table: "CommunityDevlopmentAssosition");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCategory_User_CreatedBy",
                table: "CompanyCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCategory_User_UpdatedBy",
                table: "CompanyCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanySector_User_CreatedBy",
                table: "CompanySector");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanySector_User_UpdatedBy",
                table: "CompanySector");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanySize_User_CreatedBy",
                table: "CompanySize");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanySize_User_UpdatedBy",
                table: "CompanySize");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyStatuse_User_CreatedBy",
                table: "CompanyStatuse");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyStatuse_User_UpdatedBy",
                table: "CompanyStatuse");

            migrationBuilder.DropForeignKey(
                name: "FK_Documentation_User_CreatedBy",
                table: "Documentation");

            migrationBuilder.DropForeignKey(
                name: "FK_Documentation_User_UpdatedBy",
                table: "Documentation");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_User_CreatedBy",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_User_UpdatedBy",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_EngagementModality_User_CreatedBy",
                table: "EngagementModality");

            migrationBuilder.DropForeignKey(
                name: "FK_EngagementModality_User_UpdatedBy",
                table: "EngagementModality");

            migrationBuilder.DropForeignKey(
                name: "FK_EnterpriseOrigin_User_CreatedBy",
                table: "EnterpriseOrigin");

            migrationBuilder.DropForeignKey(
                name: "FK_EnterpriseOrigin_User_UpdatedBy",
                table: "EnterpriseOrigin");

            migrationBuilder.DropForeignKey(
                name: "FK_EnterpriseType_User_CreatedBy",
                table: "EnterpriseType");

            migrationBuilder.DropForeignKey(
                name: "FK_EnterpriseType_User_UpdatedBy",
                table: "EnterpriseType");

            migrationBuilder.DropForeignKey(
                name: "FK_Governorate_User_CreatedBy",
                table: "Governorate");

            migrationBuilder.DropForeignKey(
                name: "FK_Governorate_User_UpdatedBy",
                table: "Governorate");

            migrationBuilder.DropForeignKey(
                name: "FK_Intervention_User_CreatedBy",
                table: "Intervention");

            migrationBuilder.DropForeignKey(
                name: "FK_Intervention_User_UpdatedBy",
                table: "Intervention");

            migrationBuilder.DropForeignKey(
                name: "FK_Objective_User_CreatedBy",
                table: "Objective");

            migrationBuilder.DropForeignKey(
                name: "FK_Objective_User_UpdatedBy",
                table: "Objective");

            migrationBuilder.DropForeignKey(
                name: "FK_Partner_User_CreatedBy",
                table: "Partner");

            migrationBuilder.DropForeignKey(
                name: "FK_Partner_User_UpdatedBy",
                table: "Partner");

            migrationBuilder.DropForeignKey(
                name: "FK_Score_User_CreatedBy",
                table: "Score");

            migrationBuilder.DropForeignKey(
                name: "FK_Score_User_UpdatedBy",
                table: "Score");

            migrationBuilder.DropForeignKey(
                name: "FK_privateSector_CourseTransaction_CourseTransaction",
                table: "privateSector");

            migrationBuilder.DropTable(
                name: "AccessToken");

            migrationBuilder.DropTable(
                name: "BeneficiarieCourseTransaction");

            migrationBuilder.DropTable(
                name: "BeneficiarieEngagement");

            migrationBuilder.DropTable(
                name: "BeneficiarieWorkplaceEnvironment");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "EmailSetting");

            migrationBuilder.DropTable(
                name: "EngagementFinancialServiceProvider");

            migrationBuilder.DropTable(
                name: "EngagementPrivateSector");

            migrationBuilder.DropTable(
                name: "HubConnection");

            migrationBuilder.DropTable(
                name: "Logger");

            migrationBuilder.DropTable(
                name: "New");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Policy");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "SuccessStory");

            migrationBuilder.DropTable(
                name: "Beneficiary");

            migrationBuilder.DropTable(
                name: "WorkplaceEnvironment");

            migrationBuilder.DropTable(
                name: "MessageType");

            migrationBuilder.DropTable(
                name: "FinancialServiceProvider");

            migrationBuilder.DropTable(
                name: "Engagement");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Disability");

            migrationBuilder.DropTable(
                name: "EducationLevel");

            migrationBuilder.DropTable(
                name: "EngagementLevel");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "CourseTransaction");

            migrationBuilder.DropTable(
                name: "CommunityDevlopmentAssosition");

            migrationBuilder.DropTable(
                name: "IndicatorTransaction");

            migrationBuilder.DropTable(
                name: "Intervention");

            migrationBuilder.DropTable(
                name: "Partner");

            migrationBuilder.DropTable(
                name: "privateSector");

            migrationBuilder.DropTable(
                name: "NonGovermntalOrgniszation");

            migrationBuilder.DropTable(
                name: "Indicator");

            migrationBuilder.DropTable(
                name: "Assessment");

            migrationBuilder.DropTable(
                name: "Certification");

            migrationBuilder.DropTable(
                name: "CompanyCategory");

            migrationBuilder.DropTable(
                name: "CompanySector");

            migrationBuilder.DropTable(
                name: "CompanySize");

            migrationBuilder.DropTable(
                name: "CompanyStatuse");

            migrationBuilder.DropTable(
                name: "Documentation");

            migrationBuilder.DropTable(
                name: "EngagementModality");

            migrationBuilder.DropTable(
                name: "EnterpriseOrigin");

            migrationBuilder.DropTable(
                name: "EnterpriseType");

            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.DropTable(
                name: "UserType");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Governorate");

            migrationBuilder.DropTable(
                name: "Objective");
        }
    }
}
