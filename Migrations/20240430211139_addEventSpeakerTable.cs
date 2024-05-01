using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHome.Migrations
{
    /// <inheritdoc />
    public partial class addEventSpeakerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSpeaker_Events_EventId",
                table: "EventSpeaker");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpeaker_Speakers_SpeakerId",
                table: "EventSpeaker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventSpeaker",
                table: "EventSpeaker");

            migrationBuilder.RenameTable(
                name: "EventSpeaker",
                newName: "EventsSpeaker");

            migrationBuilder.RenameIndex(
                name: "IX_EventSpeaker_SpeakerId",
                table: "EventsSpeaker",
                newName: "IX_EventsSpeaker_SpeakerId");

            migrationBuilder.RenameIndex(
                name: "IX_EventSpeaker_EventId",
                table: "EventsSpeaker",
                newName: "IX_EventsSpeaker_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventsSpeaker",
                table: "EventsSpeaker",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventsSpeaker_Events_EventId",
                table: "EventsSpeaker",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventsSpeaker_Speakers_SpeakerId",
                table: "EventsSpeaker",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventsSpeaker_Events_EventId",
                table: "EventsSpeaker");

            migrationBuilder.DropForeignKey(
                name: "FK_EventsSpeaker_Speakers_SpeakerId",
                table: "EventsSpeaker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventsSpeaker",
                table: "EventsSpeaker");

            migrationBuilder.RenameTable(
                name: "EventsSpeaker",
                newName: "EventSpeaker");

            migrationBuilder.RenameIndex(
                name: "IX_EventsSpeaker_SpeakerId",
                table: "EventSpeaker",
                newName: "IX_EventSpeaker_SpeakerId");

            migrationBuilder.RenameIndex(
                name: "IX_EventsSpeaker_EventId",
                table: "EventSpeaker",
                newName: "IX_EventSpeaker_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventSpeaker",
                table: "EventSpeaker",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpeaker_Events_EventId",
                table: "EventSpeaker",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpeaker_Speakers_SpeakerId",
                table: "EventSpeaker",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
