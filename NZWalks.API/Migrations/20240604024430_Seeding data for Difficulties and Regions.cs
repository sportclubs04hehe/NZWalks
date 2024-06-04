using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1e224fad-5f91-412b-8856-9ec6e6c8f742"), "Hard" },
                    { new Guid("621a47c3-1802-4035-a554-f0384ef752bb"), "Medium" },
                    { new Guid("c39c996d-8e8d-497a-ae47-dd47de71c91d"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("1a01cb28-325b-44d5-a428-bc642120ae31"), "XIUUU", "MESSI", "https://images.pexels.com/photos/2592884/pexels-photo-2592884.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("22e0bb74-dd7b-4050-afd1-a792ff68cba2"), "BLA BLA", "HUHI", "https://images.pexels.com/photos/3225517/pexels-photo-3225517.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("57d2b2f6-8e1a-4ea1-9158-874be2779ac6"), "KEKE", "ANH NAM", "https://images.pexels.com/photos/2582614/pexels-photo-2582614.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("adf7e9d5-5c69-46a7-ab72-b63a0422bf3f"), "SIUU", "RONALDO", "https://images.pexels.com/photos/3225517/pexels-photo-3225517.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("ff70aff2-a853-468c-b612-0015762ca4b6"), "XYZ", "ABC", "https://images.pexels.com/photos/3225517/pexels-photo-3225517.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("1e224fad-5f91-412b-8856-9ec6e6c8f742"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("621a47c3-1802-4035-a554-f0384ef752bb"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("c39c996d-8e8d-497a-ae47-dd47de71c91d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("1a01cb28-325b-44d5-a428-bc642120ae31"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("22e0bb74-dd7b-4050-afd1-a792ff68cba2"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("57d2b2f6-8e1a-4ea1-9158-874be2779ac6"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("adf7e9d5-5c69-46a7-ab72-b63a0422bf3f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ff70aff2-a853-468c-b612-0015762ca4b6"));
        }
    }
}
