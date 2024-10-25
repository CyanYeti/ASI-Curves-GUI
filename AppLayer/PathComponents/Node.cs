using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLayer.PathComponents
{
    public class Node
    {
        public Point position;
        public double heading;
        public double curvature;
        public List<Segment> segments;
        public List<Arc> arcs;
    }
}
