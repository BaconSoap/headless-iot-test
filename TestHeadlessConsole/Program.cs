using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core;

namespace TestHeadlessConsole
{
    class Program
    {
        static ManualResetEvent _quitEvent = new ManualResetEvent(false);

        static void Main()
        {
            Console.CancelKeyPress += (sender, eArgs) =>
            {
                _quitEvent.Set();
                eArgs.Cancel = true;
            };

            var builder = new ContainerBuilder();
            builder.RegisterModule<CoreModule>();
            builder.RegisterModule<ConsoleModule>();
            var scope = builder.Build().BeginLifetimeScope();

            var runner = scope.Resolve<Runner>();
            runner.Run();

            _quitEvent.WaitOne();
        }

    }

    class ConsoleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleGpioWrapper>().AsSelf().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }

    class ConsoleGpioWrapper : IGpioWrapper
    {
        private Dictionary<int, IPinWrapper> _pins;

        public ConsoleGpioWrapper()
        {
            _pins = new Dictionary<int, IPinWrapper>();
        }

        public IPinWrapper GetPin(int pin)
        {
            if (!_pins.ContainsKey(pin))
            {
                _pins[pin] = new ConsolePinWrapper();
            }

            return _pins[pin];
        }
    }

    class ConsolePinWrapper : IPinWrapper
    {
        private PinMode _mode;
        private PinValue _value;

        public void SetMode(PinMode mode)
        {
            _mode = mode;
        }

        public PinValue Read()
        {
            return _value;
        }

        public void Write(PinValue value)
        {
            Console.WriteLine(value);
            _value = value;
        }
    }
}
