using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CampChetek.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "beddings",
                columns: table => new
                {
                    BeddingId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_beddings", x => x.BeddingId);
                });

            migrationBuilder.CreateTable(
                name: "Reasons",
                columns: table => new
                {
                    ReasonId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reasons", x => x.ReasonId);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(maxLength: 25, nullable: false),
                    UserPassword = table.Column<string>(nullable: true),
                    UserEmail = table.Column<string>(nullable: true),
                    RememberMe = table.Column<bool>(nullable: false),
                    ReturnUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    RoomsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    StatusId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.RoomsId);
                    table.ForeignKey(
                        name: "FK_Room_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    event_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    event_start = table.Column<DateTime>(nullable: false),
                    event_end = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    Zip = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    NumberGuest = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    RoomsId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    BeddingId = table.Column<string>(nullable: true),
                    DateUpdate = table.Column<string>(nullable: true),
                    all_day = table.Column<bool>(nullable: false),
                    sendEmail = table.Column<bool>(nullable: false),
                    ReasonId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.event_id);
                    table.ForeignKey(
                        name: "FK_events_beddings_BeddingId",
                        column: x => x.BeddingId,
                        principalTable: "beddings",
                        principalColumn: "BeddingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_events_Reasons_ReasonId",
                        column: x => x.ReasonId,
                        principalTable: "Reasons",
                        principalColumn: "ReasonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_events_Room_RoomsId",
                        column: x => x.RoomsId,
                        principalTable: "Room",
                        principalColumn: "RoomsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_events_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Reasons",
                columns: new[] { "ReasonId", "Name" },
                values: new object[,]
                {
                    { "F", "Rental" },
                    { "G", "Family Of Staff" },
                    { "J", "Other" },
                    { "E", "Former Camper" },
                    { "D", "Former Staff" },
                    { "C", "Evanglist" },
                    { "B", "Missionary" },
                    { "H", "Sponsor" },
                    { "A", "Pastor" },
                    { "I", "Attending An Event" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { "D", "Dirty" },
                    { "O", "Occupied" },
                    { "R", "Ready" },
                    { "C", "Clean" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "userId", "Email", "FirstName", "LastName", "Password", "RememberMe", "ReturnUrl", "UserEmail", "UserPassword" },
                values: new object[,]
                {
                    { 1, "danael@campchetek.org", "Dan", "Kluver", "cccc1944ad", false, null, null, null },
                    { 2, "marikkakay@yahoo.com", "Marikka", "Kartman", "Marikka0410", false, null, null, null },
                    { 3, "system@campchetek.org", "System", "1", "12Ers45Bd5sS@", false, null, null, null },
                    { 5, "email@goeshere.org", "First", "Last", "X234Veeetw123", false, null, null, null },
                    { 4, "pr@campchetek.org", "Randy", "Tanis", "Pronly3236", false, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "beddings",
                columns: new[] { "BeddingId", "Name" },
                values: new object[,]
                {
                    { "H", "1 Queen, 6 Twin" },
                    { "G", "1 Queen, 5 Twin" },
                    { "F", "1 Queen, 4 Twin" },
                    { "E", "1 Queen, 3 Twin" },
                    { "D", "1 Queen, 2 Twin" },
                    { "C", "1 Queen, 1 Twin" },
                    { "B", "1 Twin" },
                    { "A", "1 Queen" }
                });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "RoomsId", "Name", "StatusId" },
                values: new object[,]
                {
                    { 1, "Schnick 1", "D" },
                    { 28, "Hotel 7", "D" },
                    { 27, "Hotel 6", "D" },
                    { 26, "Hotel 5", "D" },
                    { 25, "Hotel 4", "D" },
                    { 24, "Hotel 3", "D" },
                    { 23, "Hotel 2", "D" },
                    { 22, "Hotel 1", "D" },
                    { 21, "Dining Hall 5", "D" },
                    { 20, "Dining Hall 4", "D" },
                    { 19, "Dining Hall 3", "D" },
                    { 18, "Dining Hall 2", "D" },
                    { 17, "Dining Hall 1", "D" },
                    { 16, "Sanasac 4", "D" },
                    { 15, "Sanasac 3", "D" },
                    { 14, "Sanasac 2", "D" },
                    { 13, "Sanasac 1", "D" },
                    { 12, "RV Park", "D" },
                    { 11, "Chetek", "D" },
                    { 10, "New Lisbon 2", "D" },
                    { 9, "New Lisbon 1", "D" },
                    { 8, "Tomah 4", "D" },
                    { 7, "Tomah 3", "D" },
                    { 6, "Tomah 2", "D" },
                    { 5, "Tomah 1", "D" },
                    { 4, "Schnick 4", "D" },
                    { 3, "Schnick 3", "D" },
                    { 2, "Schnick 2", "D" },
                    { 29, "Hotel 8", "D" },
                    { 30, "None", "D" }
                });

            migrationBuilder.InsertData(
                table: "events",
                columns: new[] { "event_id", "Address", "BeddingId", "City", "DateUpdate", "Email", "FirstName", "LastName", "Message", "NumberGuest", "Phone", "ReasonId", "RoomsId", "State", "UserId", "Zip", "all_day", "description", "event_end", "event_start", "sendEmail", "title" },
                values: new object[] { 1, "730 Lakeview Drive", "A", "Chetek", null, "pr@campchetek.org", "Randy", "Tanis", "", 2, "715 924-3236", "J", 30, "WI", 1, 54728, false, "test ", new DateTime(2020, 8, 12, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2020, 8, 12, 14, 57, 32, 492, DateTimeKind.Local).AddTicks(9935), false, "Test " });

            migrationBuilder.InsertData(
                table: "events",
                columns: new[] { "event_id", "Address", "BeddingId", "City", "DateUpdate", "Email", "FirstName", "LastName", "Message", "NumberGuest", "Phone", "ReasonId", "RoomsId", "State", "UserId", "Zip", "all_day", "description", "event_end", "event_start", "sendEmail", "title" },
                values: new object[] { 2, "730 Lakeview Drive", "B", "Chetek", null, "noah@campchetek.org", "Noah", "Rost", "", 1, "715 924-3236", "J", 30, "WI", 2, 54728, false, "test2 ", new DateTime(2020, 8, 12, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2020, 8, 12, 14, 57, 32, 499, DateTimeKind.Local).AddTicks(862), false, "Test2 " });

            migrationBuilder.CreateIndex(
                name: "IX_events_BeddingId",
                table: "events",
                column: "BeddingId");

            migrationBuilder.CreateIndex(
                name: "IX_events_ReasonId",
                table: "events",
                column: "ReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_events_RoomsId",
                table: "events",
                column: "RoomsId");

            migrationBuilder.CreateIndex(
                name: "IX_events_UserId",
                table: "events",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_StatusId",
                table: "Room",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "beddings");

            migrationBuilder.DropTable(
                name: "Reasons");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Statuses");
        }
    }
}
