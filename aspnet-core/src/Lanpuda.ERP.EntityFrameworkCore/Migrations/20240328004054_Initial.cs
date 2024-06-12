using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanpuda.ERP.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbpAuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationName = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ImpersonatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImpersonatorUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ImpersonatorTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImpersonatorTenantName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionDuration = table.Column<int>(type: "int", nullable: false),
                    ClientIpAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    CorrelationId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    BrowserInfo = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    HttpMethod = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Exceptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    HttpStatusCode = table.Column<int>(type: "int", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpAuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpBackgroundJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    JobArgs = table.Column<string>(type: "nvarchar(max)", maxLength: 1048576, nullable: false),
                    TryCount = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextTryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastTryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAbandoned = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Priority = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)15),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpBackgroundJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpClaimTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    IsStatic = table.Column<bool>(type: "bit", nullable: false),
                    Regex = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    RegexDescription = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ValueType = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpClaimTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpFeatureGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpFeatureGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ParentName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DefaultValue = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsVisibleToClients = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailableToHost = table.Column<bool>(type: "bit", nullable: false),
                    AllowedProviders = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ValueType = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpFeatureValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpFeatureValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpLinkUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TargetUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpLinkUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpOrganizationUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(95)", maxLength: 95, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    EntityVersion = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpOrganizationUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpOrganizationUnits_AbpOrganizationUnits_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AbpPermissionGrants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpPermissionGrants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpPermissionGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpPermissionGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ParentName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    MultiTenancySide = table.Column<byte>(type: "tinyint", nullable: false),
                    Providers = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    StateCheckers = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsStatic = table.Column<bool>(type: "bit", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    EntityVersion = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpSecurityLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApplicationName = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: true),
                    Identity = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: true),
                    Action = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    TenantName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    CorrelationId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ClientIpAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    BrowserInfo = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpSecurityLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpSettingDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    DefaultValue = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    IsVisibleToClients = table.Column<bool>(type: "bit", nullable: false),
                    Providers = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    IsInherited = table.Column<bool>(type: "bit", nullable: false),
                    IsEncrypted = table.Column<bool>(type: "bit", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpSettingDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpTenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    EntityVersion = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpTenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserDelegations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SourceUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserDelegations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsExternal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ShouldChangePasswordOnNextLogin = table.Column<bool>(type: "bit", nullable: false),
                    EntityVersion = table.Column<int>(type: "int", nullable: false),
                    LastPasswordChangeTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConsentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JsonWebKeySet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Permissions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostLogoutRedirectUris = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RedirectUris = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Settings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictScopes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descriptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resources = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictScopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpAuditLogActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AuditLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MethodName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Parameters = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionDuration = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpAuditLogActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpAuditLogActions_AbpAuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalTable: "AbpAuditLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpEntityChanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChangeTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeType = table.Column<byte>(type: "tinyint", nullable: false),
                    EntityTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    EntityTypeFullName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpEntityChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpEntityChanges_AbpAuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalTable: "AbpAuditLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpOrganizationUnitRoles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpOrganizationUnitRoles", x => new { x.OrganizationUnitId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AbpOrganizationUnitRoles_AbpOrganizationUnits_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbpOrganizationUnitRoles_AbpRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AbpRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpRoleClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpRoleClaims_AbpRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AbpRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpTenantConnectionStrings",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpTenantConnectionStrings", x => new { x.TenantId, x.Name });
                    table.ForeignKey(
                        name: "FK_AbpTenantConnectionStrings_AbpTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "AbpTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpUserClaims_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(196)", maxLength: 196, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserLogins", x => new { x.UserId, x.LoginProvider });
                    table.ForeignKey(
                        name: "FK_AbpUserLogins_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserOrganizationUnits",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserOrganizationUnits", x => new { x.OrganizationUnitId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AbpUserOrganizationUnits_AbpOrganizationUnits_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbpUserOrganizationUnits_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AbpUserRoles_AbpRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AbpRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbpUserRoles_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AbpUserTokens_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppArrivalNotices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: ""),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: ""),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false, comment: ""),
                    ConfirmedTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: ""),
                    ConfirmeUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppArrivalNotices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppArrivalNotices_AbpUsers_ConfirmeUserId",
                        column: x => x.ConfirmeUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppArrivalNotices_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppCustomers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ContactTel = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Consignee = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ConsigneeTel = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    OrganizationName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TaxNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TaxAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TaxTel = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCustomers_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppInventoryMoves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    KeeperUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    SuccessfulTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInventoryMoves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInventoryMoves_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppInventoryMoves_AbpUsers_KeeperUserId",
                        column: x => x.KeeperUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppInventoryTransforms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeeperUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    SuccessfulTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInventoryTransforms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInventoryTransforms_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppInventoryTransforms_AbpUsers_KeeperUserId",
                        column: x => x.KeeperUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppMaterialReturnApplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMaterialReturnApplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMaterialReturnApplies_AbpUsers_ConfirmedUserId",
                        column: x => x.ConfirmedUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMaterialReturnApplies_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppOtherOuts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    KeeperUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    SuccessfulTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOtherOuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppOtherOuts_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppOtherOuts_AbpUsers_KeeperUserId",
                        column: x => x.KeeperUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppOtherStorages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    KeeperUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    SuccessfulTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOtherStorages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppOtherStorages_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppOtherStorages_AbpUsers_KeeperUserId",
                        column: x => x.KeeperUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppProductCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    Remark = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProductCategories_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppProductUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    Remark = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProductUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProductUnits_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppSuppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: ""),
                    FullName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    ShortName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    FactoryAddress = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: ""),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: ""),
                    ContactTel = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: ""),
                    OrganizationName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: ""),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: ""),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: ""),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: ""),
                    TaxAddress = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    TaxTel = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: ""),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSuppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSuppliers_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppWarehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppWarehouses_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppWorkshops",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    Remark = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppWorkshops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppWorkshops_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictAuthorizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Scopes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictAuthorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictAuthorizations_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AbpEntityPropertyChanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntityChangeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewValue = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    OriginalValue = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    PropertyName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PropertyTypeFullName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpEntityPropertyChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpEntityPropertyChanges_AbpEntityChanges_EntityChangeId",
                        column: x => x.EntityChangeId,
                        principalTable: "AbpEntityChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppSalesOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PromisedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CloseStatus = table.Column<int>(type: "int", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmeTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmeUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSalesOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSalesOrders_AbpUsers_ConfirmeUserId",
                        column: x => x.ConfirmeUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppSalesOrders_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppSalesOrders_AppCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AppCustomers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppSalesPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValidDate = table.Column<DateTime>(type: "date", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSalesPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSalesPrices_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppSalesPrices_AppCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AppCustomers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppSalesReturnApplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: ""),
                    Reason = table.Column<int>(type: "int", nullable: false, comment: ""),
                    IsProductReturn = table.Column<bool>(type: "bit", nullable: false, comment: ""),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "具体描述"),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false, comment: ""),
                    ConfirmeTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmeUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSalesReturnApplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSalesReturnApplies_AbpUsers_ConfirmeUserId",
                        column: x => x.ConfirmeUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppSalesReturnApplies_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppSalesReturnApplies_AppCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AppCustomers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppShipmentApplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    Consignee = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    ConsigneeTel = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false, comment: "暂存or确认"),
                    ConfirmeTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmeUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppShipmentApplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppShipmentApplies_AbpUsers_ConfirmeUserId",
                        column: x => x.ConfirmeUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppShipmentApplies_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppShipmentApplies_AppCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AppCustomers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppPurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    RequiredDate = table.Column<DateTime>(type: "date", nullable: false),
                    PromisedDate = table.Column<DateTime>(type: "date", nullable: true),
                    OrderType = table.Column<int>(type: "int", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ContactTel = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmeUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CloseStatus = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPurchaseOrders_AbpUsers_ConfirmeUserId",
                        column: x => x.ConfirmeUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPurchaseOrders_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPurchaseOrders_AppSuppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "AppSuppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppPurchasePrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    AvgDeliveryDate = table.Column<int>(type: "int", nullable: false),
                    QuotationDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPurchasePrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPurchasePrices_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPurchasePrices_AppSuppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "AppSuppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppPurchaseReturnApplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReturnReason = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmeUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPurchaseReturnApplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPurchaseReturnApplies_AbpUsers_ConfirmeUserId",
                        column: x => x.ConfirmeUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPurchaseReturnApplies_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPurchaseReturnApplies_AppSuppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "AppSuppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppInventoryChecks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KeeperUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    SuccessfulTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInventoryChecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInventoryChecks_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppInventoryChecks_AbpUsers_KeeperUserId",
                        column: x => x.KeeperUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppInventoryChecks_AppWarehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "AppWarehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    Remark = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppLocations_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppLocations_AppWarehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "AppWarehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AuthorizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RedemptionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReferenceId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId",
                        column: x => x.AuthorizationId,
                        principalTable: "OpenIddictAuthorizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    ProductCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: ""),
                    ProductUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: ""),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    Spec = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    SourceType = table.Column<int>(type: "int", nullable: false, comment: ""),
                    ProductionBatch = table.Column<double>(type: "float", nullable: true, comment: ""),
                    DefaultLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: ""),
                    LeadTime = table.Column<int>(type: "int", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: ""),
                    IsArrivalInspection = table.Column<bool>(type: "bit", nullable: false),
                    IsProcessInspection = table.Column<bool>(type: "bit", nullable: false),
                    IsFinalInspection = table.Column<bool>(type: "bit", nullable: false),
                    DefaultWorkshopId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProducts_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppProducts_AppLocations_DefaultLocationId",
                        column: x => x.DefaultLocationId,
                        principalTable: "AppLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppProducts_AppProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "AppProductCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppProducts_AppProductUnits_ProductUnitId",
                        column: x => x.ProductUnitId,
                        principalTable: "AppProductUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppProducts_AppWorkshops_DefaultWorkshopId",
                        column: x => x.DefaultWorkshopId,
                        principalTable: "AppWorkshops",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppBoms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBoms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppBoms_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBoms_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppInventories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false, comment: ""),
                    Batch = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInventories_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppInventories_AppLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "AppLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppInventories_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppInventoryCheckDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryCheckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Batch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventoryQuantity = table.Column<double>(type: "float", nullable: false),
                    CheckType = table.Column<int>(type: "int", nullable: false),
                    CheckQuantity = table.Column<double>(type: "float", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInventoryCheckDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInventoryCheckDetails_AppInventoryChecks_InventoryCheckId",
                        column: x => x.InventoryCheckId,
                        principalTable: "AppInventoryChecks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInventoryCheckDetails_AppLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "AppLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppInventoryCheckDetails_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppInventoryLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "产品"),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "库位"),
                    LogTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "发生时间"),
                    LogType = table.Column<int>(type: "int", nullable: false, comment: "出入库类型"),
                    Batch = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "批次"),
                    InQuantity = table.Column<double>(type: "float", nullable: false),
                    OutQuantity = table.Column<double>(type: "float", nullable: false),
                    AfterQuantity = table.Column<double>(type: "float", nullable: false, comment: "期末数量"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInventoryLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInventoryLogs_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppInventoryLogs_AppLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "AppLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppInventoryLogs_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppInventoryMoveDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryMoveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OutLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Batch = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    InLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInventoryMoveDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInventoryMoveDetails_AppInventoryMoves_InventoryMoveId",
                        column: x => x.InventoryMoveId,
                        principalTable: "AppInventoryMoves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppInventoryMoveDetails_AppLocations_InLocationId",
                        column: x => x.InLocationId,
                        principalTable: "AppLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppInventoryMoveDetails_AppLocations_OutLocationId",
                        column: x => x.OutLocationId,
                        principalTable: "AppLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppInventoryMoveDetails_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppInventoryTransformAfterDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryTransformId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Batch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInventoryTransformAfterDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInventoryTransformAfterDetails_AppInventoryTransforms_InventoryTransformId",
                        column: x => x.InventoryTransformId,
                        principalTable: "AppInventoryTransforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInventoryTransformAfterDetails_AppLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "AppLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppInventoryTransformAfterDetails_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppInventoryTransformBeforeDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryTransformId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Batch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInventoryTransformBeforeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInventoryTransformBeforeDetails_AppInventoryTransforms_InventoryTransformId",
                        column: x => x.InventoryTransformId,
                        principalTable: "AppInventoryTransforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInventoryTransformBeforeDetails_AppLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "AppLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppInventoryTransformBeforeDetails_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppOtherOutDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OtherOutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Batch = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOtherOutDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppOtherOutDetails_AppLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "AppLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppOtherOutDetails_AppOtherOuts_OtherOutId",
                        column: x => x.OtherOutId,
                        principalTable: "AppOtherOuts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppOtherOutDetails_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppOtherStorageDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OtherStorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Batch = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOtherStorageDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppOtherStorageDetails_AppLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "AppLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppOtherStorageDetails_AppOtherStorages_OtherStorageId",
                        column: x => x.OtherStorageId,
                        principalTable: "AppOtherStorages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppOtherStorageDetails_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppPurchaseOrderDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromiseDate = table.Column<DateTime>(type: "date", nullable: false),
                    Quantity = table.Column<double>(type: "float", maxLength: 128, nullable: false, comment: ""),
                    Price = table.Column<double>(type: "float", maxLength: 128, nullable: false, comment: ""),
                    TaxRate = table.Column<double>(type: "float", maxLength: 128, nullable: false, comment: ""),
                    Remark = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPurchaseOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPurchaseOrderDetails_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppPurchaseOrderDetails_AppPurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "AppPurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppPurchasePriceDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchasePriceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false, comment: ""),
                    TaxRate = table.Column<double>(type: "float", nullable: false, comment: ""),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPurchasePriceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPurchasePriceDetails_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPurchasePriceDetails_AppPurchasePrices_PurchasePriceId",
                        column: x => x.PurchasePriceId,
                        principalTable: "AppPurchasePrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppSafetyInventories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MinQuantity = table.Column<double>(type: "float", nullable: true),
                    MaxQuantity = table.Column<double>(type: "float", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSafetyInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSafetyInventories_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppSafetyInventories_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppSalesOrderDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "date", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    TaxRate = table.Column<double>(type: "float", nullable: false),
                    Requirement = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSalesOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSalesOrderDetails_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppSalesOrderDetails_AppSalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "AppSalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppSalesPriceDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesPriceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    TaxRate = table.Column<double>(type: "float", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSalesPriceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSalesPriceDetails_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppSalesPriceDetails_AppSalesPrices_SalesPriceId",
                        column: x => x.SalesPriceId,
                        principalTable: "AppSalesPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppBomDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false, comment: ""),
                    Remark = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBomDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppBomDetails_AppBoms_BomId",
                        column: x => x.BomId,
                        principalTable: "AppBoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppBomDetails_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppArrivalNoticeDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArrivalNoticeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: ""),
                    PurchaseOrderDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: ""),
                    Quantity = table.Column<double>(type: "float", nullable: false, comment: ""),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppArrivalNoticeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppArrivalNoticeDetails_AppArrivalNotices_ArrivalNoticeId",
                        column: x => x.ArrivalNoticeId,
                        principalTable: "AppArrivalNotices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppArrivalNoticeDetails_AppPurchaseOrderDetails_PurchaseOrderDetailId",
                        column: x => x.PurchaseOrderDetailId,
                        principalTable: "AppPurchaseOrderDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppMps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    MpsType = table.Column<int>(type: "int", nullable: false),
                    SalesOrderDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", maxLength: 128, nullable: false, comment: ""),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", maxLength: 128, nullable: false, comment: ""),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false, comment: ""),
                    Quantity = table.Column<double>(type: "float(18)", maxLength: 128, precision: 18, scale: 2, nullable: false, comment: ""),
                    Remark = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMps_AbpUsers_ConfirmedUserId",
                        column: x => x.ConfirmedUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMps_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMps_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppMps_AppSalesOrderDetails_SalesOrderDetailId",
                        column: x => x.SalesOrderDetailId,
                        principalTable: "AppSalesOrderDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppShipmentApplyDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShipmentApplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "所属申请单_Id"),
                    SalesOrderDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: ""),
                    Quantity = table.Column<double>(type: "float", nullable: false, comment: "数量"),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppShipmentApplyDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppShipmentApplyDetails_AppSalesOrderDetails_SalesOrderDetailId",
                        column: x => x.SalesOrderDetailId,
                        principalTable: "AppSalesOrderDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppShipmentApplyDetails_AppShipmentApplies_ShipmentApplyId",
                        column: x => x.ShipmentApplyId,
                        principalTable: "AppShipmentApplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppArrivalInspections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArrivalNoticeDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BadQuantity = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmeUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppArrivalInspections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppArrivalInspections_AbpUsers_ConfirmeUserId",
                        column: x => x.ConfirmeUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppArrivalInspections_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppArrivalInspections_AppArrivalNoticeDetails_ArrivalNoticeDetailId",
                        column: x => x.ArrivalNoticeDetailId,
                        principalTable: "AppArrivalNoticeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppPurchaseStorages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArrivalNoticeDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    KeeperUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    SuccessfulTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPurchaseStorages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPurchaseStorages_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPurchaseStorages_AbpUsers_KeeperUserId",
                        column: x => x.KeeperUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPurchaseStorages_AppArrivalNoticeDetails_ArrivalNoticeDetailId",
                        column: x => x.ArrivalNoticeDetailId,
                        principalTable: "AppArrivalNoticeDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppFinalInspections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MpsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BadQuantity = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmeUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFinalInspections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFinalInspections_AbpUsers_ConfirmeUserId",
                        column: x => x.ConfirmeUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppFinalInspections_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppFinalInspections_AppMps_MpsId",
                        column: x => x.MpsId,
                        principalTable: "AppMps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppMpsDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MpsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMpsDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMpsDetails_AppMps_MpsId",
                        column: x => x.MpsId,
                        principalTable: "AppMps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppMrpDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MpsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequiredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMrpDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMrpDetails_AppMps_MpsId",
                        column: x => x.MpsId,
                        principalTable: "AppMps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppMrpDetails_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppPurchaseApplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplyType = table.Column<int>(type: "int", nullable: false),
                    MpsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmeUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsAccept = table.Column<bool>(type: "bit", nullable: false),
                    AcceptTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AcceptUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPurchaseApplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPurchaseApplies_AbpUsers_AcceptUserId",
                        column: x => x.AcceptUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPurchaseApplies_AbpUsers_ConfirmeUserId",
                        column: x => x.ConfirmeUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPurchaseApplies_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPurchaseApplies_AppMps_MpsId",
                        column: x => x.MpsId,
                        principalTable: "AppMps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppWorkOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    WorkshopId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MpsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false, comment: ""),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppWorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppWorkOrders_AbpUsers_ConfirmedUserId",
                        column: x => x.ConfirmedUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppWorkOrders_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppWorkOrders_AppMps_MpsId",
                        column: x => x.MpsId,
                        principalTable: "AppMps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppWorkOrders_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppWorkOrders_AppWorkshops_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "AppWorkshops",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppSalesOuts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShipmentApplyDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: ""),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    KeeperUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false, comment: ""),
                    SuccessfulTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: ""),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSalesOuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSalesOuts_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppSalesOuts_AbpUsers_KeeperUserId",
                        column: x => x.KeeperUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppSalesOuts_AppShipmentApplyDetails_ShipmentApplyDetailId",
                        column: x => x.ShipmentApplyDetailId,
                        principalTable: "AppShipmentApplyDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppPurchaseStorageDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseStorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Batch = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPurchaseStorageDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPurchaseStorageDetails_AppLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "AppLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPurchaseStorageDetails_AppPurchaseStorages_PurchaseStorageId",
                        column: x => x.PurchaseStorageId,
                        principalTable: "AppPurchaseStorages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppPurchaseApplyDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseApplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPurchaseApplyDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPurchaseApplyDetails_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPurchaseApplyDetails_AppPurchaseApplies_PurchaseApplyId",
                        column: x => x.PurchaseApplyId,
                        principalTable: "AppPurchaseApplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppMaterialApplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: ""),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMaterialApplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMaterialApplies_AbpUsers_ConfirmedUserId",
                        column: x => x.ConfirmedUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMaterialApplies_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMaterialApplies_AppWorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "AppWorkOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppWorkOrderMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BomQuantity = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false, comment: ""),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppWorkOrderMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppWorkOrderMaterials_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppWorkOrderMaterials_AppWorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "AppWorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppWorkOrderStorageApplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppWorkOrderStorageApplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppWorkOrderStorageApplies_AbpUsers_ConfirmedUserId",
                        column: x => x.ConfirmedUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppWorkOrderStorageApplies_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppWorkOrderStorageApplies_AppWorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "AppWorkOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppSalesOutDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesOutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: ""),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Batch = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSalesOutDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSalesOutDetails_AppLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "AppLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppSalesOutDetails_AppSalesOuts_SalesOutId",
                        column: x => x.SalesOutId,
                        principalTable: "AppSalesOuts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppPurchaseReturnApplyDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseReturnApplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseStorageDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<double>(type: "float", maxLength: 128, nullable: false, comment: ""),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPurchaseReturnApplyDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPurchaseReturnApplyDetails_AppPurchaseReturnApplies_PurchaseReturnApplyId",
                        column: x => x.PurchaseReturnApplyId,
                        principalTable: "AppPurchaseReturnApplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppPurchaseReturnApplyDetails_AppPurchaseStorageDetails_PurchaseStorageDetailId",
                        column: x => x.PurchaseStorageDetailId,
                        principalTable: "AppPurchaseStorageDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppMaterialApplyDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialApplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false, comment: ""),
                    StandardQuantity = table.Column<double>(type: "float", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMaterialApplyDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMaterialApplyDetails_AppMaterialApplies_MaterialApplyId",
                        column: x => x.MaterialApplyId,
                        principalTable: "AppMaterialApplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppMaterialApplyDetails_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppProcessInspections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkOrderStorageApplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BadQuantity = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmeUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProcessInspections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProcessInspections_AbpUsers_ConfirmeUserId",
                        column: x => x.ConfirmeUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppProcessInspections_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppProcessInspections_AppWorkOrderStorageApplies_WorkOrderStorageApplyId",
                        column: x => x.WorkOrderStorageApplyId,
                        principalTable: "AppWorkOrderStorageApplies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppWorkOrderStorages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkOrderStorageApplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    SuccessfulTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KeeperUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppWorkOrderStorages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppWorkOrderStorages_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppWorkOrderStorages_AbpUsers_KeeperUserId",
                        column: x => x.KeeperUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppWorkOrderStorages_AppWorkOrderStorageApplies_WorkOrderStorageApplyId",
                        column: x => x.WorkOrderStorageApplyId,
                        principalTable: "AppWorkOrderStorageApplies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppSalesReturnApplyDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesReturnApplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: ""),
                    SalesOutDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: ""),
                    Quantity = table.Column<double>(type: "float", nullable: false, comment: ""),
                    Description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSalesReturnApplyDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSalesReturnApplyDetails_AppSalesOutDetails_SalesOutDetailId",
                        column: x => x.SalesOutDetailId,
                        principalTable: "AppSalesOutDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppSalesReturnApplyDetails_AppSalesReturnApplies_SalesReturnApplyId",
                        column: x => x.SalesReturnApplyId,
                        principalTable: "AppSalesReturnApplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppPurchaseReturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseReturnApplyDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    KeeperUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    SuccessfulTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPurchaseReturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPurchaseReturns_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPurchaseReturns_AbpUsers_KeeperUserId",
                        column: x => x.KeeperUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPurchaseReturns_AppPurchaseReturnApplyDetails_PurchaseReturnApplyDetailId",
                        column: x => x.PurchaseReturnApplyDetailId,
                        principalTable: "AppPurchaseReturnApplyDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppWorkOrderOuts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialApplyDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    KeeperUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    SuccessfulTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppWorkOrderOuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppWorkOrderOuts_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppWorkOrderOuts_AbpUsers_KeeperUserId",
                        column: x => x.KeeperUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppWorkOrderOuts_AppMaterialApplyDetails_MaterialApplyDetailId",
                        column: x => x.MaterialApplyDetailId,
                        principalTable: "AppMaterialApplyDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppWorkOrderStorageDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkOrderStorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppWorkOrderStorageDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppWorkOrderStorageDetails_AppLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "AppLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppWorkOrderStorageDetails_AppWorkOrderStorages_WorkOrderStorageId",
                        column: x => x.WorkOrderStorageId,
                        principalTable: "AppWorkOrderStorages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppSalesReturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesReturnApplyDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: ""),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    KeeperUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: ""),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false, comment: ""),
                    SuccessfulTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: ""),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSalesReturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSalesReturns_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppSalesReturns_AbpUsers_KeeperUserId",
                        column: x => x.KeeperUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppSalesReturns_AppSalesReturnApplyDetails_SalesReturnApplyDetailId",
                        column: x => x.SalesReturnApplyDetailId,
                        principalTable: "AppSalesReturnApplyDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppPurchaseReturnDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseReturnId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Batch = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPurchaseReturnDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPurchaseReturnDetails_AppLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "AppLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPurchaseReturnDetails_AppPurchaseReturns_PurchaseReturnId",
                        column: x => x.PurchaseReturnId,
                        principalTable: "AppPurchaseReturns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppWorkOrderOutDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkOrderOutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Batch = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppWorkOrderOutDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppWorkOrderOutDetails_AppLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "AppLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppWorkOrderOutDetails_AppWorkOrderOuts_WorkOrderOutId",
                        column: x => x.WorkOrderOutId,
                        principalTable: "AppWorkOrderOuts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSalesReturnDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesReturnId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: ""),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: ""),
                    Batch = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: ""),
                    Quantity = table.Column<double>(type: "float", nullable: false, comment: ""),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSalesReturnDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSalesReturnDetails_AppLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "AppLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppSalesReturnDetails_AppSalesReturns_SalesReturnId",
                        column: x => x.SalesReturnId,
                        principalTable: "AppSalesReturns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppMaterialReturnApplyDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialReturnApplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkOrderOutDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false, comment: ""),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMaterialReturnApplyDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMaterialReturnApplyDetails_AppMaterialReturnApplies_MaterialReturnApplyId",
                        column: x => x.MaterialReturnApplyId,
                        principalTable: "AppMaterialReturnApplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppMaterialReturnApplyDetails_AppWorkOrderOutDetails_WorkOrderOutDetailId",
                        column: x => x.WorkOrderOutDetailId,
                        principalTable: "AppWorkOrderOutDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppWorkOrderReturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialReturnApplyDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    KeeperUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    SuccessfulTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppWorkOrderReturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppWorkOrderReturns_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppWorkOrderReturns_AbpUsers_KeeperUserId",
                        column: x => x.KeeperUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppWorkOrderReturns_AppMaterialReturnApplyDetails_MaterialReturnApplyDetailId",
                        column: x => x.MaterialReturnApplyDetailId,
                        principalTable: "AppMaterialReturnApplyDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppWorkOrderReturnDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkOrderReturnId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppWorkOrderReturnDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppWorkOrderReturnDetails_AppLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "AppLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppWorkOrderReturnDetails_AppWorkOrderReturns_WorkOrderReturnId",
                        column: x => x.WorkOrderReturnId,
                        principalTable: "AppWorkOrderReturns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpAuditLogActions_AuditLogId",
                table: "AbpAuditLogActions",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpAuditLogActions_TenantId_ServiceName_MethodName_ExecutionTime",
                table: "AbpAuditLogActions",
                columns: new[] { "TenantId", "ServiceName", "MethodName", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpAuditLogs_TenantId_ExecutionTime",
                table: "AbpAuditLogs",
                columns: new[] { "TenantId", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpAuditLogs_TenantId_UserId_ExecutionTime",
                table: "AbpAuditLogs",
                columns: new[] { "TenantId", "UserId", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpBackgroundJobs_IsAbandoned_NextTryTime",
                table: "AbpBackgroundJobs",
                columns: new[] { "IsAbandoned", "NextTryTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpEntityChanges_AuditLogId",
                table: "AbpEntityChanges",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpEntityChanges_TenantId_EntityTypeFullName_EntityId",
                table: "AbpEntityChanges",
                columns: new[] { "TenantId", "EntityTypeFullName", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpEntityPropertyChanges_EntityChangeId",
                table: "AbpEntityPropertyChanges",
                column: "EntityChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpFeatureGroups_Name",
                table: "AbpFeatureGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpFeatures_GroupName",
                table: "AbpFeatures",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpFeatures_Name",
                table: "AbpFeatures",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpFeatureValues_Name_ProviderName_ProviderKey",
                table: "AbpFeatureValues",
                columns: new[] { "Name", "ProviderName", "ProviderKey" },
                unique: true,
                filter: "[ProviderName] IS NOT NULL AND [ProviderKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AbpLinkUsers_SourceUserId_SourceTenantId_TargetUserId_TargetTenantId",
                table: "AbpLinkUsers",
                columns: new[] { "SourceUserId", "SourceTenantId", "TargetUserId", "TargetTenantId" },
                unique: true,
                filter: "[SourceTenantId] IS NOT NULL AND [TargetTenantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AbpOrganizationUnitRoles_RoleId_OrganizationUnitId",
                table: "AbpOrganizationUnitRoles",
                columns: new[] { "RoleId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpOrganizationUnits_Code",
                table: "AbpOrganizationUnits",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_AbpOrganizationUnits_ParentId",
                table: "AbpOrganizationUnits",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissionGrants_TenantId_Name_ProviderName_ProviderKey",
                table: "AbpPermissionGrants",
                columns: new[] { "TenantId", "Name", "ProviderName", "ProviderKey" },
                unique: true,
                filter: "[TenantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissionGroups_Name",
                table: "AbpPermissionGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissions_GroupName",
                table: "AbpPermissions",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissions_Name",
                table: "AbpPermissions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpRoleClaims_RoleId",
                table: "AbpRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpRoles_NormalizedName",
                table: "AbpRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpSecurityLogs_TenantId_Action",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "Action" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSecurityLogs_TenantId_ApplicationName",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "ApplicationName" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSecurityLogs_TenantId_Identity",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "Identity" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSecurityLogs_TenantId_UserId",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSettingDefinitions_Name",
                table: "AbpSettingDefinitions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpSettings_Name_ProviderName_ProviderKey",
                table: "AbpSettings",
                columns: new[] { "Name", "ProviderName", "ProviderKey" },
                unique: true,
                filter: "[ProviderName] IS NOT NULL AND [ProviderKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AbpTenants_Name",
                table: "AbpTenants",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserClaims_UserId",
                table: "AbpUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserLogins_LoginProvider_ProviderKey",
                table: "AbpUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserOrganizationUnits_UserId_OrganizationUnitId",
                table: "AbpUserOrganizationUnits",
                columns: new[] { "UserId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserRoles_RoleId_UserId",
                table: "AbpUserRoles",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_Email",
                table: "AbpUsers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_NormalizedEmail",
                table: "AbpUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_NormalizedUserName",
                table: "AbpUsers",
                column: "NormalizedUserName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_UserName",
                table: "AbpUsers",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_AppArrivalInspections_ArrivalNoticeDetailId",
                table: "AppArrivalInspections",
                column: "ArrivalNoticeDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppArrivalInspections_ConfirmeUserId",
                table: "AppArrivalInspections",
                column: "ConfirmeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppArrivalInspections_CreatorId",
                table: "AppArrivalInspections",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppArrivalNoticeDetails_ArrivalNoticeId",
                table: "AppArrivalNoticeDetails",
                column: "ArrivalNoticeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppArrivalNoticeDetails_PurchaseOrderDetailId",
                table: "AppArrivalNoticeDetails",
                column: "PurchaseOrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AppArrivalNotices_ConfirmeUserId",
                table: "AppArrivalNotices",
                column: "ConfirmeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppArrivalNotices_CreatorId",
                table: "AppArrivalNotices",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppBomDetails_BomId",
                table: "AppBomDetails",
                column: "BomId");

            migrationBuilder.CreateIndex(
                name: "IX_AppBomDetails_ProductId",
                table: "AppBomDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppBoms_CreatorId",
                table: "AppBoms",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppBoms_ProductId",
                table: "AppBoms",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCustomers_CreatorId",
                table: "AppCustomers",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFinalInspections_ConfirmeUserId",
                table: "AppFinalInspections",
                column: "ConfirmeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFinalInspections_CreatorId",
                table: "AppFinalInspections",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFinalInspections_MpsId",
                table: "AppFinalInspections",
                column: "MpsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppInventories_CreatorId",
                table: "AppInventories",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventories_LocationId",
                table: "AppInventories",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventories_ProductId",
                table: "AppInventories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryCheckDetails_InventoryCheckId",
                table: "AppInventoryCheckDetails",
                column: "InventoryCheckId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryCheckDetails_LocationId",
                table: "AppInventoryCheckDetails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryCheckDetails_ProductId",
                table: "AppInventoryCheckDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryChecks_CreatorId",
                table: "AppInventoryChecks",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryChecks_KeeperUserId",
                table: "AppInventoryChecks",
                column: "KeeperUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryChecks_WarehouseId",
                table: "AppInventoryChecks",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryLogs_CreatorId",
                table: "AppInventoryLogs",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryLogs_LocationId",
                table: "AppInventoryLogs",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryLogs_ProductId",
                table: "AppInventoryLogs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryMoveDetails_InLocationId",
                table: "AppInventoryMoveDetails",
                column: "InLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryMoveDetails_InventoryMoveId",
                table: "AppInventoryMoveDetails",
                column: "InventoryMoveId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryMoveDetails_OutLocationId",
                table: "AppInventoryMoveDetails",
                column: "OutLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryMoveDetails_ProductId",
                table: "AppInventoryMoveDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryMoves_CreatorId",
                table: "AppInventoryMoves",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryMoves_KeeperUserId",
                table: "AppInventoryMoves",
                column: "KeeperUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryTransformAfterDetails_InventoryTransformId",
                table: "AppInventoryTransformAfterDetails",
                column: "InventoryTransformId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryTransformAfterDetails_LocationId",
                table: "AppInventoryTransformAfterDetails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryTransformAfterDetails_ProductId",
                table: "AppInventoryTransformAfterDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryTransformBeforeDetails_InventoryTransformId",
                table: "AppInventoryTransformBeforeDetails",
                column: "InventoryTransformId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryTransformBeforeDetails_LocationId",
                table: "AppInventoryTransformBeforeDetails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryTransformBeforeDetails_ProductId",
                table: "AppInventoryTransformBeforeDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryTransforms_CreatorId",
                table: "AppInventoryTransforms",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInventoryTransforms_KeeperUserId",
                table: "AppInventoryTransforms",
                column: "KeeperUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppLocations_CreatorId",
                table: "AppLocations",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppLocations_WarehouseId",
                table: "AppLocations",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMaterialApplies_ConfirmedUserId",
                table: "AppMaterialApplies",
                column: "ConfirmedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMaterialApplies_CreatorId",
                table: "AppMaterialApplies",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMaterialApplies_WorkOrderId",
                table: "AppMaterialApplies",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMaterialApplyDetails_MaterialApplyId",
                table: "AppMaterialApplyDetails",
                column: "MaterialApplyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMaterialApplyDetails_ProductId",
                table: "AppMaterialApplyDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMaterialReturnApplies_ConfirmedUserId",
                table: "AppMaterialReturnApplies",
                column: "ConfirmedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMaterialReturnApplies_CreatorId",
                table: "AppMaterialReturnApplies",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMaterialReturnApplyDetails_MaterialReturnApplyId",
                table: "AppMaterialReturnApplyDetails",
                column: "MaterialReturnApplyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMaterialReturnApplyDetails_WorkOrderOutDetailId",
                table: "AppMaterialReturnApplyDetails",
                column: "WorkOrderOutDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMps_ConfirmedUserId",
                table: "AppMps",
                column: "ConfirmedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMps_CreatorId",
                table: "AppMps",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMps_Number",
                table: "AppMps",
                column: "Number");

            migrationBuilder.CreateIndex(
                name: "IX_AppMps_ProductId",
                table: "AppMps",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMps_SalesOrderDetailId",
                table: "AppMps",
                column: "SalesOrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMpsDetails_MpsId",
                table: "AppMpsDetails",
                column: "MpsId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMrpDetails_MpsId",
                table: "AppMrpDetails",
                column: "MpsId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMrpDetails_ProductId",
                table: "AppMrpDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOtherOutDetails_LocationId",
                table: "AppOtherOutDetails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOtherOutDetails_OtherOutId",
                table: "AppOtherOutDetails",
                column: "OtherOutId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOtherOutDetails_ProductId",
                table: "AppOtherOutDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOtherOuts_CreatorId",
                table: "AppOtherOuts",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOtherOuts_KeeperUserId",
                table: "AppOtherOuts",
                column: "KeeperUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOtherStorageDetails_LocationId",
                table: "AppOtherStorageDetails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOtherStorageDetails_OtherStorageId",
                table: "AppOtherStorageDetails",
                column: "OtherStorageId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOtherStorageDetails_ProductId",
                table: "AppOtherStorageDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOtherStorages_CreatorId",
                table: "AppOtherStorages",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOtherStorages_KeeperUserId",
                table: "AppOtherStorages",
                column: "KeeperUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProcessInspections_ConfirmeUserId",
                table: "AppProcessInspections",
                column: "ConfirmeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProcessInspections_CreatorId",
                table: "AppProcessInspections",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProcessInspections_WorkOrderStorageApplyId",
                table: "AppProcessInspections",
                column: "WorkOrderStorageApplyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppProductCategories_CreatorId",
                table: "AppProductCategories",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_CreatorId",
                table: "AppProducts",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_DefaultLocationId",
                table: "AppProducts",
                column: "DefaultLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_DefaultWorkshopId",
                table: "AppProducts",
                column: "DefaultWorkshopId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_ProductCategoryId",
                table: "AppProducts",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_ProductUnitId",
                table: "AppProducts",
                column: "ProductUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductUnits_CreatorId",
                table: "AppProductUnits",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseApplies_AcceptUserId",
                table: "AppPurchaseApplies",
                column: "AcceptUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseApplies_ConfirmeUserId",
                table: "AppPurchaseApplies",
                column: "ConfirmeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseApplies_CreatorId",
                table: "AppPurchaseApplies",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseApplies_MpsId",
                table: "AppPurchaseApplies",
                column: "MpsId",
                unique: true,
                filter: "[MpsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseApplyDetails_ProductId",
                table: "AppPurchaseApplyDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseApplyDetails_PurchaseApplyId",
                table: "AppPurchaseApplyDetails",
                column: "PurchaseApplyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseOrderDetails_ProductId",
                table: "AppPurchaseOrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseOrderDetails_PurchaseOrderId",
                table: "AppPurchaseOrderDetails",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseOrders_ConfirmeUserId",
                table: "AppPurchaseOrders",
                column: "ConfirmeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseOrders_CreatorId",
                table: "AppPurchaseOrders",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseOrders_SupplierId",
                table: "AppPurchaseOrders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchasePriceDetails_ProductId",
                table: "AppPurchasePriceDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchasePriceDetails_PurchasePriceId",
                table: "AppPurchasePriceDetails",
                column: "PurchasePriceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchasePrices_CreatorId",
                table: "AppPurchasePrices",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchasePrices_SupplierId",
                table: "AppPurchasePrices",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseReturnApplies_ConfirmeUserId",
                table: "AppPurchaseReturnApplies",
                column: "ConfirmeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseReturnApplies_CreatorId",
                table: "AppPurchaseReturnApplies",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseReturnApplies_SupplierId",
                table: "AppPurchaseReturnApplies",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseReturnApplyDetails_PurchaseReturnApplyId",
                table: "AppPurchaseReturnApplyDetails",
                column: "PurchaseReturnApplyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseReturnApplyDetails_PurchaseStorageDetailId",
                table: "AppPurchaseReturnApplyDetails",
                column: "PurchaseStorageDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseReturnDetails_LocationId",
                table: "AppPurchaseReturnDetails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseReturnDetails_PurchaseReturnId",
                table: "AppPurchaseReturnDetails",
                column: "PurchaseReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseReturns_CreatorId",
                table: "AppPurchaseReturns",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseReturns_KeeperUserId",
                table: "AppPurchaseReturns",
                column: "KeeperUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseReturns_PurchaseReturnApplyDetailId",
                table: "AppPurchaseReturns",
                column: "PurchaseReturnApplyDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseStorageDetails_LocationId",
                table: "AppPurchaseStorageDetails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseStorageDetails_PurchaseStorageId",
                table: "AppPurchaseStorageDetails",
                column: "PurchaseStorageId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseStorages_ArrivalNoticeDetailId",
                table: "AppPurchaseStorages",
                column: "ArrivalNoticeDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseStorages_CreatorId",
                table: "AppPurchaseStorages",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseStorages_KeeperUserId",
                table: "AppPurchaseStorages",
                column: "KeeperUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSafetyInventories_CreatorId",
                table: "AppSafetyInventories",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSafetyInventories_ProductId",
                table: "AppSafetyInventories",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesOrderDetails_ProductId",
                table: "AppSalesOrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesOrderDetails_SalesOrderId",
                table: "AppSalesOrderDetails",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesOrders_ConfirmeUserId",
                table: "AppSalesOrders",
                column: "ConfirmeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesOrders_CreatorId",
                table: "AppSalesOrders",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesOrders_CustomerId",
                table: "AppSalesOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesOutDetails_LocationId",
                table: "AppSalesOutDetails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesOutDetails_SalesOutId",
                table: "AppSalesOutDetails",
                column: "SalesOutId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesOuts_CreatorId",
                table: "AppSalesOuts",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesOuts_KeeperUserId",
                table: "AppSalesOuts",
                column: "KeeperUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesOuts_ShipmentApplyDetailId",
                table: "AppSalesOuts",
                column: "ShipmentApplyDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesPriceDetails_ProductId",
                table: "AppSalesPriceDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesPriceDetails_SalesPriceId",
                table: "AppSalesPriceDetails",
                column: "SalesPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesPrices_CreatorId",
                table: "AppSalesPrices",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesPrices_CustomerId",
                table: "AppSalesPrices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesReturnApplies_ConfirmeUserId",
                table: "AppSalesReturnApplies",
                column: "ConfirmeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesReturnApplies_CreatorId",
                table: "AppSalesReturnApplies",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesReturnApplies_CustomerId",
                table: "AppSalesReturnApplies",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesReturnApplyDetails_SalesOutDetailId",
                table: "AppSalesReturnApplyDetails",
                column: "SalesOutDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesReturnApplyDetails_SalesReturnApplyId",
                table: "AppSalesReturnApplyDetails",
                column: "SalesReturnApplyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesReturnDetails_LocationId",
                table: "AppSalesReturnDetails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesReturnDetails_SalesReturnId",
                table: "AppSalesReturnDetails",
                column: "SalesReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesReturns_CreatorId",
                table: "AppSalesReturns",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesReturns_KeeperUserId",
                table: "AppSalesReturns",
                column: "KeeperUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSalesReturns_SalesReturnApplyDetailId",
                table: "AppSalesReturns",
                column: "SalesReturnApplyDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppShipmentApplies_ConfirmeUserId",
                table: "AppShipmentApplies",
                column: "ConfirmeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppShipmentApplies_CreatorId",
                table: "AppShipmentApplies",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppShipmentApplies_CustomerId",
                table: "AppShipmentApplies",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppShipmentApplyDetails_SalesOrderDetailId",
                table: "AppShipmentApplyDetails",
                column: "SalesOrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AppShipmentApplyDetails_ShipmentApplyId",
                table: "AppShipmentApplyDetails",
                column: "ShipmentApplyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSuppliers_CreatorId",
                table: "AppSuppliers",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWarehouses_CreatorId",
                table: "AppWarehouses",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderMaterials_ProductId",
                table: "AppWorkOrderMaterials",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderMaterials_WorkOrderId",
                table: "AppWorkOrderMaterials",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderOutDetails_LocationId",
                table: "AppWorkOrderOutDetails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderOutDetails_WorkOrderOutId",
                table: "AppWorkOrderOutDetails",
                column: "WorkOrderOutId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderOuts_CreatorId",
                table: "AppWorkOrderOuts",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderOuts_KeeperUserId",
                table: "AppWorkOrderOuts",
                column: "KeeperUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderOuts_MaterialApplyDetailId",
                table: "AppWorkOrderOuts",
                column: "MaterialApplyDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderReturnDetails_LocationId",
                table: "AppWorkOrderReturnDetails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderReturnDetails_WorkOrderReturnId",
                table: "AppWorkOrderReturnDetails",
                column: "WorkOrderReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderReturns_CreatorId",
                table: "AppWorkOrderReturns",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderReturns_KeeperUserId",
                table: "AppWorkOrderReturns",
                column: "KeeperUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderReturns_MaterialReturnApplyDetailId",
                table: "AppWorkOrderReturns",
                column: "MaterialReturnApplyDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrders_ConfirmedUserId",
                table: "AppWorkOrders",
                column: "ConfirmedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrders_CreatorId",
                table: "AppWorkOrders",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrders_MpsId",
                table: "AppWorkOrders",
                column: "MpsId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrders_Number",
                table: "AppWorkOrders",
                column: "Number");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrders_ProductId",
                table: "AppWorkOrders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrders_WorkshopId",
                table: "AppWorkOrders",
                column: "WorkshopId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderStorageApplies_ConfirmedUserId",
                table: "AppWorkOrderStorageApplies",
                column: "ConfirmedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderStorageApplies_CreatorId",
                table: "AppWorkOrderStorageApplies",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderStorageApplies_WorkOrderId",
                table: "AppWorkOrderStorageApplies",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderStorageDetails_LocationId",
                table: "AppWorkOrderStorageDetails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderStorageDetails_WorkOrderStorageId",
                table: "AppWorkOrderStorageDetails",
                column: "WorkOrderStorageId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderStorages_CreatorId",
                table: "AppWorkOrderStorages",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderStorages_KeeperUserId",
                table: "AppWorkOrderStorages",
                column: "KeeperUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkOrderStorages_WorkOrderStorageApplyId",
                table: "AppWorkOrderStorages",
                column: "WorkOrderStorageApplyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppWorkshops_CreatorId",
                table: "AppWorkshops",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictApplications_ClientId",
                table: "OpenIddictApplications",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type",
                table: "OpenIddictAuthorizations",
                columns: new[] { "ApplicationId", "Status", "Subject", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictScopes_Name",
                table: "OpenIddictScopes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type",
                table: "OpenIddictTokens",
                columns: new[] { "ApplicationId", "Status", "Subject", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_AuthorizationId",
                table: "OpenIddictTokens",
                column: "AuthorizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ReferenceId",
                table: "OpenIddictTokens",
                column: "ReferenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpAuditLogActions");

            migrationBuilder.DropTable(
                name: "AbpBackgroundJobs");

            migrationBuilder.DropTable(
                name: "AbpClaimTypes");

            migrationBuilder.DropTable(
                name: "AbpEntityPropertyChanges");

            migrationBuilder.DropTable(
                name: "AbpFeatureGroups");

            migrationBuilder.DropTable(
                name: "AbpFeatures");

            migrationBuilder.DropTable(
                name: "AbpFeatureValues");

            migrationBuilder.DropTable(
                name: "AbpLinkUsers");

            migrationBuilder.DropTable(
                name: "AbpOrganizationUnitRoles");

            migrationBuilder.DropTable(
                name: "AbpPermissionGrants");

            migrationBuilder.DropTable(
                name: "AbpPermissionGroups");

            migrationBuilder.DropTable(
                name: "AbpPermissions");

            migrationBuilder.DropTable(
                name: "AbpRoleClaims");

            migrationBuilder.DropTable(
                name: "AbpSecurityLogs");

            migrationBuilder.DropTable(
                name: "AbpSettingDefinitions");

            migrationBuilder.DropTable(
                name: "AbpSettings");

            migrationBuilder.DropTable(
                name: "AbpTenantConnectionStrings");

            migrationBuilder.DropTable(
                name: "AbpUserClaims");

            migrationBuilder.DropTable(
                name: "AbpUserDelegations");

            migrationBuilder.DropTable(
                name: "AbpUserLogins");

            migrationBuilder.DropTable(
                name: "AbpUserOrganizationUnits");

            migrationBuilder.DropTable(
                name: "AbpUserRoles");

            migrationBuilder.DropTable(
                name: "AbpUserTokens");

            migrationBuilder.DropTable(
                name: "AppArrivalInspections");

            migrationBuilder.DropTable(
                name: "AppBomDetails");

            migrationBuilder.DropTable(
                name: "AppFinalInspections");

            migrationBuilder.DropTable(
                name: "AppInventories");

            migrationBuilder.DropTable(
                name: "AppInventoryCheckDetails");

            migrationBuilder.DropTable(
                name: "AppInventoryLogs");

            migrationBuilder.DropTable(
                name: "AppInventoryMoveDetails");

            migrationBuilder.DropTable(
                name: "AppInventoryTransformAfterDetails");

            migrationBuilder.DropTable(
                name: "AppInventoryTransformBeforeDetails");

            migrationBuilder.DropTable(
                name: "AppMpsDetails");

            migrationBuilder.DropTable(
                name: "AppMrpDetails");

            migrationBuilder.DropTable(
                name: "AppOtherOutDetails");

            migrationBuilder.DropTable(
                name: "AppOtherStorageDetails");

            migrationBuilder.DropTable(
                name: "AppProcessInspections");

            migrationBuilder.DropTable(
                name: "AppPurchaseApplyDetails");

            migrationBuilder.DropTable(
                name: "AppPurchasePriceDetails");

            migrationBuilder.DropTable(
                name: "AppPurchaseReturnDetails");

            migrationBuilder.DropTable(
                name: "AppSafetyInventories");

            migrationBuilder.DropTable(
                name: "AppSalesPriceDetails");

            migrationBuilder.DropTable(
                name: "AppSalesReturnDetails");

            migrationBuilder.DropTable(
                name: "AppWorkOrderMaterials");

            migrationBuilder.DropTable(
                name: "AppWorkOrderReturnDetails");

            migrationBuilder.DropTable(
                name: "AppWorkOrderStorageDetails");

            migrationBuilder.DropTable(
                name: "OpenIddictScopes");

            migrationBuilder.DropTable(
                name: "OpenIddictTokens");

            migrationBuilder.DropTable(
                name: "AbpEntityChanges");

            migrationBuilder.DropTable(
                name: "AbpTenants");

            migrationBuilder.DropTable(
                name: "AbpOrganizationUnits");

            migrationBuilder.DropTable(
                name: "AbpRoles");

            migrationBuilder.DropTable(
                name: "AppBoms");

            migrationBuilder.DropTable(
                name: "AppInventoryChecks");

            migrationBuilder.DropTable(
                name: "AppInventoryMoves");

            migrationBuilder.DropTable(
                name: "AppInventoryTransforms");

            migrationBuilder.DropTable(
                name: "AppOtherOuts");

            migrationBuilder.DropTable(
                name: "AppOtherStorages");

            migrationBuilder.DropTable(
                name: "AppPurchaseApplies");

            migrationBuilder.DropTable(
                name: "AppPurchasePrices");

            migrationBuilder.DropTable(
                name: "AppPurchaseReturns");

            migrationBuilder.DropTable(
                name: "AppSalesPrices");

            migrationBuilder.DropTable(
                name: "AppSalesReturns");

            migrationBuilder.DropTable(
                name: "AppWorkOrderReturns");

            migrationBuilder.DropTable(
                name: "AppWorkOrderStorages");

            migrationBuilder.DropTable(
                name: "OpenIddictAuthorizations");

            migrationBuilder.DropTable(
                name: "AbpAuditLogs");

            migrationBuilder.DropTable(
                name: "AppPurchaseReturnApplyDetails");

            migrationBuilder.DropTable(
                name: "AppSalesReturnApplyDetails");

            migrationBuilder.DropTable(
                name: "AppMaterialReturnApplyDetails");

            migrationBuilder.DropTable(
                name: "AppWorkOrderStorageApplies");

            migrationBuilder.DropTable(
                name: "OpenIddictApplications");

            migrationBuilder.DropTable(
                name: "AppPurchaseReturnApplies");

            migrationBuilder.DropTable(
                name: "AppPurchaseStorageDetails");

            migrationBuilder.DropTable(
                name: "AppSalesOutDetails");

            migrationBuilder.DropTable(
                name: "AppSalesReturnApplies");

            migrationBuilder.DropTable(
                name: "AppMaterialReturnApplies");

            migrationBuilder.DropTable(
                name: "AppWorkOrderOutDetails");

            migrationBuilder.DropTable(
                name: "AppPurchaseStorages");

            migrationBuilder.DropTable(
                name: "AppSalesOuts");

            migrationBuilder.DropTable(
                name: "AppWorkOrderOuts");

            migrationBuilder.DropTable(
                name: "AppArrivalNoticeDetails");

            migrationBuilder.DropTable(
                name: "AppShipmentApplyDetails");

            migrationBuilder.DropTable(
                name: "AppMaterialApplyDetails");

            migrationBuilder.DropTable(
                name: "AppArrivalNotices");

            migrationBuilder.DropTable(
                name: "AppPurchaseOrderDetails");

            migrationBuilder.DropTable(
                name: "AppShipmentApplies");

            migrationBuilder.DropTable(
                name: "AppMaterialApplies");

            migrationBuilder.DropTable(
                name: "AppPurchaseOrders");

            migrationBuilder.DropTable(
                name: "AppWorkOrders");

            migrationBuilder.DropTable(
                name: "AppSuppliers");

            migrationBuilder.DropTable(
                name: "AppMps");

            migrationBuilder.DropTable(
                name: "AppSalesOrderDetails");

            migrationBuilder.DropTable(
                name: "AppProducts");

            migrationBuilder.DropTable(
                name: "AppSalesOrders");

            migrationBuilder.DropTable(
                name: "AppLocations");

            migrationBuilder.DropTable(
                name: "AppProductCategories");

            migrationBuilder.DropTable(
                name: "AppProductUnits");

            migrationBuilder.DropTable(
                name: "AppWorkshops");

            migrationBuilder.DropTable(
                name: "AppCustomers");

            migrationBuilder.DropTable(
                name: "AppWarehouses");

            migrationBuilder.DropTable(
                name: "AbpUsers");
        }
    }
}
