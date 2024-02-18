namespace Virus3spread
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tabControl = new TabControl();
            tabPage1 = new TabPage();
            splitContainer1 = new SplitContainer();
            ConfigurationPropertyGrid = new PropertyGrid();
            eventsListBox = new VirusSpreadLibrary.AppProperties.EventsListBox();
            ResetSimulationButton = new Button();
            ShowSimulationCheckBox = new CheckBox();
            ShowPhaseChart_button = new Button();
            ShowChart_button = new Button();
            SaveConfig_button3 = new Button();
            LoadConfig_button2 = new Button();
            StartHoldSimulationButton = new Button();
            tabPage2 = new TabPage();
            Timer = new System.Windows.Forms.Timer(components);
            tabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPage1);
            tabControl.Controls.Add(tabPage2);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1529, 774);
            tabControl.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(splitContainer1);
            tabPage1.Location = new Point(8, 46);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1513, 720);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Settings";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(ConfigurationPropertyGrid);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(eventsListBox);
            splitContainer1.Panel2.Controls.Add(ResetSimulationButton);
            splitContainer1.Panel2.Controls.Add(ShowSimulationCheckBox);
            splitContainer1.Panel2.Controls.Add(ShowPhaseChart_button);
            splitContainer1.Panel2.Controls.Add(ShowChart_button);
            splitContainer1.Panel2.Controls.Add(SaveConfig_button3);
            splitContainer1.Panel2.Controls.Add(LoadConfig_button2);
            splitContainer1.Panel2.Controls.Add(StartHoldSimulationButton);
            splitContainer1.Size = new Size(1507, 714);
            splitContainer1.SplitterDistance = 1210;
            splitContainer1.TabIndex = 3;
            // 
            // ConfigurationPropertyGrid
            // 
            ConfigurationPropertyGrid.Dock = DockStyle.Fill;
            ConfigurationPropertyGrid.Location = new Point(0, 0);
            ConfigurationPropertyGrid.Name = "ConfigurationPropertyGrid";
            ConfigurationPropertyGrid.Size = new Size(1210, 714);
            ConfigurationPropertyGrid.TabIndex = 2;
            ConfigurationPropertyGrid.PropertyValueChanged += ConfigurationPropertyGrid_PropertyValueChanged;
            // 
            // eventsListBox
            // 
            eventsListBox.Dock = DockStyle.Bottom;
            eventsListBox.FormattingEnabled = true;
            eventsListBox.Location = new Point(0, 602);
            eventsListBox.Name = "eventsListBox";
            eventsListBox.Size = new Size(293, 112);
            eventsListBox.TabIndex = 12;
            // 
            // ResetSimulationButton
            // 
            ResetSimulationButton.Location = new Point(44, 163);
            ResetSimulationButton.Name = "ResetSimulationButton";
            ResetSimulationButton.Size = new Size(205, 48);
            ResetSimulationButton.TabIndex = 11;
            ResetSimulationButton.Text = "reset";
            ResetSimulationButton.UseVisualStyleBackColor = true;
            ResetSimulationButton.Click += ResetSimulationButton_Click;
            // 
            // ShowSimulationCheckBox
            // 
            ShowSimulationCheckBox.AutoSize = true;
            ShowSimulationCheckBox.Location = new Point(44, 21);
            ShowSimulationCheckBox.Name = "ShowSimulationCheckBox";
            ShowSimulationCheckBox.Size = new Size(222, 36);
            ShowSimulationCheckBox.TabIndex = 10;
            ShowSimulationCheckBox.Text = "show Simulation";
            ShowSimulationCheckBox.UseVisualStyleBackColor = true;
            ShowSimulationCheckBox.CheckedChanged += ShowSimulationCheckBox_CheckedChanged;
            // 
            // ShowPhaseChart_button
            // 
            ShowPhaseChart_button.Location = new Point(44, 345);
            ShowPhaseChart_button.Name = "ShowPhaseChart_button";
            ShowPhaseChart_button.Size = new Size(205, 46);
            ShowPhaseChart_button.TabIndex = 8;
            ShowPhaseChart_button.Text = "PhaseChart";
            ShowPhaseChart_button.UseVisualStyleBackColor = true;
            ShowPhaseChart_button.Click += ShowPhaseChart_button_Click;
            // 
            // ShowChart_button
            // 
            ShowChart_button.Location = new Point(44, 277);
            ShowChart_button.Name = "ShowChart_button";
            ShowChart_button.Size = new Size(205, 46);
            ShowChart_button.TabIndex = 7;
            ShowChart_button.Text = "Chart";
            ShowChart_button.UseVisualStyleBackColor = true;
            ShowChart_button.Click += ShowChart_button_Click;
            // 
            // SaveConfig_button3
            // 
            SaveConfig_button3.Location = new Point(44, 515);
            SaveConfig_button3.Name = "SaveConfig_button3";
            SaveConfig_button3.Size = new Size(205, 46);
            SaveConfig_button3.TabIndex = 6;
            SaveConfig_button3.Text = "Save config";
            SaveConfig_button3.UseVisualStyleBackColor = true;
            SaveConfig_button3.Click += SaveConfig_button3_Click;
            // 
            // LoadConfig_button2
            // 
            LoadConfig_button2.Location = new Point(44, 442);
            LoadConfig_button2.Name = "LoadConfig_button2";
            LoadConfig_button2.Size = new Size(205, 46);
            LoadConfig_button2.TabIndex = 5;
            LoadConfig_button2.Text = "Load config";
            LoadConfig_button2.UseVisualStyleBackColor = true;
            LoadConfig_button2.Click += LoadConfig_button2_Click;
            // 
            // StartHoldSimulationButton
            // 
            StartHoldSimulationButton.Location = new Point(44, 88);
            StartHoldSimulationButton.Margin = new Padding(5);
            StartHoldSimulationButton.Name = "StartHoldSimulationButton";
            StartHoldSimulationButton.Size = new Size(205, 46);
            StartHoldSimulationButton.TabIndex = 3;
            StartHoldSimulationButton.Text = "start / hold";
            StartHoldSimulationButton.UseVisualStyleBackColor = true;
            StartHoldSimulationButton.Click += StartHoldSimulationButton_Click;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(8, 46);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1513, 720);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Rate Settings";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // Timer
            // 
            Timer.Interval = 1;
            Timer.Tick += Timer_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1529, 774);
            Controls.Add(tabControl);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Virus3spread MainForm";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            tabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private SplitContainer splitContainer1;
        private PropertyGrid ConfigurationPropertyGrid;
        private Button StartHoldSimulationButton;
        private Button LoadConfig_button2;
        private Button SaveConfig_button3;
        private Button ShowChart_button;
        private Button ShowPhaseChart_button;
        private System.Windows.Forms.Timer Timer;
        private CheckBox ShowSimulationCheckBox;
        private Button ResetSimulationButton;
        private VirusSpreadLibrary.AppProperties.EventsListBox eventsListBox;
    }
}