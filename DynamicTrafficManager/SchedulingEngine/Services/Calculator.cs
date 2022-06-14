using SchedulingEngine.Data.Models;
using SchedulingEngine.Services.Contracts;
using SchedulingEngine.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingEngine.Services
{
    public class Calculator : ICalculator
    {
        public double calculateEastWestDuration(FlowSegmentDataResponse flowSegmentData)
        {
            return calculatePassDuration(flowSegmentData);
        }

        public double calculateNorthSouthDuration(FlowSegmentDataResponse flowSegmentData)
        {
            return calculatePassDuration(flowSegmentData);
        }
        private double calculatePassDuration(FlowSegmentDataResponse flowSegmentData)
        {
            //ToDo: Update calculation
            var change = flowSegmentData.flowSegmentData.currentSpeed / flowSegmentData.flowSegmentData.freeFlowSpeed * 100;
            if (change > Constants.GLOBAL_TRAVEL_SPEED_THRESHOLD)
            {
                return change * flowSegmentData.flowSegmentData.currentSpeed;
            }
            return Constants.DEFAULT_SIGNAL_DURATION;
        }
    }
}
