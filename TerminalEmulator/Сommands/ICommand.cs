using System;
using System.Collections.Generic;
using System.Text;

namespace TerminalEmulator.Сommands
{
    public interface ICommand
    {
        string Name { get; }
        string[] Aliases { get; }
        string Description { get; }
        string Execute(string[] args);
    }
}
