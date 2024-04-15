using DineSpotterRestaurantManagement.Context;
using DineSpotterRestaurantManagement.Models;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Services.Interface;

namespace RestaurantReservation.Services.Implementation
{
    public class RestaurantService : IRestaurantService
    {
        private readonly appDbContext _context;

        public RestaurantService(appDbContext context)
        {
            _context = context;
        }

        public async Task<Restaurant> AddRestaurantAsync(string name, string description, Location location, Availability availability, List<Cuisine> cuisines, string Image, string MenuLink)
        {
            var restaurant = new Restaurant
            {
                Name = name,
                Description = description,
                Location = location,
                Availability = availability,
                Cuisines = cuisines,
                Image = Image,
                MenuLink = MenuLink
            };

            _context.Add(restaurant);
            await _context.SaveChangesAsync();
            return restaurant;
        }
        public async Task<List<Restaurant>> GetRestaurantsAsync()
        {
            return await _context.restaurants.Include(x => x.Location).Include(x => x.Availability).Include(x => x.Cuisines).ToListAsync();
        }

        public async Task<Restaurant> GetRestaurantByIdAsync(int id)
        {
            return await _context.restaurants.Include(x => x.Location).Include(x => x.Availability).Include(x => x.Cuisines).FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<bool> UpdateRestaurantAsync(int id, string name, string description, Location location, Availability availability, List<Cuisine> cuisines, string image, string MenuLink)
        {
            var restaurant = await _context.restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return false;
            }

            restaurant.Name = name;
            restaurant.Description = description;
            restaurant.Location = location;
            restaurant.Availability = availability;
            restaurant.Cuisines = cuisines;
            restaurant.Image = image;
            restaurant.MenuLink = MenuLink;

            _context.Entry(restaurant).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRestaurantAsync(int id)
        {


            var restaurant = await _context.restaurants.Include(x => x.Cuisines).Include(x => x.Location).Include(x => x.Availability).FirstOrDefaultAsync(x => x.Id == id);
            if (restaurant == null)
            {
                return false;
            }

            _context.restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Restaurant>> SearchRestaurantsAsync(string query)
        {
            return await _context.restaurants
                .Include(r => r.Cuisines)
                .Where(r => r.Name.ToLower().Contains(query.ToLower()) ||
                            r.Cuisines.Any(c => c.Name.ToLower().Contains(query.ToLower())))
                .ToListAsync();
        }
        public async Task<List<Restaurant>> FilterRestaurantsAsync(string query)
        {
            return await _context.restaurants
                .Include(r => r.Cuisines)
                .Where(r => r.Name.ToLower().Contains(query.ToLower()) ||
                            r.Cuisines.Any(c => c.Name.ToLower().Contains(query.ToLower())))
                .ToListAsync();
        }
    }
}
