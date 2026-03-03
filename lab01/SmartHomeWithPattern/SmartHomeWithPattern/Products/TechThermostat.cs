namespace SmartHomeWithPattern.Products
{
    public class TechThermostat : Thermostat
    {
        private int _temp;
        private int _humidity = 45;

        public void SetTemp(int temp)
        {
            _temp = temp;
            System.Console.WriteLine($"[TechPro] Температура: {temp}°C");
        }

        public int GetCurrentTemp()
        {
            return _temp;
        }

        public string GetMode()
        {
            return "Премиум";
        }

        public int GetHumidity()
        {
            return _humidity;
        }
    }
}