using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIemulator
{
    public static class ConsoleHelper
    {
        #region Direct
        public static void LogError(string errorMessage)
        {
            Console.WriteLine($"[Error]: {errorMessage}");
        }

        public static void LogInfo(string infoMessage)
        {
            Console.WriteLine($"[Info]: {infoMessage}");
        }
        #endregion

        public static void LogArgumentsCountError(string commandName, int[] argsCountValid, int argsCount)
        {
            LogError($"Команда \"{commandName}\" принимает от {argsCountValid[0]} до {argsCountValid[1]} аргумента(ов). Введено {argsCount} аргумент(ов).");
            LogInfo("Проверьте правильность ввода");
        }
    }
}
