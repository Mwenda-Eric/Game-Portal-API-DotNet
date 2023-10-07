﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePortalAPI.Migrations
{
    /// <inheritdoc />
    public partial class DateTimeFinalUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "dateCreated",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "lastUpdated",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dateCreated",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "lastUpdated",
                table: "Teachers");
        }
    }
}
