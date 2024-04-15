using DineSpotterRestaurantManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Services.Interface;

namespace RestaurantReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        public record AddRestaurantRequest(string Name, string Description, Location Location, Availability Availability, List<Cuisine> Cuisines, string image, string MenuLink)
        {
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddRestaurant([FromBody] AddRestaurantRequest request)
        {
            try
            {
                var addedRestaurant = await _restaurantService.AddRestaurantAsync(request.Name, request.Description, request.Location, request.Availability, request.Cuisines, request.image, request.MenuLink);
                return Ok(addedRestaurant);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurants = await _restaurantService.GetRestaurantsAsync();
            return Ok(restaurants);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetRestaurant(int id)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        public record UpdateRestaurantRequest(int Id, string Name, string Description, Location Location, Availability Availability, List<Cuisine> Cuisines, string image, string MenuLink)
        {
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateRestaurant([FromBody] UpdateRestaurantRequest update)
        {
            try
            {
                var result = await _restaurantService.UpdateRestaurantAsync(update.Id, update.Name, update.Description, update.Location, update.Availability, update.Cuisines, update.image, update.MenuLink);
                if (result)
                {
                    return Ok("Restaurant updated.");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var result = await _restaurantService.DeleteRestaurantAsync(id);
            if (result)
            {
                return Ok("Restaurant deleted.");
            }
            return NotFound();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> SearchRestaurants(string query)
        {
            try
            {
                var restaurants = await _restaurantService.SearchRestaurantsAsync(query);
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("filter")]
        public async Task<ActionResult<List<Restaurant>>> FilterRestaurants(string query)
        {
            return await _restaurantService.FilterRestaurantsAsync(query);
        }
    }
}
