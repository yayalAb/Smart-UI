using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class abnetInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    City = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Subcity = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    POBOX = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactPeople",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPeople", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image1 = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lookups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Vollume = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ContianerNumber = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Size = table.Column<float>(type: "real", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Loacation = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    ManufacturedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Containers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    TinNumber = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    CodeNIF = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    ContactPersonId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Companies_ContactPeople_ContactPersonId",
                        column: x => x.ContactPersonId,
                        principalTable: "ContactPeople",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShippingAgents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingAgents", x => x.Id);
                    table.ForeignKey(
                        name: "fk_shipping agent_address1",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_shipping agent_image1",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TruckNumber = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Capacy = table.Column<float>(type: "real", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Id);
                    table.ForeignKey(
                        name: "image",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserGroupId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BillOfLoadings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    NameOnPermit = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Consignee = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    NotifyParty = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    BillNumber = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    ShippingLine = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    GoodsDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Quantity = table.Column<float>(type: "real", nullable: true),
                    ContainerId = table.Column<int>(type: "int", nullable: false),
                    grossWeight = table.Column<float>(type: "real", nullable: true),
                    TruckNumber = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    ATA = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    FZIN = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    FZOUT = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    DestinationType = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    ShippingAgent = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    PortOfLoading = table.Column<int>(type: "int", nullable: true),
                    ActualDateOfDeparture = table.Column<DateTime>(type: "datetime", nullable: true),
                    EstimatedTimeOfArrival = table.Column<DateTime>(type: "datetime", nullable: true),
                    VoyageNumber = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    TypeOfMerchandise = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    PortId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillOfLoadings", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Bill of Loading_container1",
                        column: x => x.ContainerId,
                        principalTable: "Containers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_Bill of Loading_port1",
                        column: x => x.PortId,
                        principalTable: "Ports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HSCode = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    CBM = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Weight = table.Column<float>(type: "real", nullable: true),
                    Quantity = table.Column<float>(type: "real", nullable: true),
                    UnitPrice = table.Column<float>(type: "real", nullable: true),
                    UnitOfMeasurnment = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    ContainerId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
                    table.ForeignKey(
                        name: "fk_good_container1",
                        column: x => x.ContainerId,
                        principalTable: "Containers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    LicenceNumber = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    TruckId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Drivers_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Drivers_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Page = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanAdd = table.Column<bool>(type: "bit", nullable: false),
                    CanDelete = table.Column<bool>(type: "bit", nullable: false),
                    CanViewDetail = table.Column<bool>(type: "bit", nullable: false),
                    CanView = table.Column<bool>(type: "bit", nullable: false),
                    CanUpdate = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserRoles_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    OperationNumber = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    OpenedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    BillOfLoadingId = table.Column<int>(type: "int", nullable: false),
                    truck_id = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    DriverId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "fk_operation_Bill of Loading1",
                        column: x => x.BillOfLoadingId,
                        principalTable: "BillOfLoadings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_operation_company1",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_operation_driver1",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_operation_truck1",
                        column: x => x.truck_id,
                        principalTable: "Trucks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Documentations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    BankPermit = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    ImporterName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    City = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    TinNumber = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    TransportationMethod = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Source = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documentations_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ECDDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECDDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "fk_ECD Document_operation1",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShippingAgentFees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    BankCode = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    ShippingAgentId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingAgentFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingAgentFees_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShippingAgentFees_ShippingAgents_ShippingAgentId",
                        column: x => x.ShippingAgentId,
                        principalTable: "ShippingAgents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TerminalPortFees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    BankCode = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    operation_Id = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminalPortFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TerminalPortFees_Operations_operation_Id",
                        column: x => x.operation_Id,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE",
                table: "Addresses",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoles_ApplicationUserId",
                table: "AppUserRoles",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserGroupId",
                table: "AspNetUsers",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE1",
                table: "BillOfLoadings",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillOfLoadings_ContainerId",
                table: "BillOfLoadings",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_BillOfLoadings_PortId",
                table: "BillOfLoadings",
                column: "PortId");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE2",
                table: "Companies",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AddressId",
                table: "Companies",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ContactPersonId",
                table: "Companies",
                column: "ContactPersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "ContactPeople",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE1",
                table: "Containers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Containers_AddressId",
                table: "Containers",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE3",
                table: "Documentations",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documentations_OperationId",
                table: "Documentations",
                column: "OperationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_AddressId",
                table: "Drivers",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_ImageId",
                table: "Drivers",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_TruckId",
                table: "Drivers",
                column: "TruckId");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE2",
                table: "ECDDocuments",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ECDDocuments_OperationId",
                table: "ECDDocuments",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE4",
                table: "Goods",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goods_ContainerId",
                table: "Goods",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE3",
                table: "Images",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE5",
                table: "Lookups",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE6",
                table: "Operations",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operations_BillOfLoadingId",
                table: "Operations",
                column: "BillOfLoadingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operations_CompanyId",
                table: "Operations",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_DriverId",
                table: "Operations",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_truck_id",
                table: "Operations",
                column: "truck_id");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE4",
                table: "Ports",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE7",
                table: "ShippingAgentFees",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAgentFees_OperationId",
                table: "ShippingAgentFees",
                column: "OperationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAgentFees_ShippingAgentId",
                table: "ShippingAgentFees",
                column: "ShippingAgentId");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE5",
                table: "ShippingAgents",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAgents_AddressId",
                table: "ShippingAgents",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAgents_ImageId",
                table: "ShippingAgents",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE8",
                table: "TerminalPortFees",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TerminalPortFees_operation_Id",
                table: "TerminalPortFees",
                column: "operation_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_ImageId",
                table: "Trucks",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_Name",
                table: "UserGroups",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Documentations");

            migrationBuilder.DropTable(
                name: "ECDDocuments");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "Lookups");

            migrationBuilder.DropTable(
                name: "ShippingAgentFees");

            migrationBuilder.DropTable(
                name: "TerminalPortFees");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ShippingAgents");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "BillOfLoadings");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.DropTable(
                name: "Ports");

            migrationBuilder.DropTable(
                name: "ContactPeople");

            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
