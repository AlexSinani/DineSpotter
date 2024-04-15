//using DineSpotterRestaurantManagement.Services.Interface;
//using System.Security.Claims;

//namespace DineSpotterRestaurantManagement.Services.Implementation
//{
//    public class AuthService : IAuthService
//    {
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public AuthService(IHttpContextAccessor httpContextAccessor)
//        {
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public int GetRestaurantIdFromLoggedInUser()
//        {
//            var restaurantId = _httpContextAccessor.HttpContext.Session.GetInt32("RestaurantId");
//            return restaurantId ?? 0;
//        }

//        public int GetUserIdFromLoggedInUser()
//        {
//            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            return userId != null ? int.Parse(userId) : 0;
//        }
//    }
//}
