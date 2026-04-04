using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.API.Hubs;
using Vertex.Application.Interfaces;

namespace Vertex.Application.Services
{
    public class SignalRRealtimeService : IRealtimeService
    {
        private readonly IHubContext<DashboardHub> _hubContext;

        public SignalRRealtimeService(IHubContext<DashboardHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task NotifySessionStarted(object data)
        {
            return _hubContext.Clients.All.SendAsync("SessionStarted", data);
        }

        public Task NotifySessionEnded(object data)
        {
            return _hubContext.Clients.All.SendAsync("SessionEnded", data);
        }
    }
}
