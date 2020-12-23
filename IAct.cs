using System;
using System.Collections.Generic;
using System.Text;

namespace TerminalEmulator
{
    public interface IAct
    {
        public string Name { get; set; }
        void Operation();
    }
}
