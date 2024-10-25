﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLayer.Commands
{
    public class Invoker
    {
        private Thread _worker;
        private bool _keepGoing;

        private readonly ConcurrentQueue<Command> _todoQueue = new ConcurrentQueue<Command>();
        private readonly AutoResetEvent _enqueueOccurred = new AutoResetEvent(false);

        public void Start()
        {
            _keepGoing = true;
            _worker = new Thread(Run);
            _worker.Start();
        }
        public void Stop()
        {
            _keepGoing = false;
        }
        public void EnqueueCommandForExecution(Command cmd)
        {
            if (cmd != null)
            {
                _todoQueue.Enqueue(cmd);
                _enqueueOccurred.Set();
            }
        }
        public void Run() 
        {
            while (_keepGoing)
            {
                Command cmd;
                if (_todoQueue.TryDequeue(out cmd))
                {
                    cmd.Execute();
                }
            }
        }
    }
}
