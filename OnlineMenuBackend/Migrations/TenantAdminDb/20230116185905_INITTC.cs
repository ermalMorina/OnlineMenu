using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineMenu.Migrations.TenantAdminDb
{
    public partial class INITTC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    TenantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.TenantId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Table",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Table_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isLocal = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isClosed = table.Column<bool>(type: "bit", nullable: false),
                    TableId = table.Column<int>(type: "int", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Table_TableId",
                        column: x => x.TableId,
                        principalTable: "Table",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TableId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOrder_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrder_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductOrder_Table_TableId",
                        column: x => x.TableId,
                        principalTable: "Table",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductOrder_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_TenantId",
                table: "Category",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_TableId",
                table: "Order",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_TenantId",
                table: "Order",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_TenantId",
                table: "Product",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_OrderId",
                table: "ProductOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_ProductId",
                table: "ProductOrder",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_TableId",
                table: "ProductOrder",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_TenantId",
                table: "ProductOrder",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_TenantId",
                table: "Table",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductOrder");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Table");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
