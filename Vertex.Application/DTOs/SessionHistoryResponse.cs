using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.DTOs
{
    public class SessionHistoryResponse
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EnndTime { get; set; }
        public int DurationInMinutes { get; set; }
        public decimal AmountCharged { get; set; }
        public string StationName { get; set; } = string.Empty;
    }
}
