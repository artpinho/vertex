using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Domain.Entities
{
    public class Session
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int StationId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int DurationMinutes { get; set; }
        public decimal AmountCharged { get; set; }

        [NotMapped]
        public bool IsActive => EndTime == null;
    }
}
