using System;
using System.Collections.Generic;
using System.Text;

namespace TerminalEmulator.Сommands
{
    public interface ICommand
    {
        string Name { get; }
        string Execute(string[] args);
    }
}
