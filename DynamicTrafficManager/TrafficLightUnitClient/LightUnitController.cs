using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Text;
using System.Threading;

namespace TrafficLightUnitClient
{
    public  class LightUnitController
    {
        public static GpioController controller = new GpioController();
        public static GpioController CreateController()
        {
            return controller;
        }

    }
}
