using SchedulingEngine.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingEngine.Services.Contracts
{
    public interface ICalculator
    {
        double calculateNorthSouthDuration(FlowSegmentDataResponse flowSegmentData);
        double calculateEastWestDuration(FlowSegmentDataResponse flowSegmentData);
    }
}
