using System;

namespace SmartHomeWithoutPattern.Devices
{
    public class EcoLock
    {
        private bool _locked;

        public void Lock()
        {
            _locked = true;
            Console.WriteLine("[EcoHome] Замок заблокирован");
        }

        public void Unlock()
        {
            _locked = false;
            Console.WriteLine("[EcoHome] Замок разблокирован");
        }

        public bool IsLocked()
        {
            return _locked;
        }
    }
}