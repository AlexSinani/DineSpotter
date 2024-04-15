using System.ComponentModel.DataAnnotations;

namespace DineSpotterRestaurantManagement.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = "";
        [StringLength(50)]
        public string LastName { get; set; } = "";
        [StringLength(50)]
        public string Username { get; set; } = "";
        [StringLength(50)]
        public string Email { get; set; } = "";
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; } = "";
        [StringLength(50)]
        public string PhoneNumber { get; set; } = "";
    }
}
