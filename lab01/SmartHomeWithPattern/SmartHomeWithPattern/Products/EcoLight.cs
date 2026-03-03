namespace SmartHomeWithPattern.Products
{
    public class EcoLight : Light
    {
        public bool IsOn { get; private set; }

        public void TurnOn()
        {
            IsOn = true;
            System.Console.WriteLine("[EcoHome] Свет включен");
        }

        public void TurnOff()
        {
            IsOn = false;
            System.Console.WriteLine("[EcoHome] Свет выключен");
        }

        public string GetStatus()
        {
            return IsOn ? "ВКЛ" : "ВЫКЛ";
        }
    }
}