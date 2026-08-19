using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class SyncLearningTopicModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LearningTopics",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "LearningTopics");
        }
    }
}
