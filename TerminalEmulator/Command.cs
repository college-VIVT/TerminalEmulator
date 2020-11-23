using System;
using System.Collections.Generic;
using System.Text;

namespace TerminalEmulator
{
    public class Command : IAct
    {
        public string Name { get; set; }
        private Action operation;
        public Command(string name, Action operation) 
        {
            this.Name = name;
            this.operation = operation; 
        }
        public void Operation()
        {
            operation();
        }
    }
}
