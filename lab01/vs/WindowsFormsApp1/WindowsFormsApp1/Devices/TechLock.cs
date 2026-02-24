using System;

namespace SmartHomeWithoutPattern.Devices
{
    public class TechLock
    {
        private bool _locked;
        private string _encryptionKey;

        public void Lock()
        {
            _locked = true;
            _encryptionKey = "AES-256";
            Console.WriteLine("[TechPro] Замок заблокирован (Шифрование: AES-256)");
        }

        public void Unlock()
        {
            _locked = false;
            Console.WriteLine("[TechPro] Замок разблокирован");
        }

        public bool IsLocked()
        {
            return _locked;
        }

        public string GetLog()
        {
            return $"[LOG] Lock action at {DateTime.Now:HH:mm:ss}";
        }
    }
}