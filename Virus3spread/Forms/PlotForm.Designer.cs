namespace Virus3spread
{
    partial class PlotForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            BtnHoldStart = new Button();
            splitContainer1 = new SplitContainer();
            BtnExportCsv = new Button();
            AllNoneCheckbox = new CheckBox();
            LegendListBox = new CheckedListBox();
            CbCorssHair = new CheckBox();
            CbAutoAxis = new CheckBox();
            ChkShowLegend = new CheckBox();
            BtnAutoScaleY = new Button();
            BtnAutoScaleX = new Button();
            BtnManualScale = new Button();
            BtnAutoScaleTight = new Button();
            BtnAutoScale = new Button();
            DataTimer = new System.Windows.Forms.Timer(components);
            RenderTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // BtnHoldStart
            // 
            BtnHoldStart.Location = new Point(37, 34);
            BtnHoldStart.Name = "BtnHoldStart";
            BtnHoldStart.Size = new Size(190, 46);
            BtnHoldStart.TabIndex = 0;
            BtnHoldStart.Text = "hold / start";
            BtnHoldStart.UseVisualStyleBackColor = true;
            BtnHoldStart.Click += BtnHoldStart_Click;
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
            splitContainer1.Panel1.Controls.Add(AllNoneCheckbox);
            splitContainer1.Panel1.Controls.Add(LegendListBox);
            splitContainer1.Panel1.Controls.Add(CbCorssHair);
            splitContainer1.Panel1.Controls.Add(CbAutoAxis);
            splitContainer1.Panel1.Controls.Add(ChkShowLegend);
            splitContainer1.Panel1.Controls.Add(BtnAutoScaleY);
            splitContainer1.Panel1.Controls.Add(BtnAutoScaleX);
            splitContainer1.Panel1.Controls.Add(BtnManualScale);
            splitContainer1.Panel1.Controls.Add(BtnAutoScaleTight);
            splitContainer1.Panel1.Controls.Add(BtnAutoScale);
            splitContainer1.Panel1.Controls.Add(BtnHoldStart);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = SystemColors.Control;
            splitContainer1.Size = new Size(1964, 1118);
            splitContainer1.SplitterDistance = 320;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 2;
            // 
            // BtnExportCsv
            // 
            BtnExportCsv.Location = new Point(40, 488);
            BtnExportCsv.Name = "BtnExportCsv";
            BtnExportCsv.Size = new Size(179, 51);
            BtnExportCsv.TabIndex = 15;
            BtnExportCsv.Text = "export CSV";
            BtnExportCsv.UseVisualStyleBackColor = true;
            BtnExportCsv.Click += BtnExportCsv_Click;
            // 
            // AllNoneCheckbox
            // 
            AllNoneCheckbox.AutoSize = true;
            AllNoneCheckbox.Location = new Point(37, 569);
            AllNoneCheckbox.Name = "AllNoneCheckbox";
            AllNoneCheckbox.Size = new Size(203, 36);
            AllNoneCheckbox.TabIndex = 14;
            AllNoneCheckbox.Text = "check all / non";
            AllNoneCheckbox.UseVisualStyleBackColor = true;
            AllNoneCheckbox.CheckedChanged += AllNoneCheckbox_CheckedChanged;
            // 
            // LegendListBox
            // 
            LegendListBox.FormattingEnabled = true;
            LegendListBox.HorizontalScrollbar = true;
            LegendListBox.Location = new Point(37, 620);
            LegendListBox.Name = "LegendListBox";
            LegendListBox.Size = new Size(274, 292);
            LegendListBox.TabIndex = 13;
            LegendListBox.ItemCheck += LegendListBox_ItemCheck;
            // 
            // CbCorssHair
            // 
            CbCorssHair.AutoSize = true;
            CbCorssHair.Location = new Point(37, 988);
            CbCorssHair.Name = "CbCorssHair";
            CbCorssHair.Size = new Size(139, 36);
            CbCorssHair.TabIndex = 12;
            CbCorssHair.Text = "corsshair";
            CbCorssHair.UseVisualStyleBackColor = true;
            CbCorssHair.CheckedChanged += CbCorssHair_CheckedChanged;
            // 
            // CbAutoAxis
            // 
            CbAutoAxis.AutoSize = true;
            CbAutoAxis.Checked = true;
            CbAutoAxis.CheckState = CheckState.Checked;
            CbAutoAxis.Location = new Point(37, 931);
            CbAutoAxis.Margin = new Padding(6, 7, 6, 7);
            CbAutoAxis.Name = "CbAutoAxis";
            CbAutoAxis.Size = new Size(222, 36);
            CbAutoAxis.TabIndex = 11;
            CbAutoAxis.Text = "auto axis update";
            CbAutoAxis.TextAlign = ContentAlignment.TopCenter;
            CbAutoAxis.UseVisualStyleBackColor = true;
            CbAutoAxis.CheckedChanged += CbAutoAxis_CheckedChanged;
            // 
            // ChkShowLegend
            // 
            ChkShowLegend.AutoSize = true;
            ChkShowLegend.Checked = true;
            ChkShowLegend.CheckState = CheckState.Checked;
            ChkShowLegend.Location = new Point(37, 1046);
            ChkShowLegend.Margin = new Padding(5);
            ChkShowLegend.Name = "ChkShowLegend";
            ChkShowLegend.Size = new Size(182, 36);
            ChkShowLegend.TabIndex = 10;
            ChkShowLegend.Text = "show legend";
            ChkShowLegend.UseVisualStyleBackColor = true;
            ChkShowLegend.CheckedChanged += ChkShowLegend_CheckedChanged;
            // 
            // BtnAutoScaleY
            // 
            BtnAutoScaleY.Location = new Point(37, 418);
            BtnAutoScaleY.Name = "BtnAutoScaleY";
            BtnAutoScaleY.Size = new Size(190, 46);
            BtnAutoScaleY.TabIndex = 5;
            BtnAutoScaleY.Text = "Y scale";
            BtnAutoScaleY.UseVisualStyleBackColor = true;
            BtnAutoScaleY.Click += BtnAutoScaleY_Click;
            // 
            // BtnAutoScaleX
            // 
            BtnAutoScaleX.Location = new Point(37, 350);
            BtnAutoScaleX.Name = "BtnAutoScaleX";
            BtnAutoScaleX.Size = new Size(190, 46);
            BtnAutoScaleX.TabIndex = 4;
            BtnAutoScaleX.Text = "X scale";
            BtnAutoScaleX.UseVisualStyleBackColor = true;
            BtnAutoScaleX.Click += BtnAutoScaleX_Click;
            // 
            // BtnManualScale
            // 
            BtnManualScale.Location = new Point(37, 202);
            BtnManualScale.Name = "BtnManualScale";
            BtnManualScale.Size = new Size(190, 46);
            BtnManualScale.TabIndex = 3;
            BtnManualScale.Text = "manually scale";
            BtnManualScale.UseVisualStyleBackColor = true;
            BtnManualScale.Click += BtnManualScale_Click;
            // 
            // BtnAutoScaleTight
            // 
            BtnAutoScaleTight.Location = new Point(37, 134);
            BtnAutoScaleTight.Name = "BtnAutoScaleTight";
            BtnAutoScaleTight.Size = new Size(190, 46);
            BtnAutoScaleTight.TabIndex = 2;
            BtnAutoScaleTight.Text = "scale tight";
            BtnAutoScaleTight.UseVisualStyleBackColor = true;
            BtnAutoScaleTight.Click += BtnAutoScaleTight_Click;
            // 
            // BtnAutoScale
            // 
            BtnAutoScale.Location = new Point(37, 277);
            BtnAutoScale.Name = "BtnAutoScale";
            BtnAutoScale.Size = new Size(190, 46);
            BtnAutoScale.TabIndex = 1;
            BtnAutoScale.Text = "XY scale";
            BtnAutoScale.UseVisualStyleBackColor = true;
            BtnAutoScale.Click += BtnAutoScale_Click;
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
            RenderTimer.Interval = 20;
            RenderTimer.Tick += RenderTimer_Tick;
            // 
            // PlotForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1964, 1118);
            Controls.Add(splitContainer1);
            Name = "PlotForm";
            Text = "PlotForm";
            FormClosing += PlotForm_FormClosing;
            Load += PlotForm_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button BtnHoldStart;
        private SplitContainer splitContainer1;
        private Button BtnAutoScale;
        private Button BtnAutoScaleTight;
        private Button BtnManualScale;
        private Button BtnAutoScaleX;
        private Button BtnAutoScaleY;
        private CheckBox ChkShowLegend;
        private CheckBox CbAutoAxis;
        private System.Windows.Forms.Timer DataTimer;
        private System.Windows.Forms.Timer RenderTimer;
        private CheckBox CbCorssHair;
        private CheckedListBox LegendListBox;
        private CheckBox AllNoneCheckbox;
        private Button BtnExportCsv;
    }
}
