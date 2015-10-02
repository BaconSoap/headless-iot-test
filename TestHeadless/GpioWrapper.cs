using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Core;

namespace TestHeadless
{
    class GpioWrapper: IGpioWrapper
    {
        public IPinWrapper GetPin(int pin)
        {
            return new PinWrapper(GpioController.GetDefault().OpenPin(pin));
        }
    }
}
