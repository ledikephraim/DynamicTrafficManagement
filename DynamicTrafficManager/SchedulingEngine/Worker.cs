using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SchedulingEngine.Data.Models;
using SchedulingEngine.Services.Contracts;

namespace SchedulingEngine
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public IServiceProvider Services { get; }
        private ITomTomRestAPI _tomTomRestAPI;
        private ICalculator _calculator;
        private IPublisher _publisher;

        public Worker(ILogger<Worker> logger,
            IServiceProvider services
        )
        {
            Services = services;
            _logger = logger;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                using (var scope = Services.CreateScope())
                {
                    _tomTomRestAPI =
                        scope.ServiceProvider
                            .GetRequiredService<ITomTomRestAPI>();

                    _calculator =
                       scope.ServiceProvider
                           .GetRequiredService<ICalculator>();

                    _publisher =
                       scope.ServiceProvider
                           .GetRequiredService<IPublisher>();

                    //await scopedProcessingService.DoWork(stoppingToken);
                    var coordinate = new Coordinate
                    {
                        longitude = 52.41072,
                        latitude = 4.84239
                    };
                    var flowData = _tomTomRestAPI.GetFlowSegmentDataResponse(coordinate);
                    var northSouthDelay = _calculator.calculateNorthSouthDuration(flowData);
                    var eastWestDelay = _calculator.calculateNorthSouthDuration(flowData);
                    await _publisher.publishToIntersection("test", $"NS={northSouthDelay}|EW={eastWestDelay}");
                }
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await Task.Delay(30000, stoppingToken);
            }
        }
    }
}
