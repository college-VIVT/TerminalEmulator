using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace TerminalEmulator.Сommands
{
    class TouchCommand : ICommand
    {
        public string Name { get; } = "touch";

        public string Execute(string[] args)
        {
            if(args.Length == 2)
            {
                File.Create(CommandHandler.Path + "/" + args[1]).Dispose();
                return null;
            }

            return "Command arguments were passed incorrectly.\ntouch <FILE_NAME>";
        }
    }
}
