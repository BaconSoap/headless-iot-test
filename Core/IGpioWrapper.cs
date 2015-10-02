namespace Core
{
    public interface IGpioWrapper
    {
        IPinWrapper GetPin(int pin);
    }
}
