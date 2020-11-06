using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TerminalEmulator.Сommands
{
    class RemoveCommand : ICommand
    {
        public string Name { get; } = "rm";
        public string[] Aliases { get; } = new string[] { "remove" };
        public string Description { get; } = "rm <FILE> - remove file";
        public string Execute(string[] args)
        {
            if (args.Length == 2)
            {
                string path = CommandHandler.Path + "/" + args[1];
                File.Delete(path);
                return null;
            }

            return "Command arguments were passed incorrectly.\nrm <FILE>";
        }
    }
}
