using System;

namespace SmartHomeWithoutPattern.Devices
{
    public class TechLight
    {
        private bool isOn;
        private int brightness;

        public void TurnOn()
        {
            isOn = true;
            Console.WriteLine("[TechPro] Свет включен");
        }

        public void TurnOff()
        {
            isOn = false;
            Console.WriteLine("[TechPro] Свет выключен");
        }

        public void SetBrightness(int level)
        {
            brightness = level;
            Console.WriteLine($"[TechPro] Яркость: {level}%");
        }

        public string GetStatus()
        {
            return isOn ? $"ВКЛ (Яркость: {_brightness}%)" : "ВЫКЛ";
        }
    }

}
