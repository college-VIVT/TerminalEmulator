using System;
using System.Collections.Generic;
using System.Text;

namespace Terminal
{
    public class Command : StructOperation
    {
        private Action operation;
        public Command(string name, Action operation) : base(name) { this.operation = operation; }


        public override sealed void PerformingOperation()
        {
            operation();
        }
    }
}
