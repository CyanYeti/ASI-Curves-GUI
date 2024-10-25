using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLayer.PathComponents
{
    internal class Segment
    {
        public Point position;
        public double heading;
        public double curvature;
        public double dkdl;
        public double displacement;
    }
}
