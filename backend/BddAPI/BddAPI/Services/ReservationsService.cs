using AutoMapper;
using BddAPI.DTOs.Request;
using BddAPI.DTOs.Response;
using BddAPI.Exceptions;
using BddAPI.Models;
using BddAPI.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace BddAPI.Services;

public interface IReservationsService
{
    Task<ReservationResponseDto> CreateReservationAsync(ReservationRequestDto reservationRequestDto);
    Task<ReservationResponseDto> GetReservationAsync(Guid reservationId);
    Task<IEnumerable<ReservationResponseDto>> GetReservationsAsync(int quantity);
    Task<IEnumerable<ReservationResponseDto>> GetUserReservationsAsync(Guid userId);
    Task<IEnumerable<ReservationResponseDto>> GetReservationsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<ReservationResponseDto> UpdateReservationAsync(ReservationRequestDto reservationRequestDto, Guid id);
    Task DeleteReservationAsync(Guid reservationId);
}

public class ReservationsService(IReservationsRepository reservationsRepository, IMapper mapper) : IReservationsService
{
    public async Task<ReservationResponseDto> CreateReservationAsync(ReservationRequestDto reservationRequestDto)
    {
        var reservation = mapper.Map<Reservation>(reservationRequestDto);

        await reservationsRepository.CreateReservationAsync(reservation);
        await reservationsRepository.SaveChangesAsync();

        return mapper.Map<ReservationResponseDto>(reservation);
    }

    public async Task<ReservationResponseDto> GetReservationAsync(Guid reservationId)
    {
        if (await reservationsRepository.GetReservationAsync(reservationId) == null)
        {
            throw new NotFoundException($"Reservation with ID: {reservationId} not found");
        }

        var reservation = await reservationsRepository.GetReservationAsync(reservationId);

        return mapper.Map<ReservationResponseDto>(reservation);
    }

    public async Task<IEnumerable<ReservationResponseDto>> GetReservationsAsync(int quantity)
    {
        var reservations = await reservationsRepository.GetReservationsAsync(quantity);

        if (reservations.IsNullOrEmpty())
        {
            throw new NotFoundException("No reservations found");
        }

        return mapper.Map<IEnumerable<ReservationResponseDto>>(reservations);
    }

    public async Task<IEnumerable<ReservationResponseDto>> GetUserReservationsAsync(Guid userId)
    {
        var userReservations = await reservationsRepository.GetUserReservationsAsync(userId);
        
        if (userReservations.IsNullOrEmpty())
        {
            throw new NotFoundException("No reservations found for this user");
        }
        
        return mapper.Map<IEnumerable<ReservationResponseDto>>(userReservations);
    }

    public async Task<IEnumerable<ReservationResponseDto>> GetReservationsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var reservations = await reservationsRepository.GetReservationsByDateRangeAsync(startDate, endDate);

        if (reservations.IsNullOrEmpty())
        {
            throw new NotFoundException("No reservations found");
        }

        return mapper.Map<IEnumerable<ReservationResponseDto>>(reservations);
    }

    public async Task<ReservationResponseDto> UpdateReservationAsync(ReservationRequestDto reservationRequestDto, Guid id)
    {
        var reservation = await reservationsRepository.GetReservationAsync(id);

        if (reservation == null)
        {
            throw new NotFoundException($"Reservation with ID: {id} not found");
        }

        mapper.Map(reservationRequestDto, reservation);
        var updatedReservation = await reservationsRepository.UpdateReservationAsync(reservation);

        return mapper.Map<ReservationResponseDto>(updatedReservation);
    }

    public async Task DeleteReservationAsync(Guid reservationId)
    {
        await reservationsRepository.DeleteReservationAsync(reservationId);
        await reservationsRepository.SaveChangesAsync();
    }
}