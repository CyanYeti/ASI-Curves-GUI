using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppLayer.PathComponents
{
    public class Node
    {
        public Point position;
        public Point next;
        public double heading;
        public double curvature;
        public List<Segment> segments = new List<Segment>();
        public List<Arc> arcs = new List<Arc>();

        public Bitmap icon;
        public Node(Point position, double heading) 
        {
            this.heading = heading;
            this.position = position;
            this.next = position;
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
            var rotated = RotateImage(icon, heading * (180/Math.PI));
            graphics.DrawImage(rotated, position.X - icon.Width / 2, position.Y - icon.Height / 2);

            // draw the path originating at this node if exists
            // DrawArcBetweenTwoPoints(graphics, RegularPen, (PointF)position, new PointF(100,100), 100);
            if (next != position) 
            {
                DrawBezierCurve(RegularPen, graphics);
            }
            // graphics.DrawArc(RegularPen, position.X, position.Y, position.X, position.Y, 45, 270);
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
        internal void DrawBezierCurve(Pen pen, Graphics g)
        {
            var LinePath = new GraphicsPath();
            var p1 = new Point(position.X, position.Y); //starting point
            var p2 = new Point(position.X + 50, position.X + 50); //first control point
            var p3 = new Point(position.X + 50, position.X + 50); //second control point
            var p4 = new Point(next.X, next.Y); //ending point
            LinePath.AddBezier(p1, p3, p3, p4);
            g.DrawPath(pen, LinePath);
        }
    }
}
