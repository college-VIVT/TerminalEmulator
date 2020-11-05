using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIemulator
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandStats.BuildHelp();
            //help
            //pwd
            //ls "../../Program Files"
            //mv "C:\Users\userName\Desktop\Новая папка (2)\1.txt" "C:\Users\userName\Desktop\Новая папка (2)\2.txt" -f
            //cp "C:\Users\userName\Desktop\Новая папка (2)\1.txt" "C:\Users\userName\Desktop\Новая папка (2)\2.txt" -f
            //cd C:\Users\userName\Downloads
            while (true)
            {
                Console.Write(VStore.CurrentPath + "> ");
                var commandLine = Console.ReadLine();
                CommandSeparator.OnNewCommand(commandLine);
            }
        }
    }
}
