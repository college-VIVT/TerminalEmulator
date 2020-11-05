using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TerminalEmulator.Сommands
{
    public class ListCommand : ICommand
    {
        public string Name { get; } = "ls";

        public string Execute(string[] args)
        {
            List<string> items = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(CommandHandler.Path);

            foreach (var item in dir.GetDirectories()) items.Add(item.Name + "/");
            foreach (var item in dir.GetFiles()) items.Add(item.Name);

            return String.Join("\n", items.ToArray());
        }
    }
}
