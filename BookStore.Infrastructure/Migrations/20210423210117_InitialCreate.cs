using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BookStore");

            migrationBuilder.CreateTable(
                name: "Tenants",
                schema: "BookStore",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                schema: "BookStore",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "BookStore",
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "BookStore",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "BookStore",
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Books",
                schema: "BookStore",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "BookStore",
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "BookStore",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "BookStore",
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                schema: "BookStore",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Books_BookId",
                        column: x => x.BookId,
                        principalSchema: "BookStore",
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "BookStore",
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "BookStore",
                table: "Tenants",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "Name", "UpdatedAt" },
                values: new object[] { new Guid("d704c4f3-0ea7-4b2f-8c58-d7d0f10e6416"), new DateTime(2021, 4, 23, 23, 1, 16, 731, DateTimeKind.Utc).AddTicks(3677), "This is the default landing domain for Ellaithy Book stores.", "Ellaithy.ALEFbookstores.net", "Default Domain", new DateTime(2021, 4, 23, 23, 1, 16, 731, DateTimeKind.Utc).AddTicks(4604) });

            migrationBuilder.InsertData(
                schema: "BookStore",
                table: "Authors",
                columns: new[] { "Id", "Name", "Nationality", "TenantId" },
                values: new object[,]
                {
                    { new Guid("4ec5dcc0-526a-427c-bead-b2131148bc7a"), "Taha Hussein", "Egypt", new Guid("d704c4f3-0ea7-4b2f-8c58-d7d0f10e6416") },
                    { new Guid("790e3a6b-8a00-4521-9c10-ed5e421b772c"), "Naguib Mahfouz", "Egypt", new Guid("d704c4f3-0ea7-4b2f-8c58-d7d0f10e6416") }
                });

            migrationBuilder.InsertData(
                schema: "BookStore",
                table: "Categories",
                columns: new[] { "Id", "Name", "TenantId" },
                values: new object[,]
                {
                    { new Guid("2ca3868d-dfa4-4d42-9b34-5c646add8831"), "Machine Learning", new Guid("d704c4f3-0ea7-4b2f-8c58-d7d0f10e6416") },
                    { new Guid("7566b97e-9abc-4e96-8f42-ef1509866f0f"), "Literature", new Guid("d704c4f3-0ea7-4b2f-8c58-d7d0f10e6416") },
                    { new Guid("6c50a2fb-9519-4161-b101-eb3a6cf73a71"), "Quality of Control", new Guid("d704c4f3-0ea7-4b2f-8c58-d7d0f10e6416") },
                    { new Guid("f8fdf3cb-853b-405f-b5db-1a5998652972"), "Data Structure", new Guid("d704c4f3-0ea7-4b2f-8c58-d7d0f10e6416") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_TenantId",
                schema: "BookStore",
                table: "Authors",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                schema: "BookStore",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                schema: "BookStore",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_TenantId",
                schema: "BookStore",
                table: "Books",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TenantId",
                schema: "BookStore",
                table: "Categories",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId",
                schema: "BookStore",
                table: "Reviews",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TenantId",
                schema: "BookStore",
                table: "Reviews",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews",
                schema: "BookStore");

            migrationBuilder.DropTable(
                name: "Books",
                schema: "BookStore");

            migrationBuilder.DropTable(
                name: "Authors",
                schema: "BookStore");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "BookStore");

            migrationBuilder.DropTable(
                name: "Tenants",
                schema: "BookStore");
        }
    }
}
