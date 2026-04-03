using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Interfaces;
using Vertex.Infrastructure.Data;
using Vertex.Domain.Entities;
using Vertex.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using Vertex.Application.DTOs;

namespace Vertex.Application.Services
{
    public class SessionService : ISessionService
    {
        private readonly AppDbContext _context;

        private const decimal PricePerHour = 5.0m; // Preço por hora
        public SessionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SessionResponse> StartSessionAsync(int customerId, int stationId)
        {
            var station = await _context.Stations
                .FirstOrDefaultAsync(s => s.Id == stationId);

            if (station == null)
                throw new Exception("Estação não encontrada.");
            if (station.Status != StationStatus.Available)
                throw new Exception("Estação não está disponível.");

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == customerId);

            if (customer == null)
                throw new Exception("Cliente não encontrado.");

            var session = new Session
            {
                CustomerId = customerId,
                StationId = stationId,
                StartTime = DateTime.UtcNow
            };

            station.Status = StationStatus.InUse;

            _context.Sessions.Add(session);

            await _context.SaveChangesAsync();

            return new SessionResponse
            {
                Id = session.Id,
                CustomerId = session.CustomerId,
                StationId = session.StationId,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                DurationInMinutes = session.DurationMinutes,
                AmountCharged = session.AmountCharged,
                IsActive = true
            };
        }

        public async Task<SessionResponse> EndSessionAsync(int sessionId)
        {
            var session = await _context.Sessions
                    .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null)
                throw new Exception("Sessão não encontrada.");

            if (session.EndTime != null)
                throw new Exception("Sessão já foi finalizada.");

            session.EndTime = DateTime.UtcNow;

            var duration = session.EndTime.Value - session.StartTime;

            session.DurationMinutes = (int)duration.TotalMinutes;

            session.AmountCharged = (decimal)duration.TotalHours * PricePerHour; // Cálculo do valor a ser cobrado

            var station = await _context.Stations.FindAsync(session.StationId);

            if (station != null)
                station.Status = StationStatus.Available;

            await _context.SaveChangesAsync();

            return new SessionResponse
            {
                Id = session.Id,
                CustomerId = session.CustomerId,
                StationId = session.StationId,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                DurationInMinutes = session.DurationMinutes,
                AmountCharged = session.AmountCharged,
                IsActive = false
            };
        }
    }
}
