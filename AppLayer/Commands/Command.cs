using AppLayer.PathComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLayer.Commands
{
    public abstract class Command
    {
        public PathDrawing? TargetDrawing { get; set; }

        public abstract bool Execute();
    }
}
