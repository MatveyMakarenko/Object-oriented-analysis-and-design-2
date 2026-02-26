using System;
using System.Windows.Forms;

namespace SmartHomeWithoutPattern
{
    public partial class MainForm : Form
    {
        private SmartHomeManager _manager;

        public MainForm()
        {
            InitializeComponent();
            _manager = new SmartHomeManager();
            UpdateDashboard();
        }

        private void UpdateDashboard()
        {
            lblStatus.Text = _manager.GetDashboardStatus();

            // Обновляем текст кнопок
            btnLightToggle.Text = _manager.IsLightOn ? "Свет ВЫКЛ" : "Свет ВКЛ";
            btnLockToggle.Text = _manager.IsLockClosed ? "Замок ОТКРЫТЬ" : "Замок ЗАКРЫТЬ";

            
            bool isTechPro = _manager.CurrentVendor == "TechPro";
            trkBrightness.Visible = isTechPro;
            trkBrightness.Enabled = isTechPro;
        }

        private void btnEcoHome_Click(object sender, EventArgs e)
        {
            _manager.SwitchVendor("EcoHome");
            UpdateDashboard();
        }

        private void btnTechPro_Click(object sender, EventArgs e)
        {
            _manager.SwitchVendor("TechPro");
            UpdateDashboard();
        }

        private void btnLightToggle_Click(object sender, EventArgs e)
        {
            // Переключаем состояние на противоположное
            bool newState = !_manager.IsLightOn;
            _manager.ControlLight(newState, trkBrightness.Value);
            UpdateDashboard();
        }

        private void btnThermostatSet_Click(object sender, EventArgs e)
        {
            _manager.ControlThermostat((int)numTemp.Value);
            UpdateDashboard();
        }

        private void btnLockToggle_Click(object sender, EventArgs e)
        {
            // Переключаем состояние на противоположное
            bool newState = !_manager.IsLockClosed;
            _manager.ControlLock(newState);
            UpdateDashboard();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string config = _manager.ExportConfig();
            MessageBox.Show(config, "Экспорт конфигурации (JSON)");
        }
    }

}
