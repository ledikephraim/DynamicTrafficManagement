using SchedulingEngine.Services.Contracts;
using SchedulingEngine.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingEngine.Services
{
    public class Publisher : IPublisher
    {
        public async Task publishToIntersection(string topic, string payload)
        {
            await MQTTUtil.ConnectAsync(topic, payload);
         
        }
    }
}
