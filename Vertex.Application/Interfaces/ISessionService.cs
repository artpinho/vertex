using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;
using Vertex.Application.DTOs;

namespace Vertex.Application.Interfaces
{
    public  interface ISessionService
    {
        Task<SessionResponse> StartSessionAsync(int customerId, int stationId);
        Task<SessionResponse> EndSessionAsync(int sessionId);    
        Task<List<SessionHistoryResponse>> GetHistoryAsync(int customerId);
    }
}
