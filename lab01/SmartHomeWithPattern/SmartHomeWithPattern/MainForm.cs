using System;
using System.Drawing;
using System.Windows.Forms;
using SmartHomeWithPattern.Factories;
using SmartHomeWithPattern.Products;

namespace SmartHomeWithPattern
{
    public partial class MainForm : Form
    {
        private DeviceFactory _factory;
        private Light _light;
        private Thermostat _thermostat;
        private Lock _lock;

        private Image _lightOnImage;
        private Image _lightOffImage;
        private bool _isLightOn;
        private bool _isLockClosed;
        private int _currentBrightness;
        private string _lastLockLog = "";

        public MainForm()
        {
            InitializeComponent();
            _lightOnImage = Image.FromFile("images/light_on.png");
            _lightOffImage = Image.FromFile("images/light_off.png");
            SetFactory(new EcoHomeFactory());
            UpdateDashboard();
        }

        private void SetFactory(DeviceFactory factory)
        {
            _factory = factory;
            _light = factory.CreateLight();
            _thermostat = factory.CreateThermostat();
            _lock = factory.CreateLock();
            _isLightOn = false;
            _isLockClosed = false;
            _currentBrightness = 50;
            _lastLockLog = "";
            trkBrightness.Value = _currentBrightness;

    
            Console.WriteLine($"[DEBUG] Создан термостат: {_thermostat.GetType().Name}");
        }

        private void UpdateDashboard()
        {
            string vendorName = _factory is EcoHomeFactory ? "EcoHome" : "TechPro";
            string thermostatMode = _thermostat.GetMode();

            string lightStatus = _light.GetStatus();
            if (_isLightOn && _factory is TechProFactory)
            {
                lightStatus = $"ВКЛ (Яркость: {_currentBrightness}%)";
            }

            string status = $"SmartHome Hub ({vendorName})\n\n";
            status += $"💡 Свет: {lightStatus}\n";
            status += $"🌡️ Термостат: {_thermostat.GetCurrentTemp()}°C ({thermostatMode})\n";
            status += $"🔒 Замок: {(_lock.IsLocked() ? "Закрыт" : "Открыт")}\n";

            if (_factory is TechProFactory && !string.IsNullOrEmpty(_lastLockLog))
            {
                status += $"📊 {_lastLockLog}\n";
            }

            lblStatus.Text = status;

            btnLightToggle.Text = _isLightOn ? "Свет ВЫКЛ" : "Свет ВКЛ";
            btnLockToggle.Text = "Замок";
            pictureBoxLight.Image = _isLightOn ? _lightOnImage : _lightOffImage;

            bool isTechPro = _factory is TechProFactory;
            trkBrightness.Visible = isTechPro;
            trkBrightness.Enabled = isTechPro;

            lblLockLabel.Visible = !isTechPro;
        }

        private void btnEcoHome_Click(object sender, EventArgs e)
        {
            SetFactory(new EcoHomeFactory());
            UpdateDashboard();
        }

        private void btnTechPro_Click(object sender, EventArgs e)
        {
            SetFactory(new TechProFactory());
            UpdateDashboard();
        }

        private void btnLightToggle_Click(object sender, EventArgs e)
        {
            _isLightOn = !_isLightOn;
            if (_isLightOn)
                _light.TurnOn();
            else
                _light.TurnOff();

            if (_light is TechLight techLight)
            {
                techLight.SetBrightness(trkBrightness.Value);
                _currentBrightness = trkBrightness.Value;
            }

            UpdateDashboard();
        }

        private void trkBrightness_Scroll(object sender, EventArgs e)
        {
            _currentBrightness = trkBrightness.Value;
            if (_isLightOn && _light is TechLight techLight)
            {
                techLight.SetBrightness(_currentBrightness);
            }
            UpdateDashboard();
        }

        private void btnThermostatSet_Click(object sender, EventArgs e)
        {
            _thermostat.SetTemp((int)numTemp.Value);
            UpdateDashboard();
        }

        private void btnLockToggle_Click(object sender, EventArgs e)
        {
            _isLockClosed = !_isLockClosed;
            if (_isLockClosed)
                _lock.Lock();
            else
                _lock.Unlock();

            _lastLockLog = _lock.GetLog();
            Console.WriteLine($"[DEBUG] Лог замка: {_lastLockLog}");

            UpdateDashboard();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string vendorName = _factory is EcoHomeFactory ? "EcoHome" : "TechPro";
            string config = "{\n";
            config += $"  \"vendor\": \"{vendorName}\",\n";
            config += "  \"devices\": [\n";
            config += $"    {{ \"type\": \"Light\", \"status\": \"{_light.GetStatus()}\" }},\n";
            config += $"    {{ \"type\": \"Thermostat\", \"temp\": {_thermostat.GetCurrentTemp()}, \"mode\": \"{_thermostat.GetMode()}\" }},\n";
            config += $"    {{ \"type\": \"Lock\", \"locked\": {_lock.IsLocked().ToString().ToLower()} }}\n";
            config += "  ]\n}";
            MessageBox.Show(config, "Экспорт конфигурации (JSON)");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _lightOnImage?.Dispose();
            _lightOffImage?.Dispose();
            base.OnFormClosing(e);
        }
    }
}