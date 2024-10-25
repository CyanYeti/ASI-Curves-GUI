using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppLayer.Commands
{
    public class NewNodeCommand : Command
    {
        private Point _position;
        private double _heading;
        public NewNodeCommand(params object[] commandParameters)
        {
            if (commandParameters.Length > 0)
            {
                _position = (Point)commandParameters[0];
            }
            if (commandParameters.Length > 1)
            {
                _heading = (double)commandParameters[1];
            }
        }
        public override bool Execute()
        {
            if (TargetDrawing == null) return false;
            TargetDrawing.Add(new PathComponents.Node(_position, _heading));
            return true;
        }
    }
}
