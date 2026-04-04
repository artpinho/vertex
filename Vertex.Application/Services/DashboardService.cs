using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.DTOs;
using Vertex.Application.Interfaces;
using Vertex.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Vertex.Domain.Enums;

namespace Vertex.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;
        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardResponse> GetAsync()
        {
            var today = DateTime.UtcNow.Date;

            var totalCustomers = await _context.Customers.CountAsync();

            var totalStations = await _context.Stations.CountAsync();

            var stationsInUse = await _context.Stations
                .CountAsync(s => s.Status == StationStatus.InUse);

            var activeSessions = await _context.Sessions
               .CountAsync(s => s.EndTime == null);

            var todayRevenue = await _context.Sessions
                .Where(s => s.EndTime != null && s.EndTime.Value.Date == today)
                .SumAsync(s => (decimal?)s.AmountCharged) ?? 0;

            return new DashboardResponse
            {
                TotalCustomers = totalCustomers,
                TotalStations = totalStations,
                StationsInUse = stationsInUse,
                ActiveSessions = activeSessions,
                TodayRevenue = todayRevenue
            };

        }

        public async Task<List<RevenueChartResponse>> GetRevenueChartAsync(int days)
        {
            var startDate = DateTime.UtcNow.Date.AddDays(-days);

            var data = await _context.Sessions
                .Where(s => s.EndTime != null && s.EndTime.Value.Date >= startDate)
                .GroupBy(s => s.EndTime!.Value.Date)
                .Select(g => new RevenueChartResponse
                {
                    Date = g.Key,
                    Revenue = g.Sum(s => s.AmountCharged),
                    Sessions = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            return data;
        }
            
    }
        
}
