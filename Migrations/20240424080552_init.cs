using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Grupp3Hattmakaren.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    headSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    size = table.Column<double>(type: "float", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
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
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    streetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    zipCode = table.Column<int>(type: "int", nullable: true),
                    countryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
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
                name: "Enquiries",
                columns: table => new
                {
                    EnquiryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    headSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    consentHat = table.Column<bool>(type: "bit", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fabricMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    specialEffectMaterials = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    font = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    textOnHat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isInProgress = table.Column<bool>(type: "bit", nullable: false),
                    isSpecial = table.Column<bool>(type: "bit", nullable: false),
                    getInStore = table.Column<bool>(type: "bit", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enquiries", x => x.EnquiryId);
                    table.ForeignKey(
                        name: "FK_Enquiries_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_customerId",
                        column: x => x.customerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    materialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    supplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.materialId);
                    table.ForeignKey(
                        name: "FK_Materials_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    price = table.Column<double>(type: "float", nullable: false),
                    isPayed = table.Column<bool>(type: "bit", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    EnquiryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Enquiries_EnquiryId",
                        column: x => x.EnquiryId,
                        principalTable: "Enquiries",
                        principalColumn: "EnquiryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductShoppingCart",
                columns: table => new
                {
                    ShoppingCartsId = table.Column<int>(type: "int", nullable: false),
                    productsProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductShoppingCart", x => new { x.ShoppingCartsId, x.productsProductId });
                    table.ForeignKey(
                        name: "FK_ProductShoppingCart_Products_productsProductId",
                        column: x => x.productsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductShoppingCart_ShoppingCarts_ShoppingCartsId",
                        column: x => x.ShoppingCartsId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductShoppingCarts",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false),
                    shoppingCartId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductShoppingCarts", x => new { x.productId, x.shoppingCartId });
                    table.ForeignKey(
                        name: "FK_ProductShoppingCarts_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductShoppingCarts_ShoppingCarts_shoppingCartId",
                        column: x => x.shoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_Materials",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Materials", x => new { x.ProductId, x.MaterialId });
                    table.ForeignKey(
                        name: "FK_Product_Materials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "materialId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Materials_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    link = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                    table.ForeignKey(
                        name: "FK_Suppliers_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "materialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    productsProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.ProductId, x.productsProductId });
                    table.ForeignKey(
                        name: "FK_OrderProduct_Orders_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Products_productsProductId",
                        column: x => x.productsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingBills",
                columns: table => new
                {
                    ShippingBillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingBills", x => x.ShippingBillId);
                    table.ForeignKey(
                        name: "FK_ShippingBills_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "firstName", "headSize", "lastName" },
                values: new object[,]
                {
                    { "1", 0, "0dc3031b-191f-4177-99bf-92101f8bd862", "Customer", "jonasmoll@outlook.com", false, false, null, null, null, null, null, false, "02b50fa7-7492-4f84-b6b6-cdf85af8f4ef", false, "jonasmoll", "Jonas", "28cm", "Moll" },
                    { "2", 0, "e0959edc-921b-473f-92e1-8768813751d3", "Customer", "tanjahavstorm@outlook.com", false, false, null, null, null, null, null, false, "42f4f7aa-917a-4aa8-9b06-8d8536d9c0de", false, "tanjahavstorm", "Tanja", "79cm", "Havstorm" },
                    { "3", 0, "ef891183-185f-401c-bf8c-3b946990f25a", "Customer", "icamaxi@outlook.com", false, false, null, null, null, null, null, false, "4f10b067-3fc6-412d-ac43-2feaee910bc5", false, "maxmaxsson", "Max", "21cm", "Maxsson" }
                });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "materialId", "ProductId", "name", "price", "quantity", "supplier" },
                values: new object[,]
                {
                    { 1, null, "Rishalm", 599.0, 1000, "Temu" },
                    { 2, null, "Palmlöv", 749.0, 800, "Temu" },
                    { 3, null, "Majsblad", 625.0, 1200, "Wish" },
                    { 4, null, "Hampfibrer", 8.9900000000000002, 1500, "Shein" },
                    { 5, null, "Bomull", 499.0, 2000, "ICA Maxi Bogulundsängen" },
                    { 6, null, "Linne", 649.0, 1800, "Ikea" },
                    { 7, null, "Ull", 999.0, 1600, "Får I Närke AB" },
                    { 8, null, "Läder", 1299.0, 2200, "Läder Byxan AB" },
                    { 9, null, "Fjädrar från strutsar", 399.0, 300, "Lannas Strutsfarm" },
                    { 10, null, "Fjädrar från påfåglar", 549.0, 250, "Lannas Strutsfarm" },
                    { 11, null, "Fjädrar från höns", 299.0, 400, "Kronfågeln" },
                    { 12, null, "Tygblommor", 799.0, 500, "Majblomman" },
                    { 13, null, "Pärlor", 699.0, 600, "Förskolan Myrorna" },
                    { 14, null, "Spets", 849.0, 700, "Victoria Secret" },
                    { 15, null, "Lackerat papper", 599.0, 450, "Dunder Mifflin" },
                    { 16, null, "Lurextråd", 449.0, 550, "Närke Slakteri AB" },
                    { 17, null, "Fuskpäls", 1099.0, 350, "MotherLoad AB" },
                    { 18, null, "Kaninfilt", 105.0, 10, "Kaninens KooperationAB" },
                    { 19, null, "Ull", 200.0, 200, "Kaninens KooperationAB" },
                    { 20, null, "Toquillastrå", 3075.0, 300, "Ecuadour Finest AB" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Discriminator", "ImagePath", "description", "price", "productName", "size" },
                values: new object[] { 1, "Product", null, "Denna mysiga nalle tänds när du rör honom.", 0.0, "Magisk Nalle Natlampa", 350.0 });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "CustomerId", "countryName", "streetName", "zipCode" },
                values: new object[,]
                {
                    { 1, "1", "Countryland", "123 Main Street", 12345 },
                    { 2, "2", "Sweden", "Potatisvägen", 70284 }
                });

            migrationBuilder.InsertData(
                table: "Enquiries",
                columns: new[] { "EnquiryId", "CustomerId", "color", "consentHat", "description", "fabricMaterial", "font", "getInStore", "headSize", "isInProgress", "isSpecial", "specialEffectMaterials", "textOnHat" },
                values: new object[,]
                {
                    { 1787, "2", "blue", true, "test", "", "Times New Roman", true, "22", true, true, "", "" },
                    { 1788, "2", "blue", true, "test", "", "Times New Roman", true, "22", true, true, "", "" },
                    { 1789, "2", "blue", true, "test", "", "Times New Roman", true, "22", true, true, "", "" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "AddressId", "CustomerId", "EnquiryId", "ProductId", "isPayed", "price" },
                values: new object[,]
                {
                    { 1, 1, "1", 1789, 1, true, 150.0 },
                    { 2, 1, "2", 1788, 1, true, 130.0 },
                    { 3, 2, "3", 1787, 1, true, 330.0 }
                });

            migrationBuilder.InsertData(
                table: "ShippingBills",
                columns: new[] { "ShippingBillId", "OrderId", "productCode" },
                values: new object[,]
                {
                    { 1, 1, "SHB001" },
                    { 2, 2, "SHB002" },
                    { 3, 3, "SHB003" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses",
                column: "CustomerId");

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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_CustomerId",
                table: "Enquiries",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_ProductId",
                table: "Materials",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_productsProductId",
                table: "OrderProduct",
                column: "productsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EnquiryId",
                table: "Orders",
                column: "EnquiryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_Materials_MaterialId",
                table: "Product_Materials",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductShoppingCart_productsProductId",
                table: "ProductShoppingCart",
                column: "productsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductShoppingCarts_shoppingCartId",
                table: "ProductShoppingCarts",
                column: "shoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingBills_OrderId",
                table: "ShippingBills",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_customerId",
                table: "ShoppingCarts",
                column: "customerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_MaterialId",
                table: "Suppliers",
                column: "MaterialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "Product_Materials");

            migrationBuilder.DropTable(
                name: "ProductShoppingCart");

            migrationBuilder.DropTable(
                name: "ProductShoppingCarts");

            migrationBuilder.DropTable(
                name: "ShippingBills");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Enquiries");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
