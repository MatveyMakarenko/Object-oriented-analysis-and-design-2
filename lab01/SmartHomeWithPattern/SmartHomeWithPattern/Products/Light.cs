namespace SmartHomeWithPattern.Products
{
    public interface Light
    {
        void TurnOn();
        void TurnOff();
        string GetStatus();
        bool IsOn { get; }
    }
}