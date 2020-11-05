using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalEmulator
{
    public static class CommandStats
    {
        public static int[] command_Help_argsCount = new int[] { 0, 0 };
        public static string command_Help_signature = "<help/?>";
        public static string command_Help_help = "выводит это сообщение";

        public static int[] command_Ls_Dir_argsCount = new int[] { 0, 1 };
        public static string command_Ls_Dir_signature = "<ls/dir> [path]";
        public static string command_Ls_Dir_help = "выводит содержание указанной или текущей папки";

        public static int[] command_Move_Mv_argsCount = new int[] { 2, 3 };
        public static string command_Move_Mv_signature = "<move/mv> pathToFile1 pathToFile2 [-f]";
        public static string command_Move_Mv_help = "перемещает файл из pathToFile1 в pathToFile2";

        public static int[] command_Copy_Cp_argsCount = new int[] { 2, 3 };
        public static string command_Copy_Cp_signature = "<copy/cp> pathToFile1 pathToFile2 [-f]";
        public static string command_Copy_Cp_help = "копирует файл pathToFile1 в pathToFile2";

        public static int[] command_PWD_argsCount = new int[] { 0, 0 };
        public static string command_PWD_signature = "pwd";
        public static string command_PWD_help = "Выводит текущий рабочий каталог";

        public static int[] command_CD_argsCount = new int[] { 1, 1 };
        public static string command_CD_signature = "cd path";
        public static string command_CD_help = "Переход в каталог по пути path, может принимать относительный путь (.. или ~)";

        public static int[] command_Del_Delete_Rm_Rem_Remove_argsCount = new int[] { 1, 1 };
        public static string command_Del_Delete_Rm_Rem_Remove_signature = "<Del/Delete/Rm/Rem/Remove> path";
        public static string command_Del_Delete_Rm_Rem_Remove_help = "Удаляет файл по указаному пути. Может принимать относительные пути.";

        public static int[] command_Touch_CreateFile_argsCount = new int[] { 1, 1 };
        public static string command_Touch_CreateFile_signature = "<Touch/CreateFile> path";
        public static string command_Touch_CreateFile_help = "Создаёт файл по указаному пути. Может принимать относительные пути.";

        public static int[] command_Cat_Less_argsCount = new int[] { 1, 1 };
        public static string command_Cat_Less_signature = "<Cat/Less> path";
        public static string command_Cat_Less_help = "Выводит содержимое файла. Может принимать относительные пути.";

        public static int[] command_MD_MkDir_argsCount = new int[] { 1, 1 };
        public static string command_MD_MkDir_signature = "<mk/mkdir> path";
        public static string command_MD_MkDir_help = "Создаёт каталог по указанному пути. Может принимать относительные пути.";

        public static int[] command_RmDir_argsCount = new int[] { 1, 2 };
        public static string command_RmDir_signature = "rmdir path [-f]";
        public static string command_RmDir_help = "Удаляет пустой каталог по указанному пути. Может принимать относительные пути. Используйте флаг -f в конце для удаления не пустых каталогов.";

        public static int[] command_ECHO_argsCount = new int[] { 0, -1 };
        public static string command_ECHO_signature = "echo text1 text2 ... textN";
        public static string command_ECHO_help = "выводит переданный текст или пустую строку";

        public static string[] HelpList { get; private set; }

        public static void BuildHelp()
        {
            var helpList = new List<string>
            {
                $"{command_Help_signature} - {command_Help_help}",
                $"{command_Ls_Dir_signature} - {command_Ls_Dir_help}",
                $"{command_ECHO_signature} - {command_ECHO_help}",
                $"{command_Move_Mv_signature} - {command_Move_Mv_help}",
                $"{command_Copy_Cp_signature} - {command_Copy_Cp_help}",
                $"{command_PWD_signature} - {command_PWD_help}",
                $"{command_CD_signature} - {command_CD_help}",
                $"{command_Del_Delete_Rm_Rem_Remove_signature} - {command_Del_Delete_Rm_Rem_Remove_help}",
                $"{command_Touch_CreateFile_signature} - {command_Touch_CreateFile_help}",
                $"{command_Cat_Less_signature} - {command_Cat_Less_help}",
                $"{command_MD_MkDir_signature} - {command_MD_MkDir_help}",
                $"{command_RmDir_signature} - {command_RmDir_help}"
            };
            HelpList = helpList.ToArray();
        }
    }
}
