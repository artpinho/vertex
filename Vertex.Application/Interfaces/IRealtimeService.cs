using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Interfaces
{
    public interface IRealtimeService
    {
        Task NotifySessionStarted(object data);
        Task NotifySessionEnded(object data);
    }
}
