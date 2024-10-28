using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AppLayer.PathComponents
{
    public class Node
    {
        public Point position;
        public double heading;
        public double curvature;
        public bool arc_constraint = false; 
        public List<Arc> arcs = new List<Arc>();

        public Bitmap icon;
        public Node(Point position, double heading) 
        {
            this.heading = heading;
            this.position = position;
            using (MemoryStream ms = new MemoryStream(AppLayer.Properties.Resources.icon))
            {
                icon = new Bitmap(ms);
                icon = new Bitmap(icon, new Size(icon.Width / 4, icon.Height / 4));
            }

        }

        public static Pen RegularPen { get; set; } = new Pen(Color.Black);
        public void Draw(Graphics graphics)
        {
            if (graphics == null) return;
            // Draw the node point, pointing to heading
            int x = position.X;
            int y = -position.Y; // Transform from map to screen
            var rotated = RotateImage(icon, (Math.PI/2 - heading) * (180/Math.PI));
            graphics.DrawImage(rotated, x - icon.Width / 2, y - icon.Height / 2);
            foreach (var arc in arcs)
            {
                arc.Draw(RegularPen, graphics);
                if (arc_constraint) {
                    Point mid = arc.Position(arc.length/2);
                    SolidBrush midbrush = new SolidBrush(Color.DarkCyan); 
                    int rad = 20;
                    graphics.FillEllipse(midbrush, mid.X-rad/2, -mid.Y-rad/2, rad, rad); // Transform from map to screen
                }

            }
        }
        public static Bitmap RotateImage(Bitmap b, double angle)
        {
            //create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
            //make a graphics object from the empty bitmap
            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                //move rotation point to center of image
                g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
                //rotate
                g.RotateTransform((float)angle);
                //move image back
                g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
                //draw passed in image onto graphics object
                g.DrawImage(b, new Point(0, 0));
            }
            return returnBitmap;
        }
    }
}
