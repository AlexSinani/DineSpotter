using DineSpotterRestaurantManagement.Models;

namespace RestaurantReservation.Services.Interface
{
    public interface IRestaurantService
    {
        Task<Restaurant> AddRestaurantAsync(string name, string description, Location location, Availability availability, List<Cuisine> cuisines, string Image, string MenuLink);
        Task<List<Restaurant>> GetRestaurantsAsync();
        Task<Restaurant> GetRestaurantByIdAsync(int id);
        Task<bool> UpdateRestaurantAsync(int id, string name, string description, Location location, Availability availability, List<Cuisine> cuisines, string Image, string MenuLink);
        Task<bool> DeleteRestaurantAsync(int id);
        Task<List<Restaurant>> SearchRestaurantsAsync(string query);
        Task<List<Restaurant>> FilterRestaurantsAsync(string query);
    }
}
