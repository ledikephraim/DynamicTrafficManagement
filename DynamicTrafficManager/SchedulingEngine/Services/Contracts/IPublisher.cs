using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingEngine.Services.Contracts
{
    public interface IPublisher
    {
        Task publishToIntersection(string topic);
    }
}
