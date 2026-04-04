using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.DTOs
{
    public class DashboardResponse
    {
        public int TotalCustomers { get; set; }
        public int TotalStations { get; set; }
        public int StationsInUse { get; set; }
        public int ActiveSessions { get; set; }
        public decimal TodayRevenue { get; set; }
    }
}
