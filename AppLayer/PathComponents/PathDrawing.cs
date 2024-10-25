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
        private readonly object _mylock = new object();
        public bool IsDirty { get; set; }
        public bool Draw(Graphics graphics, bool forceRedraw = false)
        {
            lock (_mylock)
            {
                if (!IsDirty && !forceRedraw) return false;

                graphics.Clear(Color.White);
                // Set any backgrounds
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
    }
}
