using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TerminalEmulator.Сommands
{
    class ChangeDirectoryCommand : ICommand
    {
        public string Name { get; } = "cd";

        public string Execute(string[] args)
        {
            if (args.Length == 2)
            {
                string path = CommandHandler.Path + "/" + args[1];
                if (Directory.Exists(path))
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    CommandHandler.ToDirectory(dir.FullName);
                } else
                {
                    return "Directory does not exist";
                }
            }
            else
            {
                return "Command arguments were passed incorrectly.\ncd <PATH>";
            }

            return null;
        }
    }
}
