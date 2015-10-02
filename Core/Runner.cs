using System;
using System.Threading;

namespace Core
{
    public class Runner
    {
        private readonly IGpioWrapper _gpio;
        private PinValue value = PinValue.High;
        private const int LED_PIN = 25;
        private IPinWrapper pin;
        private Timer timer;

        public Runner(IGpioWrapper gpio)
        {
            _gpio = gpio;
        }

        public void Run()
        {
            InitGPIO();
            timer = new Timer(Timer_Tick, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

        }

        private void InitGPIO()
        {
            pin = _gpio.GetPin(LED_PIN);
            pin.Write(PinValue.High);
            pin.SetMode(PinMode.Out);
        }

        private void Timer_Tick(object state)
        {
            value = (value == PinValue.High) ? PinValue.Low : PinValue.High;
            pin.Write(value);
        }
    }
}
