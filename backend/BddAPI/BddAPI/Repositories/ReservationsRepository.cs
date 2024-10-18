using BddAPI.Data;
using BddAPI.Exceptions;
using BddAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BddAPI.Repositories;

public interface IReservationsRepository
{
    Task<Reservation> CreateReservationAsync(Reservation reservation);
    Task<Reservation?> GetReservationAsync(Guid id);
    Task<List<Reservation>?> GetReservationsAsync(int quantity);
    Task<List<Reservation>?> GetUserReservationsAsync(Guid userId);
    Task<List<Reservation>?> GetReservationsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<Reservation> UpdateReservationAsync(Reservation reservation);
    Task DeleteReservationAsync(Guid id);
    Task SaveChangesAsync();
}

public class ReservationsRepository(BddDbContext dbContext) : IReservationsRepository
{
    public async Task<Reservation> CreateReservationAsync(Reservation reservation)
    {
        await dbContext.Reservations.AddAsync(reservation);
        return reservation;
    }

    public async Task<Reservation?> GetReservationAsync(Guid id)
    {
        return await dbContext.Reservations.FindAsync(id);
    }

    public async Task<List<Reservation>?> GetReservationsAsync(int quantity)
    {
        if (quantity == 0)
        {
            return await dbContext.Reservations.ToListAsync();
        }

        return await dbContext.Reservations.Take(quantity).ToListAsync();
    }

    public async Task<List<Reservation>?> GetUserReservationsAsync(Guid userId)
    {
        return await dbContext.Reservations.Include(r => r.CommunityCenter).Where(r => r.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<Reservation>?> GetReservationsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }

    public async Task<Reservation> UpdateReservationAsync(Reservation reservation)
    {
        dbContext.Reservations.Update(reservation);
        await dbContext.SaveChangesAsync();
        return reservation;
    }

    public async Task DeleteReservationAsync(Guid id)
    {
        var reservation = await dbContext.Reservations.FindAsync(id);

        if (reservation == null)
        {
            throw new NotFoundException($"Reservation with ID: {id} not found");
        }

        dbContext.Reservations.Remove(reservation);
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}