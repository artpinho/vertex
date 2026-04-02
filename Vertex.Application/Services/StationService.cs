using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vertex.Application.DTOs;
using Vertex.Application.Interfaces;
using Vertex.Domain.Entities;
using Vertex.Infrastructure.Data;
using Vertex.Domain.Enums;

namespace Vertex.Application.Services
{
    public class StationService : IStationService
    {
        private readonly AppDbContext _context;
        public StationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<StationResponse> CreateAsync(CreateStationRequest request)
        {
            var station = new Station
            {
                Name = request.Name,
                Status = StationStatus.Available
            };

            _context.Stations.Add(station);

            await _context.SaveChangesAsync();

            return new StationResponse
            {
                Id = station.Id,
                Name = station.Name,
                Status = station.Status
            };
        }

        public async Task<List<StationResponse>> GetAllAsync()
        {
            return await _context.Stations
                .Select(s => new StationResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Status = s.Status
                })
                .ToListAsync();
        }

        public async Task<StationResponse?> GetByIdAsync(int id)
        {
            return await _context.Stations
                .Where(s => s.Id == id)
                .Select(s => new StationResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Status = s.Status
                })
                .FirstOrDefaultAsync();
        }
    }
}
