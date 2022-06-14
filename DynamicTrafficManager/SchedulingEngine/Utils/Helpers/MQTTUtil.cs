using MQTTnet;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchedulingEngine.Utils.Helpers
{
    public static class MQTTUtil
    {
        private static IMqttClient _client;
        private static MqttClientOptions _options;
        public static async Task ConnectAsync(string topic, string payload)
        {
            Console.WriteLine("Starting Publisher....");
            try
            {
                // Create a new MQTT client.
                var factory = new MqttFactory();
                _client = factory.CreateMqttClient();

                //configure options
                _options = new MqttClientOptionsBuilder()
                    .WithClientId("PublisherId")
                    .WithTcpServer("localhost", 1884)
                    .WithCredentials("bud", "%spencer%")
                    .WithCleanSession()
                    .Build();


                _client.ApplicationMessageReceivedAsync += _client_ApplicationMessageReceivedAsync;


                //connect
                await _client.ConnectAsync(_options);

                //publish
                Publish(topic, payload);

                await _client.DisconnectAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static Task _client_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            Task task = null;
            try
            {
                string topic = arg.ApplicationMessage.Topic;
                if (string.IsNullOrWhiteSpace(topic) == false)
                {
                    string payload = Encoding.UTF8.GetString(arg.ApplicationMessage.Payload);
                    Console.WriteLine($"Topic: {topic}. Message Received: {payload}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }
            return task;
        }

        static void Publish(string topic, string payload)
        {
            var greenLightDurationUpate = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload($"{payload}")
                .WithRetainFlag()
                .Build();


            if (_client.IsConnected)
            {
                Console.WriteLine($"publishing at {DateTime.UtcNow}");
                Console.WriteLine($"publishing topic {topic}");
                Console.WriteLine($"publishing payload {payload}");
                _client.PublishAsync(greenLightDurationUpate);
            }
        }
    }
}
