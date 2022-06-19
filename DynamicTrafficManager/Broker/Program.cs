using MQTTnet;
using MQTTnet.Server;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Broker
{
    class Program
    {
        static void Main(string[] args)
        {
            //configure options
            MqttServerOptions options = new MqttServerOptions();
            var optionsBuilder = new MqttServerOptionsBuilder()

                .WithDefaultEndpoint()
                .WithConnectionBacklog(100)
                .WithDefaultEndpointPort(1884);



            //start server

            var mqttServer = new MqttFactory().CreateMqttServer(optionsBuilder.Build());
            mqttServer.InterceptingInboundPacketAsync += MqttServer_InterceptingInboundPacketAsync;
            mqttServer.StartedAsync += MqttServer_StartedAsync;
            mqttServer.StartAsync().Wait();

            Console.WriteLine($"Broker is Running");
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();

            //To keep the app running in container
            //https://stackoverflow.com/questions/38549006/docker-container-exits-immediately-even-with-console-readline-in-a-net-core-c
            Task.Run(() => Thread.Sleep(Timeout.Infinite)).Wait();

            mqttServer.StopAsync().Wait();
        }

        private static Task MqttServer_StartedAsync(EventArgs arg)
        {

            Console.WriteLine($"The connection details : {arg}");
            return Task.FromResult(arg);
        }

        private static Task MqttServer_InterceptingInboundPacketAsync(InterceptingPacketEventArgs arg)
        {
            string endpoint = arg.Endpoint;
            Console.WriteLine($"Endpoint :{arg.Endpoint}");

            return Task.FromResult(endpoint);
        }
    }
}
