using System;

namespace SmartHomeWithoutPattern.Devices
{
    public class TechLight
    {
        private bool _isOn;
        private int _brightness;

        public void TurnOn()
        {
            _isOn = true;
            Console.WriteLine("[TechPro] Свет включен");
        }

        public void TurnOff()
        {
            _isOn = false;
            Console.WriteLine("[TechPro] Свет выключен");
        }

        public void SetBrightness(int level)
        {
            _brightness = level;
            Console.WriteLine($"[TechPro] Яркость: {level}%");
        }

        public string GetStatus()
        {
            return _isOn ? $"ВКЛ (Яркость: {_brightness}%)" : "ВЫКЛ";
        }
    }
}