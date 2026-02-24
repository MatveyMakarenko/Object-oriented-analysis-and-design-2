namespace SmartHomeWithoutPattern
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnEcoHome = new System.Windows.Forms.Button();
            this.btnTechPro = new System.Windows.Forms.Button();
            this.btnLightToggle = new System.Windows.Forms.Button();
            this.trkBrightness = new System.Windows.Forms.TrackBar();
            this.btnThermostatSet = new System.Windows.Forms.Button();
            this.numTemp = new System.Windows.Forms.NumericUpDown();
            this.btnLockToggle = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblVendor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trkBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTemp)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.Location = new System.Drawing.Point(12, 150);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(360, 150);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Статус загрузки...";
            // 
            // btnEcoHome
            // 
            this.btnEcoHome.Location = new System.Drawing.Point(12, 12);
            this.btnEcoHome.Name = "btnEcoHome";
            this.btnEcoHome.Size = new System.Drawing.Size(100, 30);
            this.btnEcoHome.TabIndex = 1;
            this.btnEcoHome.Text = "EcoHome";
            this.btnEcoHome.UseVisualStyleBackColor = true;
            this.btnEcoHome.Click += new System.EventHandler(this.btnEcoHome_Click);
            // 
            // btnTechPro
            // 
            this.btnTechPro.Location = new System.Drawing.Point(118, 12);
            this.btnTechPro.Name = "btnTechPro";
            this.btnTechPro.Size = new System.Drawing.Size(100, 30);
            this.btnTechPro.TabIndex = 2;
            this.btnTechPro.Text = "TechPro";
            this.btnTechPro.UseVisualStyleBackColor = true;
            this.btnTechPro.Click += new System.EventHandler(this.btnTechPro_Click);
            // 
            // btnLightToggle
            // 
            this.btnLightToggle.Location = new System.Drawing.Point(12, 80);
            this.btnLightToggle.Name = "btnLightToggle";
            this.btnLightToggle.Size = new System.Drawing.Size(105, 30);
            this.btnLightToggle.TabIndex = 4;
            this.btnLightToggle.Text = "Свет ВКЛ/ВЫКЛ";
            this.btnLightToggle.UseVisualStyleBackColor = true;
            this.btnLightToggle.Click += new System.EventHandler(this.btnLightToggle_Click);
            // 
            // trkBrightness
            // 
            this.trkBrightness.Location = new System.Drawing.Point(118, 85);
            this.trkBrightness.Maximum = 100;
            this.trkBrightness.Name = "trkBrightness";
            this.trkBrightness.Size = new System.Drawing.Size(104, 45);
            this.trkBrightness.TabIndex = 5;
            this.trkBrightness.Value = 50;
            // 
            // btnThermostatSet
            // 
            this.btnThermostatSet.Location = new System.Drawing.Point(228, 80);
            this.btnThermostatSet.Name = "btnThermostatSet";
            this.btnThermostatSet.Size = new System.Drawing.Size(100, 30);
            this.btnThermostatSet.TabIndex = 6;
            this.btnThermostatSet.Text = "Термостат";
            this.btnThermostatSet.UseVisualStyleBackColor = true;
            this.btnThermostatSet.Click += new System.EventHandler(this.btnThermostatSet_Click);
            // 
            // numTemp
            // 
            this.numTemp.Location = new System.Drawing.Point(334, 82);
            this.numTemp.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numTemp.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numTemp.Name = "numTemp";
            this.numTemp.Size = new System.Drawing.Size(50, 20);
            this.numTemp.TabIndex = 7;
            this.numTemp.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // btnLockToggle
            // 
            this.btnLockToggle.Location = new System.Drawing.Point(12, 120);
            this.btnLockToggle.Name = "btnLockToggle";
            this.btnLockToggle.Size = new System.Drawing.Size(100, 30);
            this.btnLockToggle.TabIndex = 8;
            this.btnLockToggle.Text = "Замок";
            this.btnLockToggle.UseVisualStyleBackColor = true;
            this.btnLockToggle.Click += new System.EventHandler(this.btnLockToggle_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(280, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 30);
            this.btnExport.TabIndex = 9;
            this.btnExport.Text = "Экспорт";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblVendor
            // 
            this.lblVendor.AutoSize = true;
            this.lblVendor.Location = new System.Drawing.Point(12, 55);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Size = new System.Drawing.Size(105, 13);
            this.lblVendor.TabIndex = 3;
            this.lblVendor.Text = "Выберите вендора:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 311);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnLockToggle);
            this.Controls.Add(this.numTemp);
            this.Controls.Add(this.btnThermostatSet);
            this.Controls.Add(this.trkBrightness);
            this.Controls.Add(this.btnLightToggle);
            this.Controls.Add(this.lblVendor);
            this.Controls.Add(this.btnTechPro);
            this.Controls.Add(this.btnEcoHome);
            this.Controls.Add(this.lblStatus);
            this.Name = "MainForm";
            this.Text = "SmartHome Hub (Без паттерна)";
            ((System.ComponentModel.ISupportInitialize)(this.trkBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTemp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnEcoHome;
        private System.Windows.Forms.Button btnTechPro;
        private System.Windows.Forms.Button btnLightToggle;
        private System.Windows.Forms.TrackBar trkBrightness;
        private System.Windows.Forms.Button btnThermostatSet;
        private System.Windows.Forms.NumericUpDown numTemp;
        private System.Windows.Forms.Button btnLockToggle;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblVendor;
    }
}