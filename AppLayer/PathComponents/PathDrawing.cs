using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                // Will be a call to clothoid library. The libary will take the position heading and curvature
                // of each node and generate arcs. It may also modify the heading and/or curvature of each node.
                // For now fake something: Draw arcs between even to odd indexed nodes. Draw straight line between
                // odd to even indexed nodes
                // *******************************************************************************************
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
                // *******************************************************************************************

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
