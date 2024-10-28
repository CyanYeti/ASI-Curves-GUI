using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace AppLayer.PathComponents
{
    public class Arc
    {
        public Point position;
        public double heading;
        public double curvature;
        public double length;
        public Arc(Point position, double heading, double curvature, double length) {
            this.position = position;
            this.heading = heading;
            this.curvature = curvature;
            this.length = length;
        }
        public Point Position(double l) {
            double x1 = position.X;
            double y1 = position.Y;
            double cq0 = Math.Cos(heading);
            double sq0 = Math.Sin(heading);
            double VERY_SMALL_CURVATURE = 1.0e-6;                         // (1/m)
            if (Math.Abs(curvature) < VERY_SMALL_CURVATURE) { // Draw a straight line
                x1 += l*cq0;
                y1 += l*sq0;
            } else {
                double cq = Math.Cos(heading + curvature*l);
                double sq = Math.Sin(heading + curvature*l);
                x1 += (sq-sq0)/curvature;
                y1 += (cq0-cq)/curvature;
            }
            return new Point((int)x1,(int)y1);
        }
        public void Draw(Pen pen, Graphics g)
        {
            double VERY_SMALL_CURVATURE = 1.0e-6;                         // (1/m)
            if (Math.Abs(curvature) < VERY_SMALL_CURVATURE) { // Draw a straight line
                int x0 = (int)(position.X);
                int y0 = (int)(position.Y);
                int x1 = (int)(position.X + length*Math.Cos(heading));
                int y1 = (int)(position.Y + length*Math.Sin(heading));
                // Transform from map to screen coordinates 
                g.DrawLine(pen, new Point(x0, -y0), new Point(x1, -y1));
            } else { // Draw an arc
                Single cx = (Single)(position.X - Math.Sin(heading)/curvature);
                Single cy = (Single)(position.Y + Math.Cos(heading)/curvature);
                Single radius = (Single)(Math.Abs(1/curvature));
                // Transform from map to screen coordinates
                Single x = cx - radius;
                Single y = -cy - radius;
                Single startAngle = (Single)((Math.PI/2 - heading)*180/Math.PI);
                Single sweepAngle = -(Single)((curvature*length)*180/Math.PI);
                if (curvature < 0) {
                    startAngle = (Single)((-Math.PI/2 - heading)*180/Math.PI);
                }
                g.DrawArc(pen, x, y, 2*radius, 2*radius, startAngle, sweepAngle);
            }
        }

    }
}
