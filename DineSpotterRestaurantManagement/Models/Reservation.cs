using System.ComponentModel.DataAnnotations;

namespace DineSpotterRestaurantManagement.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int UserId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public bool Confirmed { get; set; }
    }
}
