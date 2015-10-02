using Windows.Devices.Gpio;
using Core;

namespace TestHeadless
{
    class PinWrapper : IPinWrapper
    {
        private readonly GpioPin _pin;

        public PinWrapper(GpioPin pin)
        {
            _pin = pin;
        }

        public PinValue Read()
        {
            return ToPinValue(_pin.Read());
        }

        public void SetMode(PinMode mode)
        {
            _pin.SetDriveMode(mode == PinMode.In ? GpioPinDriveMode.Input : GpioPinDriveMode.Output);
        }

        public void Write(PinValue value)
        {
            _pin.Write(ToGpioiPinValue(value));
        }

        private GpioPinValue ToGpioiPinValue(PinValue value)
        {
            return value == PinValue.High ? GpioPinValue.High : GpioPinValue.Low;
        }

        private PinValue ToPinValue(GpioPinValue value)
        {
            return value == GpioPinValue.High ? PinValue.High : PinValue.Low;
        }
    }
}