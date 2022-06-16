using MQTTnet;
using MQTTnet.Client;
using System;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrafficLightUnitClient.Utils;

namespace TrafficLightUnitClient
{
    class Program
    {
        private static IMqttClient _client;
        private static MqttClientOptions _options;

        private static double NSDuration;
        private static double EWDuration;

        private static bool running = true;
        private static GpioController controller;
        static void Main(string[] args)
        {

            try
            {
                Console.WriteLine("Starting Subsriber....");

                //create subscriber client
                var factory = new MqttFactory();
                _client = factory.CreateMqttClient();

                //configure options
                _options = new MqttClientOptionsBuilder()
                    .WithClientId("SubscriberId")
                    //.WithTcpServer("192.168.0.101", 1884)
                    .WithTcpServer("192.168.0.101", 1884)
                    .WithCredentials("bud", "%spencer%")
                    .WithCleanSession()
                    .Build();

                //Handlers
                _client.ConnectedAsync += _client_ConnectedAsync;
                _client.DisconnectedAsync += _client_DisconnectedAsync;
                _client.ApplicationMessageReceivedAsync += _client_ApplicationMessageReceivedAsync;

                //actually connect
                _client.ConnectAsync(_options).Wait(new TimeSpan(0, 10, 15));

                Console.WriteLine("Press key to exit");
                Console.ReadLine();

                //To keep the app running in container
                //https://stackoverflow.com/questions/38549006/docker-container-exits-immediately-even-with-console-readline-in-a-net-core-c
                Task.Run(() => Thread.Sleep(Timeout.Infinite)).Wait();
                _client.DisconnectAsync().Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }



        private static Task _client_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
            Console.WriteLine($"+ Topic = {arg.ApplicationMessage.Topic}");
            Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(arg.ApplicationMessage.Payload)}");
            Console.WriteLine($"+ QoS = {arg.ApplicationMessage.QualityOfServiceLevel}");
            Console.WriteLine($"+ Retain = {arg.ApplicationMessage.Retain}");
            Console.WriteLine();

            string payload = Encoding.UTF8.GetString(arg.ApplicationMessage.Payload);

            NSDuration = double.Parse(payload.Split('|')[0].Split('=')[1]);
            EWDuration = double.Parse(payload.Split('|')[1].Split('=')[1]);

            return Task.FromResult(arg.ApplicationMessage.Topic);
        }

        private static Task _client_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            Console.WriteLine("Disconnected from MQTT Brokers.");
            return Task.FromResult(arg.ReasonString);
        }

        private async static Task _client_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            Console.WriteLine("Connected successfully with MQTT Brokers.");


            //Subscribe to topic
            await _client.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("test").Build());

            NSDuration = Constants.DEFAULT_GREENLIGHT_DURATION;
            EWDuration = Constants.DEFAULT_GREENLIGHT_DURATION;
            ControlLightUnit();
        }
        private static void ControlLightUnit()
        {
            Console.WriteLine("Blinking LED. Press Ctrl+C to end.");
            
       

            using (controller = new GpioController(PinNumberingScheme.Logical, new SysFsDriver()))
            {
                Console.WriteLine($"The pin numbering scheme {controller.NumberingScheme}");
                Console.WriteLine($"The pin 12 supported scheme {controller.IsPinModeSupported(21, PinMode.Output)}");

                // controller.OpenPin(12, PinMode.Output, PinValue.Low);
                // controller.OpenPin(20, PinMode.Output, PinValue.Low);
                // controller.OpenPin(21, PinMode.Output, PinValue.Low);
                //
                // controller.OpenPin(18, PinMode.Output, PinValue.Low);
                // controller.OpenPin(23, PinMode.Output, PinValue.Low);
                // controller.OpenPin(24, PinMode.Output, PinValue.Low);

                try
                {
                    controller.OpenPin(12, PinMode.Output);
                    controller.OpenPin(20, PinMode.Output);
                    controller.OpenPin(21, PinMode.Output);

                    controller.OpenPin(18, PinMode.Output);
                    controller.OpenPin(23, PinMode.Output);
                    controller.OpenPin(24, PinMode.Output);

                    while (running)
                    {
                        controller.Write(21, PinValue.High);//GREEN ON
                        controller.Write(20, PinValue.Low);//YELLOW OFF
                        controller.Write(12, PinValue.Low);//RED OFF


                        controller.Write(18, PinValue.High);//RED ON
                        controller.Write(23, PinValue.Low);//YELLOW OFF
                        controller.Write(24, PinValue.Low);//GREEN OFF
                        Console.WriteLine($"seeping for direction NS with value {NSDuration}");
                        Thread.Sleep((int)NSDuration);


                        controller.Write(21, PinValue.Low);//GREEN OFF
                        controller.Write(20, PinValue.High);//YELLOW ON
                        controller.Write(12, PinValue.Low);//RED OFF

                        Thread.Sleep(1000);

                        controller.Write(21, PinValue.Low);//GREEN OFF
                        controller.Write(20, PinValue.Low);//YELLOW OFF
                        controller.Write(12, PinValue.High);//RED ON

                        controller.Write(18, PinValue.Low);//RED OFF
                        controller.Write(23, PinValue.Low);//YELLOW OFF
                        controller.Write(24, PinValue.High);//GREEN ON

                        Console.WriteLine($"seeping for direction EW with value {EWDuration}");
                        Thread.Sleep((int)EWDuration);

                        controller.Write(23, PinValue.High);//YELLOW ON
                        controller.Write(24, PinValue.Low);//GREEN OFF


                        Thread.Sleep(1000);

                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
