using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIemulator
{
    public static class CommandStats
    {
        public static int[] command_HELP_argsCount = new int[] { 0, 0 };
        public static string command_HELP_signature = "<help/?>";
        public static string command_HELP_help = "выводит это сообщение";

        public static int[] command_LS_DIR_argsCount = new int[] { 0, 1 };
        public static string command_LS_DIR_signature = "<ls/dir> [path]";
        public static string command_LS_DIR_help = "выводит содержание указанной или текущей папки";

        public static int[] command_MOVE_MV_argsCount = new int[] { 2, 3 };
        public static string command_MOVE_MV_signature = "<move/mv> pathToFile1 pathToFile2 [-f]";
        public static string command_MOVE_MV_help = "перемещает файл из pathToFile1 в pathToFile2";

        public static int[] command_COPY_CP_argsCount = new int[] { 2, 3 };
        public static string command_COPY_CP_signature = "<copy/cp> pathToFile1 pathToFile2 [-f]";
        public static string command_COPY_CP_help = "копирует файл pathToFile1 в pathToFile2";

        public static int[] command_PWD_argsCount = new int[] { 0, 0 };
        public static string command_PWD_signature = "pwd";
        public static string command_PWD_help = "Выводит текущий рабочий каталог";

        public static int[] command_CD_argsCount = new int[] { 1, 1 };
        public static string command_CD_signature = "cd path";
        public static string command_CD_help = "Переход в каталог по пути path, может принимать относительный путь (.. или ~)";

        public static int[] command_ECHO_argsCount = new int[] { 0, -1 };
        public static string command_ECHO_signature = "echo text1 text2 ... textN";
        public static string command_ECHO_help = "выводит переданный текст или пустую строку";

        public static string[] HelpList { get; private set; }

        public static void BuildHelp()
        {
            var helpList = new List<string>
            {
                $"{command_HELP_signature} - {command_HELP_help}",
                $"{command_LS_DIR_signature} - {command_LS_DIR_help}",
                $"{command_ECHO_signature} - {command_ECHO_help}",
                $"{command_MOVE_MV_signature} - {command_MOVE_MV_help}",
                $"{command_COPY_CP_signature} - {command_COPY_CP_help}",
                $"{command_PWD_signature} - {command_PWD_help}",
                $"{command_CD_signature} - {command_CD_help}"
            };
            HelpList = helpList.ToArray();
        }
    }
}
