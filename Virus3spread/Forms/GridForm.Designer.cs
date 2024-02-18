namespace Virus3spread.Forms
{
    partial class GridForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridForm));
            SkglControl = new SkiaSharp.Views.Desktop.SKGLControl();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // SkglControl
            // 
            SkglControl.AllowDrop = true;
            SkglControl.BackColor = Color.Black;
            SkglControl.Dock = DockStyle.Fill;
            SkglControl.Location = new Point(0, 0);
            SkglControl.Margin = new Padding(6, 8, 6, 8);
            SkglControl.Name = "SkglControl";
            SkglControl.Size = new Size(1334, 781);
            SkglControl.TabIndex = 0;
            SkglControl.VSync = true;
            SkglControl.PaintSurface += SkglControl1_PaintSurface;
            SkglControl.SizeChanged += SkglControl1_SizeChanged;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Tick += Timer1_Tick_1;
            // 
            // GridForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1334, 781);
            Controls.Add(SkglControl);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5);
            Name = "GridForm";
            Text = "GridForm";
            ResumeLayout(false);
        }

        #endregion

        private SkiaSharp.Views.Desktop.SKGLControl SkglControl;
        private System.Windows.Forms.Timer timer1;
    }
}