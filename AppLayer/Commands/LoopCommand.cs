using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLayer.Commands
{
    public class LoopCommand : Command
    {
        public LoopCommand(params object[] commandParameters) { }
        public override bool Execute()
        {
            if (TargetDrawing == null) return false;
            TargetDrawing.ToggleLoop();
            return true;
        }
    }
}
