namespace SmartHomeWithPattern.Products
{
    public class EcoThermostat : Thermostat
    {
        private int _temp;

        public void SetTemp(int temp)
        {
            _temp = temp;
            System.Console.WriteLine($"[EcoHome] Температура: {temp}°C");
        }

        public int GetCurrentTemp()
        {
            return _temp;
        }

        public string GetMode()
        {
            return "Стандарт";
        }
    }
}