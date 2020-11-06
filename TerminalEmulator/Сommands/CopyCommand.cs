using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TerminalEmulator.Сommands
{
    class CopyCommand : ICommand
    {
        public string Name { get; } = "cp";
        public string[] Aliases { get; } = new string[] { "copy" };
        public string Description { get; } = "cp <SRC_FILE> <DST_FILE> - Copy file";
        public string Execute(string[] args)
        {
            if (args.Length == 3)
            {
                string pathSrc = CommandHandler.Path + "/" + args[1];
                string pathDst = CommandHandler.Path + "/" + args[2];
                File.Copy(pathSrc, pathDst);
                return null;
            }

            return "Command arguments were passed incorrectly.\ncp <SRC_FILE> <DST_FILE>";
        }
    }
}
