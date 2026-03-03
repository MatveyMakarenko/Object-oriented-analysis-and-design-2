namespace SmartHomeWithPattern.Products
{
    public class TechLight : Light
    {
        public bool IsOn { get; private set; }
        private int _brightness;

        public void TurnOn()
        {
            IsOn = true;
            System.Console.WriteLine("[TechPro] Свет включен");
        }

        public void TurnOff()
        {
            IsOn = false;
            System.Console.WriteLine("[TechPro] Свет выключен");
        }

        public void SetBrightness(int level)
        {
            _brightness = level;
            System.Console.WriteLine($"[TechPro] Яркость: {level}%");
        }

        public int GetBrightness()
        {
            return _brightness;
        }

        public string GetStatus()
        {
            return IsOn ? $"ВКЛ (Яркость: {_brightness}%)" : "ВЫКЛ";
        }
    }
}