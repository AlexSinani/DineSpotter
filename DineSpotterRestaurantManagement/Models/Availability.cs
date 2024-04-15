using System.ComponentModel.DataAnnotations;

namespace DineSpotterRestaurantManagement.Models
{
    public class Availability
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string OpeningHours { get; set; } = "";
        [StringLength(50)]
        public string ClosingHours { get; set; } = "";
    }
}
