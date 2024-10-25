using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppLayer.PathComponents
{
    public class Node
    {
        public Point position;
        public double heading;
        public double curvature;
        public List<Segment> segments = new List<Segment>();
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
            graphics.DrawImage(icon, position.X - icon.Width / 2, position.Y - icon.Height / 2);

            // draw the path originating at this node if exists
            //graphics.DrawArc(RegularPen)
        }
        public static Bitmap RotateImage(Bitmap b, float angle)
        {
            //create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
            //make a graphics object from the empty bitmap
            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                //move rotation point to center of image
                g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
                //rotate
                g.RotateTransform(angle);
                //move image back
                g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
                //draw passed in image onto graphics object
                g.DrawImage(b, new Point(0, 0));
            }
            return returnBitmap;
        }
    }
}
