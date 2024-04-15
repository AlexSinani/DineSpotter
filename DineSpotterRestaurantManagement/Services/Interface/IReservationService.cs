using DineSpotterRestaurantManagement.Models;

namespace RestaurantReservation.Services.Interface
{
    public interface IReservationService
    {
        Task<Reservation> BookTableAsync(int RestaurantId, int UserId, string Email, int NumberOfGuests, DateTime ReservationDate, string notes, bool Confirmed);
        Task<List<Reservation>> GetReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(int id);
        Task<bool> ConfirmReservationAsync(int id);
        Task<bool> DeleteReservationAsync(int id);
    }
}
