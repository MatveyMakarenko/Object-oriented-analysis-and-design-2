namespace SmartHomeWithPattern.Products
{
    public interface Thermostat
    {
        void SetTemp(int temp);
        int GetCurrentTemp();
        string GetMode();
    }
}