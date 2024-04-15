using DineSpotterRestaurantManagement.Context;
using DineSpotterRestaurantManagement.Helpers;
using DineSpotterRestaurantManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace RestaurantReservation.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly appDbContext _context;

        public UserService(appDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterUserAsync(string name, string lastName, string username, string email, string password, string phoneNumber)
        {
            var newUser = new User
            {
                Name = name,
                LastName = lastName,
                Username = username,
                Email = email,
                Password = password,
                PhoneNumber = phoneNumber
            };

            _context.users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<User> LoginUserAsync(string username,  string password)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                throw new ApplicationException("Invalid username, or password.");
            }

            return user;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.users.FindAsync(id);
        }

        public async Task<bool> UpdateUserAsync(int id, string name, string lastName, string username, string email, string password, string phoneNumber)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            user.Name = name;
            user.LastName = lastName;
            user.Username = username;
            user.Email = email;
            user.Password = password;
            user.PhoneNumber = phoneNumber;

            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<string> HashPasswordAsync(string password)
        {
            return PasswordHasher.HashPassword(password);
        }

        public async Task<bool> VerifyPasswordAsync(string password, string hashedPassword)
        {
            return PasswordHasher.VerifyPassword(password, hashedPassword);
        }
        public async Task<bool> CheckPasswordStrength(string password)
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");
            return regex.IsMatch(password);
        }

    }
}