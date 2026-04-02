using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.DTOs
{
    public class SessionResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int StationId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int DurationInMinutes { get; set; }
        public decimal AmountCharged { get; set; }
        public bool IsActive { get; set; }
    }
}
