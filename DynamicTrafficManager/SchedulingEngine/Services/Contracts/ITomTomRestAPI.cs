using SchedulingEngine.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingEngine.Services.Contracts
{
    public interface ITomTomRestAPI
    {
        FlowSegmentDataResponse GetFlowSegmentDataResponse(Coordinate coordinate);
    }
}
