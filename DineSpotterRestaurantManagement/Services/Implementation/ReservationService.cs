using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DineSpotterRestaurantManagement.Context;
using DineSpotterRestaurantManagement.Models;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Services.Interface;

namespace RestaurantReservation.Services.Implementation
{
    public class ReservationService : IReservationService
    {
        private readonly appDbContext _context;
        //private readonly IEmailService _emailService;
        public ReservationService(appDbContext context/*, IEmailService emailService*/)
        {
            _context = context;
            //_emailService = emailService;
        }
        public async Task<Reservation> BookTableAsync(int RestaurantId, int UserId, string Email, int NumberOfGuests, DateTime ReservationDate, string notes, bool Confirmed)
        {
            var reservation = new Reservation
            {
                RestaurantId = RestaurantId,
                UserId = UserId,
                Email = Email,
                NumberOfGuests = NumberOfGuests,
                ReservationDate = ReservationDate,
                Notes = notes,
                Confirmed = Confirmed
            };

            _context.Add(reservation);
            await _context.SaveChangesAsync();

            //Send email confirmation
           //await _emailService.SendEmailAsync(Email, "Table Booking Confirmation", "Your table has been successfully booked.");

            return reservation;
        }

        public async Task<List<Reservation>> GetReservationsAsync()
        {
            return await _context.reservations.ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            return await _context.reservations.FindAsync(id);
        }

        public async Task<bool> ConfirmReservationAsync(int id)
        {
            var reservation = await _context.reservations.FindAsync(id);
            if (reservation != null)
            {
                reservation.Confirmed = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            var reservation = await _context.reservations.FindAsync(id);
            if (reservation == null)
            {
                return false;
            }

            _context.reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
