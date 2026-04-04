using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.DTOs;

namespace Vertex.Application.Interfaces
{
    public interface IDashboardService
    {
        Task <DashboardResponse> GetAsync();
    }
}
