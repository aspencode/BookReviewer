using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookReviewer.Migrations
{
    /// <inheritdoc />
    public partial class AddAverageRatingFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE OR REPLACE FUNCTION calculate_average_rating(book_id_param INT)
            RETURNS NUMERIC AS $$
            BEGIN
                RETURN (SELECT AVG(rating) 
                        FROM ""Reviews"" 
                        WHERE ""BookId"" = book_id_param);
            END;
            $$ LANGUAGE plpgsql;");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS calculate_average_rating(INT);");
        }
    }
}
