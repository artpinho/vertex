using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.DTOs;

namespace Vertex.Application.Interfaces
{
    public interface IStationService
    {
        Task<StationResponse> CreateAsync(CreateStationRequest request);
        Task<List<StationResponse>> GetAllAsync();
        Task<StationResponse?> GetByIdAsync(int id);
    }
}
