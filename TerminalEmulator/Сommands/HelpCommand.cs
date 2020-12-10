using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerminalEmulator.Сommands
{
    class HelpCommand : ICommand
    {
        public string Name { get; } = "help";
        public string[] Aliases { get; } = new string[] { "?" };
        public string Description { get; } = "? - get help";
        public string Execute(string[] args)
        {
            return string.Join("\n", CommandHandler.GetCommandDescription());
        }
    }
}
