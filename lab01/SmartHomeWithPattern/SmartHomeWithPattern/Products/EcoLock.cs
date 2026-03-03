namespace SmartHomeWithPattern.Products
{
    public class EcoLock : Lock
    {
        private bool _locked;

        public void Lock()
        {
            _locked = true;
            System.Console.WriteLine("[EcoHome] Замок заблокирован");
        }

        public void Unlock()
        {
            _locked = false;
            System.Console.WriteLine("[EcoHome] Замок разблокирован");
        }

        public bool IsLocked()
        {
            return _locked;
        }

        public string GetLog()
        {
            return "";
        }
    }
}