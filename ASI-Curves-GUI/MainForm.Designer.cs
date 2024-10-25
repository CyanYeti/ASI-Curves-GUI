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
            btnClear = new Button();
            btnToggleLoop = new Button();
            panel1 = new Panel();
            drawingPanel = new Panel();
            refreshTimer = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnClear
            // 
            btnClear.Location = new Point(3, 12);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(96, 23);
            btnClear.TabIndex = 0;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnToggleLoop
            // 
            btnToggleLoop.Location = new Point(3, 41);
            btnToggleLoop.Name = "btnToggleLoop";
            btnToggleLoop.Size = new Size(96, 23);
            btnToggleLoop.TabIndex = 1;
            btnToggleLoop.Text = "Toggle Loop";
            btnToggleLoop.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 64, 64);
            panel1.Controls.Add(btnClear);
            panel1.Controls.Add(btnToggleLoop);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(102, 452);
            panel1.TabIndex = 2;
            // 
            // drawingPanel
            // 
            drawingPanel.BackColor = Color.FromArgb(224, 224, 224);
            drawingPanel.Location = new Point(120, 12);
            drawingPanel.Name = "drawingPanel";
            drawingPanel.Size = new Size(668, 426);
            drawingPanel.TabIndex = 3;
            // 
            // refreshTimer
            // 
            refreshTimer.Tick += this.refreshTimer_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(drawingPanel);
            Controls.Add(panel1);
            Name = "MainForm";
            Text = "Form1";
            Load += MainForm_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnClear;
        private Button btnToggleLoop;
        private Panel panel1;
        private Panel panel2;
        private Panel drawingPanel;
        private System.Windows.Forms.Timer refreshTimer;
    }
}
