using System;
using System.Drawing;
using AppLayer.Commands;
using AppLayer.PathComponents;

namespace ASI_Curves_GUI
{
    public partial class MainForm : Form
    {
        private readonly PathDrawing _pathDrawing;
        private bool _forceRedraw;
        private readonly Invoker _invoker;

        private enum PossibleModes
        {
            Dragging,
            Placing
        }

        private PossibleModes _mode = PossibleModes.Dragging;

        private Bitmap? _imageBuffer;
        private Graphics _imageBufferGraphics;
        private Graphics _panelGraphics;



        public MainForm()
        {
            InitializeComponent();

            _pathDrawing = new PathDrawing();
            _invoker = new Invoker();

            CommandFactory.Instance.TargetDrawing = _pathDrawing;
            CommandFactory.Instance.Invoker = _invoker;

            _invoker.Start();

            System.Diagnostics.Debug.WriteLine("CONSOLE OUT");

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //ComputeDrawingPanelSize();
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
        private void ComputeDrawingPanelSize()
        {
            var width = Width;
            var height = Height - menuStrip.Height;

            drawingPanel.Size = new Size(width, height);
            drawingPanel.Location = new Point(0, menuStrip.Height);

            _imageBuffer = null;

            _forceRedraw = true;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            ComputeDrawingPanelSize();
        }

        private void drawingPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                CommandFactory.Instance.CreateAndDo("newnode", e.Location);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Tried Dragging");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CommandFactory.Instance.CreateAndDo("clear");
        }
    }
}
