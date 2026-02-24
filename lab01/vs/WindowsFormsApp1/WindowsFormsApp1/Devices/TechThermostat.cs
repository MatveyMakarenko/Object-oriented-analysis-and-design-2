using System;

namespace SmartHomeWithoutPattern.Devices
{
    public class TechThermostat
    {
        private int _temp;
        private int _humidity = 45;

        public void SetTemp(int temp)
        {
            _temp = temp;
            Console.WriteLine($"[TechPro] Температура: {temp}°C");
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