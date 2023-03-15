using System.ComponentModel.DataAnnotations;

namespace BeagleStreetTest.Data.Models
{
    public class FavouriteModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Favourite { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
    }
}
