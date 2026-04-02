using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Enums;

namespace Vertex.Application.DTOs
{
    public class StationResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public StationStatus Status { get; set; }
    }
}
