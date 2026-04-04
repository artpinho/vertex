using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.DTOs
{
    public class RevenueChartResponse
    {
        public DateTime Date { get; set; }
        public decimal Revenue { get; set; }
        public int Sessions { get; set; }
    }
}
