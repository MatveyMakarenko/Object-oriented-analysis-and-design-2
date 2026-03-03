namespace SmartHomeWithPattern
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
            this.pictureBoxLight = new System.Windows.Forms.PictureBox();
            this.groupBoxVendor = new System.Windows.Forms.GroupBox();
            this.groupBoxDevices = new System.Windows.Forms.GroupBox();
            this.lblLightLabel = new System.Windows.Forms.Label();
            this.lblTempLabel = new System.Windows.Forms.Label();
            this.lblLockLabel = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblBrightness = new System.Windows.Forms.Label();
            this.groupBoxVendor.SuspendLayout();
            this.groupBoxDevices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLight)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(180, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🏠 SmartHome Hub";
            // 
            // groupBoxVendor
            // 
            this.groupBoxVendor.Controls.Add(this.btnEcoHome);
            this.groupBoxVendor.Controls.Add(this.btnTechPro);
            this.groupBoxVendor.Controls.Add(this.btnExport);
            this.groupBoxVendor.Location = new System.Drawing.Point(12, 40);
            this.groupBoxVendor.Name = "groupBoxVendor";
            this.groupBoxVendor.Size = new System.Drawing.Size(360, 50);
            this.groupBoxVendor.TabIndex = 1;
            this.groupBoxVendor.TabStop = false;
            this.groupBoxVendor.Text = "Выбор вендора";
            // 
            // btnEcoHome
            // 
            this.btnEcoHome.Location = new System.Drawing.Point(6, 20);
            this.btnEcoHome.Name = "btnEcoHome";
            this.btnEcoHome.Size = new System.Drawing.Size(90, 25);
            this.btnEcoHome.TabIndex = 1;
            this.btnEcoHome.Text = "🌿 EcoHome";
            this.btnEcoHome.UseVisualStyleBackColor = true;
            this.btnEcoHome.Click += new System.EventHandler(this.btnEcoHome_Click);
            // 
            // btnTechPro
            // 
            this.btnTechPro.Location = new System.Drawing.Point(102, 20);
            this.btnTechPro.Name = "btnTechPro";
            this.btnTechPro.Size = new System.Drawing.Size(90, 25);
            this.btnTechPro.TabIndex = 2;
            this.btnTechPro.Text = "⚡ TechPro";
            this.btnTechPro.UseVisualStyleBackColor = true;
            this.btnTechPro.Click += new System.EventHandler(this.btnTechPro_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(264, 20);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(90, 25);
            this.btnExport.TabIndex = 9;
            this.btnExport.Text = "📄 Экспорт";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // groupBoxDevices
            // 
            this.groupBoxDevices.Controls.Add(this.lblLightLabel);
            this.groupBoxDevices.Controls.Add(this.btnLightToggle);
            this.groupBoxDevices.Controls.Add(this.pictureBoxLight);
            this.groupBoxDevices.Controls.Add(this.lblBrightness);
            this.groupBoxDevices.Controls.Add(this.trkBrightness);
            this.groupBoxDevices.Controls.Add(this.lblTempLabel);
            this.groupBoxDevices.Controls.Add(this.btnThermostatSet);
            this.groupBoxDevices.Controls.Add(this.numTemp);
            this.groupBoxDevices.Controls.Add(this.lblLockLabel);
            this.groupBoxDevices.Controls.Add(this.btnLockToggle);
            this.groupBoxDevices.Location = new System.Drawing.Point(12, 96);
            this.groupBoxDevices.Name = "groupBoxDevices";
            this.groupBoxDevices.Size = new System.Drawing.Size(360, 140);
            this.groupBoxDevices.TabIndex = 2;
            this.groupBoxDevices.TabStop = false;
            this.groupBoxDevices.Text = "Управление устройствами";
            // 
            // lblLightLabel
            // 
            this.lblLightLabel.AutoSize = true;
            this.lblLightLabel.Location = new System.Drawing.Point(6, 25);
            this.lblLightLabel.Name = "lblLightLabel";
            this.lblLightLabel.Size = new System.Drawing.Size(35, 13);
            this.lblLightLabel.TabIndex = 0;
            this.lblLightLabel.Text = "💡 Свет:";
            // 
            // btnLightToggle
            // 
            this.btnLightToggle.Location = new System.Drawing.Point(9, 45);
            this.btnLightToggle.Name = "btnLightToggle";
            this.btnLightToggle.Size = new System.Drawing.Size(100, 30);
            this.btnLightToggle.TabIndex = 4;
            this.btnLightToggle.Text = "Свет ВКЛ";
            this.btnLightToggle.UseVisualStyleBackColor = true;
            this.btnLightToggle.Click += new System.EventHandler(this.btnLightToggle_Click);
            // 
            // pictureBoxLight
            // 
            this.pictureBoxLight.Location = new System.Drawing.Point(120, 40);
            this.pictureBoxLight.Name = "pictureBoxLight";
            this.pictureBoxLight.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxLight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLight.TabIndex = 10;
            this.pictureBoxLight.TabStop = false;
            // 
            // lblBrightness
            // 
            this.lblBrightness.AutoSize = true;
            this.lblBrightness.Location = new System.Drawing.Point(180, 25);
            this.lblBrightness.Name = "lblBrightness";
            this.lblBrightness.Size = new System.Drawing.Size(45, 13);
            this.lblBrightness.TabIndex = 11;
            
            // 
            // trkBrightness
            // 
            this.trkBrightness.Location = new System.Drawing.Point(180, 45);
            this.trkBrightness.Maximum = 100;
            this.trkBrightness.Name = "trkBrightness";
            this.trkBrightness.Size = new System.Drawing.Size(160, 45);
            this.trkBrightness.TabIndex = 5;
            this.trkBrightness.Value = 50;
            this.trkBrightness.Scroll += new System.EventHandler(this.trkBrightness_Scroll);
            // 
            // lblTempLabel
            // 
            this.lblTempLabel.AutoSize = true;
            this.lblTempLabel.Location = new System.Drawing.Point(6, 75);
            this.lblTempLabel.Name = "lblTempLabel";
            this.lblTempLabel.Size = new System.Drawing.Size(65, 13);
            this.lblTempLabel.TabIndex = 0;
            this.lblTempLabel.Text = "🌡️ Термостат:";
            // 
            // btnThermostatSet
            // 
            this.btnThermostatSet.Location = new System.Drawing.Point(9, 95);
            this.btnThermostatSet.Name = "btnThermostatSet";
            this.btnThermostatSet.Size = new System.Drawing.Size(100, 30);
            this.btnThermostatSet.TabIndex = 6;
            this.btnThermostatSet.Text = "Установить";
            this.btnThermostatSet.UseVisualStyleBackColor = true;
            this.btnThermostatSet.Click += new System.EventHandler(this.btnThermostatSet_Click);
            // 
            // numTemp
            // 
            this.numTemp.Location = new System.Drawing.Point(120, 97);
            this.numTemp.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            this.numTemp.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numTemp.Name = "numTemp";
            this.numTemp.Size = new System.Drawing.Size(60, 20);
            this.numTemp.TabIndex = 7;
            this.numTemp.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // lblLockLabel
            // 
            this.lblLockLabel.AutoSize = true;
            this.lblLockLabel.Location = new System.Drawing.Point(200, 75);
            this.lblLockLabel.Name = "lblLockLabel";
            this.lblLockLabel.Size = new System.Drawing.Size(45, 13);
            this.lblLockLabel.TabIndex = 0;
            this.lblLockLabel.Text = "🔒 Замок:";
            // 
            // btnLockToggle
            // 
            this.btnLockToggle.Location = new System.Drawing.Point(203, 95);
            this.btnLockToggle.Name = "btnLockToggle";
            this.btnLockToggle.Size = new System.Drawing.Size(100, 30);
            this.btnLockToggle.TabIndex = 8;
            this.btnLockToggle.Text = "Замок";
            this.btnLockToggle.UseVisualStyleBackColor = true;
            this.btnLockToggle.Click += new System.EventHandler(this.btnLockToggle_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStatus.Location = new System.Drawing.Point(12, 245);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(360, 120);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Статус загрузки...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(384, 371);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.groupBoxDevices);
            this.Controls.Add(this.groupBoxVendor);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartHome Hub (С паттерном Abstract Factory)";
            this.groupBoxVendor.ResumeLayout(false);
            this.groupBoxDevices.ResumeLayout(false);
            this.groupBoxDevices.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLight)).EndInit();
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
        private System.Windows.Forms.PictureBox pictureBoxLight;
        private System.Windows.Forms.GroupBox groupBoxVendor;
        private System.Windows.Forms.GroupBox groupBoxDevices;
        private System.Windows.Forms.Label lblLightLabel;
        private System.Windows.Forms.Label lblTempLabel;
        private System.Windows.Forms.Label lblLockLabel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblBrightness;
    }
}