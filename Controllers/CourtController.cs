using demo.WebApiDotNetCore8.Data;
using demo.WebApiDotNetCore8.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demo.WebApiDotNetCore8.ViewModels;

namespace demo.WebApiDotNetCore8.Controllers
{
   //[Authorize]
   [ApiController]
   [Route("api/[controller]")]
   [ApiVersion("1.0")]
   [Produces("application/json")]
   public class CourtController : Controller
   {

      private readonly DataContext _context;

      public CourtController(DataContext context)
      {
         _context = context;
      }
      /// <summary>
      /// This Enpoint Gets All Courts info
      /// </summary>
      /// <response code="200">Returns all courts</response>
      [HttpGet("Get-Courts")]
      public ActionResult<IEnumerable<Court>> Get()
      {
         // Use Stored Procedure
         return _context.Courts.FromSqlRaw("EXEC sp_GetAllCourts").ToList();

         // Use EF
         //return _context.Courts;
      }

      /// <summary>
      /// This Endpoint Gets All Court Locations
      /// </summary>
      [HttpGet("Get-All-Court-Locations")]
      public ActionResult<ICollection<CourtLocationsVM>> GetAllCourtLocations()
      {
         return _context.Courts.Select(d => new CourtLocationsVM { Id = d.Id, Latitude = d.Latitude, Longitude = d.Longitude }).ToList();
      }

      /// <summary>
      /// This Endpoint Gets a Court by Id
      /// </summary>
      [HttpGet("Get-CourtById/{Id}")]

      public async Task<ActionResult<Court?>> GetById(int Id)
      {
         // Use Stored Procedure
         var courts = await _context.Courts.FromSqlInterpolated($"EXEC sp_GetCourtById {Id}").ToListAsync();
         if (courts.Count == 0)
         {
            return NotFound();
         }
         return Ok(courts.First());

         // Use EF
         //return await _context.Courts.Where(c => c.Id == id).FirstOrDefaultAsync();
      }
      /// <summary>
      /// This Endpoint Creates a new Court
      /// </summary>
      [HttpPost("Add-Court")]
      public async Task<ActionResult<Court?>> Create(Court court)
      {
         await _context.Courts.AddAsync(court);
         await _context.SaveChangesAsync();
         return CreatedAtAction(nameof(GetById), new { Id = court.Id }, court);
      }
      /// <summary>
      /// This Endpoint Updates a Court
      /// </summary>
      [HttpPut("Update-Court")]
      public async Task<ActionResult> Update(Court court)
      {
         _context.Courts.Update(court);
         await _context.SaveChangesAsync();
         return Ok();
      }
      /// <summary>
      /// This Endpoint Deletes a Court
      /// </summary>
      [HttpDelete("Delete-Court/{Id}")]
      public async Task<ActionResult<Court?>> Delete(int Id)
      {
         var result = await GetById(Id);

         if (result.Value == null)
            return NotFound();

         _context.Courts.Remove(result.Value);
         await _context.SaveChangesAsync();

         return Ok();
      }
   }
}

