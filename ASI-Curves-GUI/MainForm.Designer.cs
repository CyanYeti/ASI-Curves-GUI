namespace ASI_Curves_GUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            drawingPanel = new Panel();
            refreshTimer = new System.Windows.Forms.Timer(components);
            menuStrip = new ToolStrip();
            btnClear = new ToolStripButton();
            btnToggleLoop = new ToolStripButton();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // drawingPanel
            // 
            drawingPanel.BackColor = Color.FromArgb(224, 224, 224);
            drawingPanel.Location = new Point(0, 53);
            drawingPanel.Name = "drawingPanel";
            drawingPanel.Size = new Size(800, 398);
            drawingPanel.TabIndex = 3;
            drawingPanel.MouseUp += drawingPanel_MouseUp;
            // 
            // refreshTimer
            // 
            refreshTimer.Tick += refreshTimer_Tick;
            // 
            // menuStrip
            // 
            menuStrip.AutoSize = false;
            menuStrip.Items.AddRange(new ToolStripItem[] { btnClear, btnToggleLoop });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(800, 50);
            menuStrip.TabIndex = 4;
            menuStrip.Text = "toolStrip1";
            // 
            // btnClear
            // 
            btnClear.AutoSize = false;
            btnClear.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnClear.Image = (Image)resources.GetObject("btnClear.Image");
            btnClear.ImageTransparentColor = Color.Magenta;
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(50, 50);
            btnClear.Text = "Clear";
            btnClear.Click += btnClear_Click;
            // 
            // btnToggleLoop
            // 
            btnToggleLoop.AutoSize = false;
            btnToggleLoop.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnToggleLoop.Image = (Image)resources.GetObject("btnToggleLoop.Image");
            btnToggleLoop.ImageTransparentColor = Color.Magenta;
            btnToggleLoop.Name = "btnToggleLoop";
            btnToggleLoop.Size = new Size(50, 50);
            btnToggleLoop.Text = "Toggle Loop";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip);
            Controls.Add(drawingPanel);
            Name = "MainForm";
            Text = "Form1";
            Load += MainForm_Load;
            Resize += MainForm_Resize;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel2;
        private Panel drawingPanel;
        private System.Windows.Forms.Timer refreshTimer;
        private ToolStrip menuStrip;
        private ToolStripButton btnClear;
        private ToolStripButton btnToggleLoop;
    }
}
