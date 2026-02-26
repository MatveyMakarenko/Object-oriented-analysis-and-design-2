using System;

namespace SmartHomeWithoutPattern.Devices
{
    public class TechThermostat
    {
        private int temp;
        private int humidity = 45;

        public void SetTemp(int temp)
        {
            temp = temp;
            Console.WriteLine($"[TechPro] Температура: {temp}°C");
        }

        public int GetCurrentTemp()
        {
            return temp;
        }

        public string GetMode()
        {
            return "Премиум";
        }

        public int GetHumidity()
        {
            return humidity;
        }
    }

}
