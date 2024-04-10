using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace demo.WebApiDotNetCore8.Migrations
{
    /// <inheritdoc />
    public partial class Addsp_getallcourts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         migrationBuilder.Sql(@"

         USE[WebApiDotNetCore8]
         GO
         SET ANSI_NULLS ON
         GO
         SET QUOTED_IDENTIFIER ON
         GO
         CREATE PROCEDURE[dbo].[sp_GetAllCourts]
         AS
         BEGIN

         SET NOCOUNT ON;

         SELECT*
         FROM dbo.Courts
         END
         GO

         ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
