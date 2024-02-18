namespace Virus3spread.Forms
{
    partial class PhaseChartForm
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
            splitContainer1 = new SplitContainer();
            BtnExportCsv = new Button();
            BtnAutoScaleY = new Button();
            CbCorssHair = new CheckBox();
            BtnAutoScaleX = new Button();
            BtnAutoScale = new Button();
            BtnManualScale = new Button();
            BtnAutoScaleTight = new Button();
            BtnHoldStart = new Button();
            CbAutoAxis = new CheckBox();
            label2 = new Label();
            YvalueListBox = new ListBox();
            label1 = new Label();
            XvalueListBox = new ListBox();
            DataTimer = new System.Windows.Forms.Timer(components);
            RenderTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = SystemColors.ControlLightLight;
            splitContainer1.Panel1.Controls.Add(BtnExportCsv);
            splitContainer1.Panel1.Controls.Add(BtnAutoScaleY);
            splitContainer1.Panel1.Controls.Add(CbCorssHair);
            splitContainer1.Panel1.Controls.Add(BtnAutoScaleX);
            splitContainer1.Panel1.Controls.Add(BtnAutoScale);
            splitContainer1.Panel1.Controls.Add(BtnManualScale);
            splitContainer1.Panel1.Controls.Add(BtnAutoScaleTight);
            splitContainer1.Panel1.Controls.Add(BtnHoldStart);
            splitContainer1.Panel1.Controls.Add(CbAutoAxis);
            splitContainer1.Panel1.Controls.Add(label2);
            splitContainer1.Panel1.Controls.Add(YvalueListBox);
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(XvalueListBox);
            splitContainer1.Size = new Size(1376, 961);
            splitContainer1.SplitterDistance = 322;
            splitContainer1.TabIndex = 0;
            // 
            // BtnExportCsv
            // 
            BtnExportCsv.Location = new Point(35, 509);
            BtnExportCsv.Name = "BtnExportCsv";
            BtnExportCsv.Size = new Size(190, 51);
            BtnExportCsv.TabIndex = 20;
            BtnExportCsv.Text = "export CSV";
            BtnExportCsv.UseVisualStyleBackColor = true;
            BtnExportCsv.Click += BtnExportCsv_Click;
            // 
            // BtnAutoScaleY
            // 
            BtnAutoScaleY.Location = new Point(35, 437);
            BtnAutoScaleY.Name = "BtnAutoScaleY";
            BtnAutoScaleY.Size = new Size(190, 46);
            BtnAutoScaleY.TabIndex = 19;
            BtnAutoScaleY.Text = "Y scale";
            BtnAutoScaleY.UseVisualStyleBackColor = true;
            BtnAutoScaleY.Click += BtnAutoScaleY_Click;
            // 
            // CbCorssHair
            // 
            CbCorssHair.AutoSize = true;
            CbCorssHair.Location = new Point(35, 897);
            CbCorssHair.Name = "CbCorssHair";
            CbCorssHair.Size = new Size(139, 36);
            CbCorssHair.TabIndex = 18;
            CbCorssHair.Text = "corsshair";
            CbCorssHair.UseVisualStyleBackColor = true;
            CbCorssHair.CheckedChanged += CbCorssHair_CheckedChanged;
            // 
            // BtnAutoScaleX
            // 
            BtnAutoScaleX.Location = new Point(35, 357);
            BtnAutoScaleX.Name = "BtnAutoScaleX";
            BtnAutoScaleX.Size = new Size(190, 46);
            BtnAutoScaleX.TabIndex = 17;
            BtnAutoScaleX.Text = "X scale";
            BtnAutoScaleX.UseVisualStyleBackColor = true;
            BtnAutoScaleX.Click += BtnAutoScaleX_Click;
            // 
            // BtnAutoScale
            // 
            BtnAutoScale.Location = new Point(35, 279);
            BtnAutoScale.Name = "BtnAutoScale";
            BtnAutoScale.Size = new Size(190, 46);
            BtnAutoScale.TabIndex = 16;
            BtnAutoScale.Text = "XY scale";
            BtnAutoScale.UseVisualStyleBackColor = true;
            BtnAutoScale.Click += BtnAutoScale_Click;
            // 
            // BtnManualScale
            // 
            BtnManualScale.Location = new Point(35, 207);
            BtnManualScale.Name = "BtnManualScale";
            BtnManualScale.Size = new Size(190, 46);
            BtnManualScale.TabIndex = 15;
            BtnManualScale.Text = "manually scale";
            BtnManualScale.UseVisualStyleBackColor = true;
            BtnManualScale.Click += BtnManualScale_Click;
            // 
            // BtnAutoScaleTight
            // 
            BtnAutoScaleTight.Location = new Point(35, 132);
            BtnAutoScaleTight.Name = "BtnAutoScaleTight";
            BtnAutoScaleTight.Size = new Size(190, 46);
            BtnAutoScaleTight.TabIndex = 14;
            BtnAutoScaleTight.Text = "scale tight";
            BtnAutoScaleTight.UseVisualStyleBackColor = true;
            BtnAutoScaleTight.Click += BtnAutoScaleTight_Click;
            // 
            // BtnHoldStart
            // 
            BtnHoldStart.Location = new Point(35, 36);
            BtnHoldStart.Name = "BtnHoldStart";
            BtnHoldStart.Size = new Size(190, 46);
            BtnHoldStart.TabIndex = 13;
            BtnHoldStart.Text = "hold / start";
            BtnHoldStart.UseVisualStyleBackColor = true;
            BtnHoldStart.Click += BtnHoldStart_Click;
            // 
            // CbAutoAxis
            // 
            CbAutoAxis.AutoSize = true;
            CbAutoAxis.Checked = true;
            CbAutoAxis.CheckState = CheckState.Checked;
            CbAutoAxis.Location = new Point(35, 841);
            CbAutoAxis.Margin = new Padding(6, 7, 6, 7);
            CbAutoAxis.Name = "CbAutoAxis";
            CbAutoAxis.Size = new Size(222, 36);
            CbAutoAxis.TabIndex = 12;
            CbAutoAxis.Text = "auto axis update";
            CbAutoAxis.TextAlign = ContentAlignment.TopCenter;
            CbAutoAxis.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 709);
            label2.Name = "label2";
            label2.Size = new Size(92, 32);
            label2.TabIndex = 3;
            label2.Text = "Y Value";
            // 
            // YvalueListBox
            // 
            YvalueListBox.FormattingEnabled = true;
            YvalueListBox.Location = new Point(35, 744);
            YvalueListBox.Name = "YvalueListBox";
            YvalueListBox.Size = new Size(380, 68);
            YvalueListBox.TabIndex = 2;
            YvalueListBox.SelectedIndexChanged += YvalueListBox_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 575);
            label1.Name = "label1";
            label1.Size = new Size(93, 32);
            label1.TabIndex = 1;
            label1.Text = "X Value";
            // 
            // XvalueListBox
            // 
            XvalueListBox.FormattingEnabled = true;
            XvalueListBox.Location = new Point(35, 620);
            XvalueListBox.Name = "XvalueListBox";
            XvalueListBox.Size = new Size(380, 68);
            XvalueListBox.TabIndex = 0;
            XvalueListBox.SelectedIndexChanged += XvalueListBox_SelectedIndexChanged;
            // 
            // DataTimer
            // 
            DataTimer.Enabled = true;
            DataTimer.Interval = 1;
            DataTimer.Tick += DataTimer_Tick;
            // 
            // RenderTimer
            // 
            RenderTimer.Enabled = true;
            RenderTimer.Interval = 200;
            RenderTimer.Tick += RenderTimer_Tick;
            // 
            // PhaseChartForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1376, 961);
            Controls.Add(splitContainer1);
            Name = "PhaseChartForm";
            Text = "PhaseChartForm";
            FormClosing += PhaseChartForm_FormClosing;
            Load += PhaseChartForm_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Label label1;
        private ListBox XvalueListBox;
        private Label label2;
        private ListBox YvalueListBox;
        private System.Windows.Forms.Timer DataTimer;
        private System.Windows.Forms.Timer RenderTimer;
        private CheckBox CbAutoAxis;
        private Button BtnHoldStart;
        private Button BtnAutoScaleTight;
        private Button BtnManualScale;
        private Button BtnAutoScale;
        private Button BtnAutoScaleX;
        private CheckBox CbCorssHair;
        private Button BtnAutoScaleY;
        private Button BtnExportCsv;
    }
}