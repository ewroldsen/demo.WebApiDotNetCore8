using System.ComponentModel.DataAnnotations;

namespace demo.WebApiDotNetCore8.ViewModels
{
   public class CourtLocationsVM
   {
      public int Id { get; set; }
      public string? Latitude { get; set; }
      public string? Longitude { get; set; }
   }
}
