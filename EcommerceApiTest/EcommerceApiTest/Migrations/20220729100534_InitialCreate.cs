using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApiTest.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catigorie",
                columns: table => new
                {
                    CatigorieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatigorieName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CatigorieDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catigorie", x => x.CatigorieId);
                });

            migrationBuilder.CreateTable(
                name: "SubCatigorie",
                columns: table => new
                {
                    SubCatigorieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCatigorieName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCatigorieDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    catigorie = table.Column<int>(type: "int", nullable: false),
                    CatigorieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCatigorie", x => x.SubCatigorieId);
                    table.ForeignKey(
                        name: "FK_SubCatigorie_Catigorie_CatigorieId",
                        column: x => x.CatigorieId,
                        principalTable: "Catigorie",
                        principalColumn: "CatigorieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    customer = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    PictureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pictures = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ProductRef = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.PictureId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureId = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Matricule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Picture_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Picture",
                        principalColumn: "PictureId");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Ref = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Descreption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    subCatigorie = table.Column<int>(type: "int", nullable: false),
                    SubCatigorieId = table.Column<int>(type: "int", nullable: false),
                    provider = table.Column<int>(type: "int", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Ref);
                    table.ForeignKey(
                        name: "FK_Product_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_Product_SubCatigorie_SubCatigorieId",
                        column: x => x.SubCatigorieId,
                        principalTable: "SubCatigorie",
                        principalColumn: "SubCatigorieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_User_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_ProductRef",
                table: "Picture",
                column: "ProductRef");

            migrationBuilder.CreateIndex(
                name: "IX_Product_OrderId",
                table: "Product",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProviderId",
                table: "Product",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SubCatigorieId",
                table: "Product",
                column: "SubCatigorieId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCatigorie_CatigorieId",
                table: "SubCatigorie",
                column: "CatigorieId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PictureId",
                table: "User",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Product_ProductRef",
                table: "Picture",
                column: "ProductRef",
                principalTable: "Product",
                principalColumn: "Ref");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_ProviderId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "SubCatigorie");

            migrationBuilder.DropTable(
                name: "Catigorie");
        }
    }
}
