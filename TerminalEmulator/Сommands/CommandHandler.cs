using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TerminalEmulator.Сommands
{
    public static class CommandHandler
    {
        public static string Path { get; private set; }
        private static Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();
        private static Dictionary<string, string> aliases = new Dictionary<string, string>();

        public static void ToDirectory(string path)
        {
            Path = path;
        }
        public static bool Register(ICommand command)
        {
            if (commands.ContainsKey(command.Name))
            {
                return false;
            }
            
            commands.Add(command.Name, command);

            foreach (string alias in command.Aliases)
            {
                if (!aliases.ContainsKey(alias))
                    aliases.Add(alias, command.Name);
            }

            return true;
        }

        public static string Execute(string command)
        {
            string[] args = command.Split(' ');

            if(commands.ContainsKey(args[0]))
            {
                return commands[args[0]].Execute(args);
            } else if(aliases.ContainsKey(args[0]))
            {
                return commands[aliases[args[0]]].Execute(args);
            } else
            {
                return "Command not found";
            }
        }

        public static List<string> GetCommandDescription()
        {
            List<string> descriptions = new List<string>();

            foreach (KeyValuePair<string, ICommand> command in commands)
            {
                descriptions.Add(command.Value.Description);
            }

            return descriptions;
        }
    }
}
