using System;

namespace SmartHomeWithPattern.Products
{
    public class TechLock : Lock
    {
        private bool _locked;
        private string _encryptionKey;

        public void Lock()
        {
            _locked = true;
            _encryptionKey = "AES-256";
            System.Console.WriteLine("[TechPro] Замок заблокирован (Шифрование: AES-256)");
        }

        public void Unlock()
        {
            _locked = false;
            System.Console.WriteLine("[TechPro] Замок разблокирован");
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