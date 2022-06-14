using Microsoft.Extensions.Configuration;
using RestSharp;
using SchedulingEngine.Data.Models;
using SchedulingEngine.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingEngine.APIClients
{
    public class TomTomRestAPI : ITomTomRestAPI
    {
        public readonly IConfiguration configuration;
        public APIClientConfiguration APIClientConfiguration;
        public TomTomRestAPI(IConfiguration configuration)
        {
            this.configuration = configuration;
            APIClientConfiguration = this.configuration.GetSection("TrafficDataSources")
                .GetSection("TomTomAPI").Get< APIClientConfiguration>();
        }
        public FlowSegmentDataResponse GetFlowSegmentDataResponse(Coordinate coordinate)
        {
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = "https";
            uriBuilder.Host = APIClientConfiguration.BaseUrl;
            uriBuilder.Path = $"flowSegmentData/relative0/10/json";
            uriBuilder.Query = $"point=52.41072%2C4.84239&unit=KMPH&openLr=false&key={APIClientConfiguration.APIKey}"; 
            RestClientOptions restClientOptions = new RestClientOptions(uriBuilder.Uri);
            
            var client = new RestClient(restClientOptions);
            var response = client.Execute<FlowSegmentDataResponse>(new RestRequest());
            return response.Data;
        }
    }
}
