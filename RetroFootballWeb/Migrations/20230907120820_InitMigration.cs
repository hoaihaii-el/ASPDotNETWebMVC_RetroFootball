using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroFootballWeb.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(450)", nullable: false),
                    Name = table.Column<string>(type: "varchar(max)", nullable: false),
                    Password = table.Column<string>(type: "varchar(max)", nullable: false),
                    Role = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(450)", nullable: false),
                    Name = table.Column<string>(type: "varchar(max)", nullable: false),
                    Club = table.Column<string>(type: "varchar(max)", nullable: false),
                    Nation = table.Column<string>(type: "varchar(max)", nullable: false),
                    Season = table.Column<string>(type: "varchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size_s = table.Column<int>(type: "int", nullable: false),
                    Size_m = table.Column<int>(type: "int", nullable: false),
                    Size_l = table.Column<int>(type: "int", nullable: false),
                    Size_xl = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(max)", nullable: false),
                    Time_added = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", nullable: false),
                    Image = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryInfos",
                columns: table => new
                {
                    CustomerID = table.Column<string>(type: "varchar(450)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(max)", nullable: false),
                    Address = table.Column<string>(type: "varchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryInfos", x => new { x.CustomerID, x.Priority });
                    table.ForeignKey(
                        name: "FK_DeliveryInfos_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<string>(type: "varchar(450)", nullable: false),
                    Time_create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pay_method = table.Column<string>(type: "varchar(max)", nullable: false),
                    Delivery_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delivery_method = table.Column<string>(type: "varchar(max)", nullable: false),
                    Status = table.Column<string>(type: "varchar(max)", nullable: false),
                    Note = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "varchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "varchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => new { x.CustomerId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_Carts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishLists",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "varchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "varchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishLists", x => new { x.CustomerId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_WishLists_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishLists_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<string>(type: "varchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.OrderID, x.CustomerID });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_CustomerID",
                table: "OrderDetails",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_ProductId",
                table: "WishLists",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "DeliveryInfos");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "WishLists");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
