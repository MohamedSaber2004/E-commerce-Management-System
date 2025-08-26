﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence_Layer.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyColumnsName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "Orders",
                newName: "ShipToAddress_Street");

            migrationBuilder.RenameColumn(
                name: "Address_LastName",
                table: "Orders",
                newName: "ShipToAddress_LastName");

            migrationBuilder.RenameColumn(
                name: "Address_FirstName",
                table: "Orders",
                newName: "ShipToAddress_FirstName");

            migrationBuilder.RenameColumn(
                name: "Address_Country",
                table: "Orders",
                newName: "ShipToAddress_Country");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "Orders",
                newName: "ShipToAddress_City");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Orders",
                newName: "BuyerEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "ShipToAddress_Street",
                table: "Orders",
                newName: "Address_Street");

            migrationBuilder.RenameColumn(
                name: "ShipToAddress_LastName",
                table: "Orders",
                newName: "Address_LastName");

            migrationBuilder.RenameColumn(
                name: "ShipToAddress_FirstName",
                table: "Orders",
                newName: "Address_FirstName");

            migrationBuilder.RenameColumn(
                name: "ShipToAddress_Country",
                table: "Orders",
                newName: "Address_Country");

            migrationBuilder.RenameColumn(
                name: "ShipToAddress_City",
                table: "Orders",
                newName: "Address_City");

            migrationBuilder.RenameColumn(
                name: "BuyerEmail",
                table: "Orders",
                newName: "UserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
