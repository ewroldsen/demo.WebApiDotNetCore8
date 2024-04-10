using demo.WebApiDotNetCore8.Data;
using demo.WebApiDotNetCore8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demo.WebApiDotNetCore8.Controllers
{

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
     /// This Enpoint Gets All Courts
     /// </summary>
     /// <response code="200">Returns all courts</response>
      [HttpGet("api/Get-Courts")]
      public ActionResult<IEnumerable<Court>> Get()
      {
         return _context.Courts;
      }

      [HttpGet("api/Get-CourtById/{id}")]

      public async Task<ActionResult<Court?>> GetById(int id)
      {
         return await _context.Courts.Where(c => c.Id == id).FirstOrDefaultAsync();
      }

      [HttpPost("api/Add-Court")]
      public async Task<ActionResult<Court?>> Create(Court court)
      {
         await _context.Courts.AddAsync(court);
         await _context.SaveChangesAsync();
         return CreatedAtAction(nameof(GetById), new { id = court.Id }, court);
      }

      [HttpPut("api/Update-Court")]
      public async Task<ActionResult> Update(Court court)
      {
         _context.Courts.Update(court);
         await _context.SaveChangesAsync();
         return Ok();
      }
      /// <summary>
      /// This Endpoint Deletes a Court
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpDelete("api/Delete-Court/{id}")]
      public async Task<ActionResult<Court?>> Delete(int id)
      {
         var result = await GetById(id);

         if (result.Value == null)
            return NotFound();

         _context.Courts.Remove(result.Value);
         await _context.SaveChangesAsync();

         return Ok();

      }

   }
}

