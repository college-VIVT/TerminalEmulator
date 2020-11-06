using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TerminalEmulator
{
    public static class CommandSeparator
    {
        private static readonly string commandRegExPattern = @"("".*?""|.*?) ";

        public static void OnNewCommand(string commandLine)
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

                CommandDistribute(command, args);
            }
        }

        public static void CommandDistribute(string commandName, List<string> commandArgs)
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
                    if (CheckArgs(commandName, CommandStats.command_Ls_Dir_argsCount, argsCount))
                        OnCommand.OnCommand_Ls_Dir(commandArgs, argsCount);
                    break;
                case "help": case "?":
                    if (CheckArgs(commandName, CommandStats.command_Help_argsCount, argsCount))
                        OnCommand.OnCommand_Help();
                    break;
                case "move": case "mv":
                    if (CheckArgs(commandName, CommandStats.command_Move_Mv_argsCount, argsCount))
                        OnCommand.OnCommand_Move_Mv(commandArgs, argsCount);
                    break;
                case "copy": case "cp":
                    if (CheckArgs(commandName, CommandStats.command_Move_Mv_argsCount, argsCount))
                        OnCommand.OnCommand_Copy_Cp(commandArgs, argsCount);
                    break;
                case "del": case "delete": case "rm": case "rem": case "remove":
                    if (CheckArgs(commandName, CommandStats.command_Del_Delete_Rm_Rem_Remove_argsCount, argsCount))
                        OnCommand.OnCommand_Del_Delete_Rm_Rem_Remove(commandArgs, argsCount);
                    break;
                case "touch": case "createfile":
                    if (CheckArgs(commandName, CommandStats.command_Touch_CreateFile_argsCount, argsCount))
                        OnCommand.OnCommand_Touch_CreateFile(commandArgs, argsCount);
                    break;
                case "cat": case "less":
                    if (CheckArgs(commandName, CommandStats.command_Cat_Less_argsCount, argsCount))
                        OnCommand.OnCommand_Cat_Less(commandArgs, argsCount);
                    break;
                case "mkdir": case "md":
                    if (CheckArgs(commandName, CommandStats.command_MD_MkDir_argsCount, argsCount))
                        OnCommand.OnCommand_MD_MkDir(commandArgs, argsCount);
                    break;
                case "rmdir":
                    if (CheckArgs(commandName, CommandStats.command_RmDir_argsCount, argsCount))
                        OnCommand.OnCommand_RmDir(commandArgs, argsCount);
                    break;
                case "pwd":
                    if (CheckArgs(commandName, CommandStats.command_PWD_argsCount, argsCount))
                        OnCommand.OnCommand_PWD();
                    break;
                case "echo":
                    if (CheckArgs(commandName, CommandStats.command_ECHO_argsCount, argsCount))
                        OnCommand.OnCommand_Echo(commandArgs, argsCount);
                    break;
            }
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
