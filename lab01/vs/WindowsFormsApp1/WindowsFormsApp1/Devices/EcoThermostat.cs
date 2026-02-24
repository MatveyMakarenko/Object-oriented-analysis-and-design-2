using System;

namespace SmartHomeWithoutPattern.Devices
{
    public class EcoThermostat
    {
        private int _temp;

        public void SetTemp(int temp)
        {
            _temp = temp;
            Console.WriteLine($"[EcoHome] Температура: {temp}°C");
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