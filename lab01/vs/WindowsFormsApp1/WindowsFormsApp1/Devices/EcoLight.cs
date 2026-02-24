using System;

namespace SmartHomeWithoutPattern.Devices
{
    public class EcoLight
    {
        private bool _isOn;

        public void TurnOn()
        {
            _isOn = true;
            Console.WriteLine("[EcoHome] Свет включен");
        }

        public void TurnOff()
        {
            _isOn = false;
            Console.WriteLine("[EcoHome] Свет выключен");
        }

        public string GetStatus()
        {
            return _isOn ? "ВКЛ" : "ВЫКЛ";
        }
    }
}