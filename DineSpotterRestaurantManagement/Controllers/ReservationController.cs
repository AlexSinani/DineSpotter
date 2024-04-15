using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Services.Implementation;
using RestaurantReservation.Services.Interface;
using DineSpotterRestaurantManagement.Models;
using System.Security.Claims;
//using DineSpotterRestaurantManagement.Services.Interface;

namespace RestaurantReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        //private readonly IEmailService _emailService;
        //private readonly IAuthService _authService;

        public ReservationController(IReservationService reservationService/*, IAuthService authService, IEmailService emailService*/)
        {
            _reservationService = reservationService;
            //_authService = authService;
            //_emailService = emailService;
        }

        public record BookTableRequest(int RestaurantId, int UserId, string Email, int NumberOfGuests, DateTime ReservationDate, string Notes, bool Confirmed)
        {
        }

        [HttpPost("book")]
        public async Task<IActionResult> BookTable([FromBody] BookTableRequest request)
        {
            try
            {
             
                //var restaurantId = _authService.GetRestaurantIdFromLoggedInUser();
                //var userId = _authService.GetUserIdFromLoggedInUser();

                var bookedReservation = await _reservationService.BookTableAsync(request.RestaurantId, request.UserId, request.Email, request.NumberOfGuests, request.ReservationDate, request.Notes, request.Confirmed);

                // Send email confirmation
                //await _emailService.SendEmailAsync(request.Email, "Table Booking Confirmation", "Your table has been successfully booked.");

                return Ok(bookedReservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetReservations()
        {
            var reservations = await _reservationService.GetReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetReservation(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        public record ConfirmReservationRequest(int ReservationId)
        {
        }

        [HttpPost("confirm/{id}")]
        public async Task<IActionResult> ConfirmReservation(int id)
        {
            var result = await _reservationService.ConfirmReservationAsync(id);
            if (result)
            {
                return Ok("Reservation confirmed.");
            }
            return NotFound();
        }

        public record DeleteReservationRequest(int ReservationId)
        {
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var result = await _reservationService.DeleteReservationAsync(id);
            if (result)
            {
                return Ok("Reservation deleted.");
            }
            return NotFound();
        }
    }
}