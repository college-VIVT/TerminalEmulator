using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CLIemulator
{
    public static class CommandSeparator
    {
        static string commandRegExPattern = @"("".*?""|.*?) ";

        public static bool OnNewCommand(string commandLine)
        {
            if (commandLine != "")
            {
                commandLine += " ";

                var command = "";
                var args = new List<string>();
                var maches = Regex.Matches(commandLine, commandRegExPattern, RegexOptions.Multiline);
                command = maches[0].Groups[1].Value;

                var machesCount = maches.Count;
                for (var i = 1; i < machesCount; i++)
                    args.Add(maches[i].Groups[1].Value.Replace("\"", ""));

                return CommandDistribute(command, args);
            }

            return false;
        }

        public static bool CommandDistribute(string commandName, List<string> commandArgs)
        {
            commandName = commandName.ToLower();
            var argsCount = commandArgs.Count;
            switch (commandName)
            {
                case "cd":
                    if (CheckArgs(commandName, CommandStats.command_CD_argsCount, argsCount))
                        OnCommand.OnCommand_CD(commandArgs, argsCount);
                    break;
                case "ls": case "dir":
                    if (CheckArgs(commandName, CommandStats.command_LS_DIR_argsCount, argsCount))
                        OnCommand.OnCommand_LS_DIR(commandArgs, argsCount);
                    break;
                case "help": case "?":
                    if (CheckArgs(commandName, CommandStats.command_HELP_argsCount, argsCount))
                        OnCommand.OnCommand_HELP();
                    break;
                case "move": case "mv":
                    if (CheckArgs(commandName, CommandStats.command_MOVE_MV_argsCount, argsCount))
                        OnCommand.OnCommand_MOVE_MV(commandArgs, argsCount);
                    break;
                case "copy": case "cp":
                    if (CheckArgs(commandName, CommandStats.command_MOVE_MV_argsCount, argsCount))
                        OnCommand.OnCommand_COPY_CP(commandArgs, argsCount);
                    break;
                case "pwd":
                    if (CheckArgs(commandName, CommandStats.command_PWD_argsCount, argsCount))
                        OnCommand.OnCommand_PWD();
                    break;
                case "echo":
                    if (CheckArgs(commandName, CommandStats.command_ECHO_argsCount, argsCount))
                        OnCommand.OnCommand_ECHO(commandArgs, argsCount);
                    break;
            }
            return true;
        }

        private static bool CheckArgs(string commandName, int[] commandArgsCount, int argsCount)
        {
            if (argsCount.InRange(commandArgsCount))
                return true;
            else
                ConsoleHelper.LogArgumentsCountError(commandName, commandArgsCount, argsCount);
            return false;
        }

        private static bool InRange(this int value, int[] commandArgsCount) => value >= commandArgsCount[0] && (value <= commandArgsCount[1] || commandArgsCount[1] == -1);
    }
}
