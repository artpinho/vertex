using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;

namespace Vertex.Application.Interfaces
{
    public  interface ISessionService
    {
        Task<Session> StartSessionAsync(int customerId, int stationId);
        Task<Session> EndSessionAsync(int sessionId);    
    }
}
