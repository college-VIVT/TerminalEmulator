using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TerminalEmulator.Сommands
{
    class CatenateCommand : ICommand
    {
        public string Name { get; } = "cat";

        public string Execute(string[] args)
        {
            if (args.Length == 2)
            {
                string path = CommandHandler.Path + "/" + args[1];
                return File.ReadAllText(path);
            }

            return "Command arguments were passed incorrectly.\ncat <FILE>";
        }
    }
}
