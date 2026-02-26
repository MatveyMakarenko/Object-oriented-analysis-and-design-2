using System;

namespace SmartHomeWithoutPattern.Devices
{
    public class TechLock
    {
        private bool locked;
        private string encryptionKey;

        public void Lock()
        {
            locked = true;
            encryptionKey = "AES-256";
            Console.WriteLine("[TechPro] Замок заблокирован (Шифрование: AES-256)");
        }

        public void Unlock()
        {
            locked = false;
            Console.WriteLine("[TechPro] Замок разблокирован");
        }

        public bool IsLocked()
        {
            return locked;
        }

        public string GetLog()
        {
            return $"[LOG] Lock action at {DateTime.Now:HH:mm:ss}";
        }
    }

}
