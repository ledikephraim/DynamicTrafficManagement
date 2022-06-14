using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingEngine.Data.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Coordinate
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Coordinates
    {
        public List<Coordinate> coordinate { get; set; }
    }

    public class FlowSegmentData
    {
        public string frc { get; set; }
        public int currentSpeed { get; set; }
        public int freeFlowSpeed { get; set; }
        public int currentTravelTime { get; set; }
        public int freeFlowTravelTime { get; set; }
        public int confidence { get; set; }
        public bool roadClosure { get; set; }
        public Coordinates coordinates { get; set; }

       // [JsonProperty("@version")]
        public string Version { get; set; }
    }

    public class FlowSegmentDataResponse
    {
        public FlowSegmentData flowSegmentData { get; set; }
    }


}
