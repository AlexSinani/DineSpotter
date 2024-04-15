using System.ComponentModel.DataAnnotations;

namespace DineSpotterRestaurantManagement.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Address { get; set; } = "";
        [StringLength(50)]
        public string City { get; set; } = "";
    }
}
