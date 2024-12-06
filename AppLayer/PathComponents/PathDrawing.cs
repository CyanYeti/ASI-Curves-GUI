// #define ASIAVAILABLE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if ASIAVAILABLE
using Asi;
#endif

namespace AppLayer.PathComponents
{
    public class PathDrawing

    {
        private readonly List<Node> _nodes = new List<Node>();
        private bool _is_closed_loop = false;
        private readonly object _mylock = new object();
        public bool IsDirty { get; set; }
        public static double PiMod(double value)
        {
            return (((value + System.Math.PI) % (System.Math.PI*2)) + (System.Math.PI*2)) % (System.Math.PI*2) - System.Math.PI;
        }                
        public bool Draw(Graphics graphics, bool forceRedraw = false)
        {
            lock (_mylock)
            {
                if (!IsDirty && !forceRedraw) return false;

                graphics.Clear(Color.White);
                // Set any backgrounds

#if ASIAVAILABLE
                // *******************************************************************************************
                // Call to clothoid library. The libary will take the position heading and curvature
                // of each node and generate arcs. It may also modify the heading and/or curvature of each node.
                // For now fake something: Draw arcs between even to odd indexed nodes. Draw straight line between
                // odd to even indexed nodes
                var nodes = new List<Asi.CoverageLibraries.SegmentState>();
                foreach (var node in _nodes) {
                    var point = new Asi.CoverageLibraries.Point2D(node.position.X, node.position.Y);
                    nodes.Add(new Asi.CoverageLibraries.SegmentState(point, node.heading, node.curvature));
                }
                var results = Asi.CoverageLibraries.ClothoidGeometry.ArcCloFromNodes(nodes.ToArray(), 20, _is_closed_loop);
                if (_nodes.Count == results.Length) {
                    var i = 0;
                    foreach (var tup in results) {
                        // Update position, heading, curvature of each node
                        _nodes[i].position.X = (int)tup.Item1.XY.X;
                        _nodes[i].position.Y = (int)tup.Item1.XY.Y;
                        _nodes[i].heading = tup.Item1.Heading;
                        _nodes[i].curvature = tup.Item1.Curvature;
                        // Set constraint based on number of arcs, (TODO: Maybe get explicitly at some point)
                        _nodes[i].arc_constraint = tup.Item2.Length == 1;
                        // Clear and read new arcs
                        _nodes[i].arcs.Clear();
                        foreach (var seg in tup.Item2) {
                            _nodes[i].arcs.Add(new Arc(new Point((int)seg.StartX, (int)seg.StartY), seg.InitialHeading, seg.InitialCurvature, seg.Length));
                        }
                        // Console.WriteLine("X={0:F} Y={1:F} Narcs={2:D}", tup.Item1.XY.X, tup.Item1.XY.Y,tup.Item2.Length);
                        i += 1;
                    }
                }
                // *******************************************************************************************
#else
                double VERY_SMALL_CURVATURE = 1.0e-6;                         // (1/m)
                if (_nodes.Count > 1) {
                    Node n2 = _nodes[0];
                    for (int i = 0; i < _nodes.Count; i++) {
                        Node n1 = n2;
                        n2 = _nodes[(i+1) % _nodes.Count];
                        double dy = n2.position.Y - n1.position.Y;
                        double dx = n2.position.X - n1.position.X;
                        double L = Math.Sqrt(dy*dy + dx*dx);
                        double q = Math.Atan2(dy, dx);
                        _nodes[i].arcs.Clear();
                        _nodes[i].arc_constraint = false;
                        if (i+1 == _nodes.Count && !_is_closed_loop) {
                        } else if (i+1 == _nodes.Count || (i % 2 == 1)) {
                            // Straight segment for now
                            _nodes[i].arcs.Add(new Arc(n1.position, q, 0, L));
                        } else { // even functions are simple arcs
                            _nodes[i].arc_constraint = true;
                            double dq = PiMod(q - n1.heading);
                            if (Math.Max(L, Math.Abs(dq)) < VERY_SMALL_CURVATURE) { // treat as line
                                _nodes[i].heading = q;
                                _nodes[i].arcs.Add(new Arc(n1.position, q, 0, L));
                                _nodes[(i+1) % _nodes.Count].heading = q;
                            } else {
                                double chord = L, sdq = Math.Sin(dq);
                                double k = sdq/(chord/2);
                                if (Math.Abs(k) > VERY_SMALL_CURVATURE) L = Math.Abs(dq*2/k);
                                _nodes[(i+1) % _nodes.Count].heading = _nodes[i].heading + 2*dq;
                                _nodes[i].arcs.Add(new Arc(n1.position, n1.heading, k, L));
                            }
                        }
                    }
                }
#endif

                // Draw each node
                foreach (var n in _nodes)
                {
                    n.Draw(graphics);
                }
                IsDirty = false;
            }
            return true;
        }
        public void Add(Node node)
        {
            if (node == null) return;
            lock (_mylock)
            {
                _nodes.Add(node);
                IsDirty = true;
            }
        }
        public void Clear()
        {
            lock(_mylock)
            {
                _nodes.Clear();
                IsDirty = true;
            }
        }
        public void ToggleLoop()
        {
            lock(_mylock)
            {
                _is_closed_loop = !_is_closed_loop;
                IsDirty = true;
            }
        }
    }
}
