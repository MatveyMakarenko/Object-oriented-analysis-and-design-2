using System;

namespace SmartHomeWithoutPattern.Devices
{
    public class EcoThermostat
    {
        private int temp;

        public void SetTemp(int temp)
        {
            temp = temp;
            Console.WriteLine($"[EcoHome] Температура: {temp}°C");
        }

        public int GetCurrentTemp()
        {
            return temp;
        }

        public string GetMode()
        {
            return "Стандарт";
        }
    }

}
