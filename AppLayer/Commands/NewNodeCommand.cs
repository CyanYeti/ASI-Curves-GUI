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
        public NewNodeCommand(params object[] commandParameters)
        {
            if (commandParameters.Length > 0)
            {
                _position = (Point)commandParameters[0];
            }
        }
        public override bool Execute()
        {
            throw new NotImplementedException();
        }
    }
}
