using System;
using System.Drawing;
using AppLayer.PathComponents;

namespace ASI_Curves_GUI
{
    public partial class MainForm : Form
    {
        private readonly PathDrawing _pathDrawing;
        private bool _forceRedraw;

        private Bitmap _imageBuffer;
        private Graphics _imageBufferGraphics;
        private Graphics _panelGraphics;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            refreshTimer.Start();
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            DisplayPath();
        }

        private void DisplayPath()
        {
            if (_imageBuffer == null)
            {
                _imageBuffer = new Bitmap(drawingPanel.Width, drawingPanel.Height);
                _imageBufferGraphics = Graphics.FromImage(_imageBuffer);
                _panelGraphics = drawingPanel.CreateGraphics();
            }

            if (_pathDrawing.Draw(_imageBufferGraphics, _forceRedraw))
            {
                _panelGraphics.DrawImageUnscaled(_imageBuffer, 0, 0);
            }

            _forceRedraw = false;
        }
    }
}
