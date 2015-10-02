namespace Core
{
    public interface IPinWrapper
    {
        void SetMode(PinMode mode);
        PinValue Read();
        void Write(PinValue value);
    }
}