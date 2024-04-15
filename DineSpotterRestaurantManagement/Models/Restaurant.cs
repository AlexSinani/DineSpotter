using System.ComponentModel.DataAnnotations;

namespace DineSpotterRestaurantManagement.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Image { get; set; }
        public Location Location { get; set; }
        public Availability Availability { get; set; }
        public List<Cuisine> Cuisines { get; set; }
        public string MenuLink { get; set; } = "";
    }
}
