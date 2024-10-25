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
        public Node(Point position) 
        {
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
    }
}
