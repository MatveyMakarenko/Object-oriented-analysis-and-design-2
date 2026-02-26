using System;

namespace SmartHomeWithoutPattern.Devices
{
    public class EcoLock
    {
        private bool locked;

        public void Lock()
        {
            locked = true;
            Console.WriteLine("[EcoHome] Замок заблокирован");
        }

        public void Unlock()
        {
            locked = false;
            Console.WriteLine("[EcoHome] Замок разблокирован");
        }

        public bool IsLocked()
        {
            return locked;
        }
    }

}
