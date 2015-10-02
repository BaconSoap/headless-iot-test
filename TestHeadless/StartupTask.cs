using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Windows.Devices.Gpio;
using Windows.System.Threading;
using Autofac;
using Core;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace TestHeadless
{
    public sealed class StartupTask : IBackgroundTask
    {
        BackgroundTaskDeferral deferral;
        private GpioPinValue value = GpioPinValue.High;
        private const int LED_PIN = 25;
        private GpioPin pin;
        private ThreadPoolTimer timer;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();
            var builder = new ContainerBuilder();
            builder.RegisterModule<TestHeadlessModule>();
            builder.RegisterModule<CoreModule>();
            var scope = builder.Build().BeginLifetimeScope();
            var runner = scope.Resolve<Runner>();
            runner.Run();
        }
    }

    class TestHeadlessModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GpioWrapper>().AsSelf().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
