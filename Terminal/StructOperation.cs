using System;
using System.Collections.Generic;
using System.Text;

namespace Terminal
{
    public abstract class StructOperation
    {
        public string name;
        public string path;
        protected StructOperation(string name)
        {
            this.name = name;
        }
        public abstract void PerformingOperation();
    }
}
