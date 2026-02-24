using SmartHomeWithoutPattern.Devices;
using System;

namespace SmartHomeWithoutPattern
{
    /// <summary>
    /// Класс-менеджер
    /// </summary>
    public class SmartHomeManager
    {

        private EcoLight _ecoLight;
        private TechLight _techLight;
        private EcoThermostat _ecoThermostat;
        private TechThermostat _techThermostat;
        private EcoLock _ecoLock;
        private TechLock _techLock;

        private string _currentVendor;

        // чтобы скрывать ползунок яркости
        public string CurrentVendor => _currentVendor;

        public bool IsLightOn { get; private set; }
        public bool IsLockClosed { get; private set; }

        public SmartHomeManager()
        {
            _currentVendor = "EcoHome";
            IsLightOn = false;
            IsLockClosed = false;
            InitializeDevices();
        }

        /// <summary>
        /// Инициализация устройств 
        /// </summary>
        private void InitializeDevices()
        {
            if (_currentVendor == "EcoHome")
            {
                _ecoLight = new EcoLight();
                _ecoThermostat = new EcoThermostat();
                _ecoLock = new EcoLock();
            }
            else if (_currentVendor == "TechPro")
            {
                _techLight = new TechLight();
                _techThermostat = new TechThermostat();
                _techLock = new TechLock();
            }
        }

        /// <summary>
        /// Переключение вендора
        /// </summary>
        public void SwitchVendor(string vendor)
        {
            _currentVendor = vendor;
            IsLightOn = false;
            IsLockClosed = false;
            InitializeDevices();
        }

        /// <summary>
        /// Управление светом
        /// </summary>
        public void ControlLight(bool turnOn, int brightness = 0)
        {
            IsLightOn = turnOn;
            if (_currentVendor == "EcoHome")
            {
                if (turnOn)
                    _ecoLight.TurnOn();
                else
                    _ecoLight.TurnOff();
            }
            else if (_currentVendor == "TechPro")
            {
                if (turnOn)
                {
                    _techLight.TurnOn();
                    _techLight.SetBrightness(brightness);
                }
                else
                    _techLight.TurnOff();
            }
        }

        /// <summary>
        /// Управление термостатом
        /// </summary>
        public void ControlThermostat(int temp)
        {
            if (_currentVendor == "EcoHome")
            {
                _ecoThermostat.SetTemp(temp);
            }
            else if (_currentVendor == "TechPro")
            {
                _techThermostat.SetTemp(temp);
                Console.WriteLine($"[TechPro] Влажность: {_techThermostat.GetHumidity()}%");
            }
        }

        /// <summary>
        /// Управление замком
        /// </summary>
        public void ControlLock(bool lockDoor)
        {
            IsLockClosed = lockDoor;
            if (_currentVendor == "EcoHome")
            {
                if (lockDoor)
                    _ecoLock.Lock();
                else
                    _ecoLock.Unlock();
            }
            else if (_currentVendor == "TechPro")
            {
                if (lockDoor)
                    _techLock.Lock();
                else
                    _techLock.Unlock();

                Console.WriteLine(_techLock.GetLog());
            }
        }

        /// <summary>
        /// Получение статуса для отображения в GUI
        /// </summary>
        public string GetDashboardStatus()
        {
            string status = $"SmartHome Hub ({_currentVendor})\n\n";

            if (_currentVendor == "EcoHome")
            {
                status += $"💡 Свет: {_ecoLight.GetStatus()}\n";
                status += $"🌡️ Термостат: {_ecoThermostat.GetCurrentTemp()}°C ({_ecoThermostat.GetMode()})\n";
                status += $"🔒 Замок: {(_ecoLock.IsLocked() ? "Закрыт" : "Открыт")}\n";
            }
            else if (_currentVendor == "TechPro")
            {
                status += $"💡 Свет: {_techLight.GetStatus()}\n";
                status += $"🌡️ Термостат: {_techThermostat.GetCurrentTemp()}°C ({_techThermostat.GetMode()})\n";
                status += $"🔒 Замок: {(_techLock.IsLocked() ? "Закрыт" : "Открыт")}\n";
                status += $"📊 {_techLock.GetLog()}\n";
            }

            return status;
        }

        /// <summary>
        /// Экспорт конфигурации
        /// </summary>
        public string ExportConfig()
        {
            string config = "{\n";
            config += $"  \"vendor\": \"{_currentVendor}\",\n";
            config += "  \"devices\": [\n";

            if (_currentVendor == "EcoHome")
            {
                config += "    { \"type\": \"EcoLight\", \"status\": \"" + _ecoLight.GetStatus() + "\" },\n";
                config += "    { \"type\": \"EcoThermostat\", \"temp\": " + _ecoThermostat.GetCurrentTemp() + " },\n";
                config += "    { \"type\": \"EcoLock\", \"locked\": " + _ecoLock.IsLocked().ToString().ToLower() + " }\n";
            }
            else if (_currentVendor == "TechPro")
            {
                config += "    { \"type\": \"TechLight\", \"status\": \"" + _techLight.GetStatus() + "\" },\n";
                config += "    { \"type\": \"TechThermostat\", \"temp\": " + _techThermostat.GetCurrentTemp() + " },\n";
                config += "    { \"type\": \"TechLock\", \"locked\": " + _techLock.IsLocked().ToString().ToLower() + " }\n";
            }

            config += "  ]\n}";
            return config;
        }
    }
}