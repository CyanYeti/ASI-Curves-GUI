using AppLayer.PathComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLayer.Commands
{
    public class CommandFactory
    {
        private static CommandFactory? _instance;
        private static readonly object _mylock = new object();

        private CommandFactory() { }
        // Singleton command factory
        public static CommandFactory Instance
        {
            get
            {
                lock (_mylock)
                {
                    if (_instance == null)
                    {
                        _instance = new CommandFactory();
                    }
                    return _instance;
                }
            }
        }

        public PathDrawing? TargetDrawing { get; set; }
        public Invoker? Invoker { get; set; }

        public void CreateAndDo(string commandType, params object[] commandParameters)
        {
            if (string.IsNullOrEmpty(commandType)) return;

            if (TargetDrawing == null) return;

            Command? command = null;
            switch (commandType.Trim().ToUpper())
            {
                case "NEWNODE":
                    command = new NewNodeCommand(commandParameters);
                    break;
                case "CLEAR":
                    command = new ClearCommand(commandParameters);
                    break;
                case "LOOP":
                    command = new LoopCommand(commandParameters);
                    break;
            }

            if (command != null && Invoker != null)
            {
                command.TargetDrawing = TargetDrawing;
                Invoker.EnqueueCommandForExecution(command);
            }
        }
    }
}
