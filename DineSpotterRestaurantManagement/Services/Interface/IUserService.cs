using DineSpotterRestaurantManagement.Models;

namespace RestaurantReservation.Services.Interface
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(string name, string lastName, string username, string email, string password, string phoneNumber);
        Task<User> LoginUserAsync(string username, string password);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByPhoneNumberAsync(string phoneNumber);
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(int id, string name, string lastName, string username, string email, string password, string phoneNumber);
        Task<bool> DeleteUserAsync(int id);
        Task<string> HashPasswordAsync(string password);
        Task<bool> VerifyPasswordAsync(string password, string hashedPassword);
        Task<bool> CheckPasswordStrength(string password);
    }
}
