using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalEmulator
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

            //touch testFileInCDisk.txt //создаст файл в текущем каталоге
            //del testFileInCDisk.txt //удалит файл в текущем каталоге

            //touch ~/../../testFileInCDisk.txt //недостаточно прав
            //del ~/../../testFileInCDisk.txt //недостаточно прав

            //touch ~/../../testFolder/testFileInCDisk.txt
            //del ~/../../testFolder/testFileInCDisk.txt

            //cat "~\Desktop\123.txt"

            //mkdir "~\Desktop\Новая папка (3000)"
            //rmdir "~\Desktop\Новая папка (3000)" -f
            while (true)
            {
                Console.Write(VStore.CurrentPath + "> ");
                var commandLine = Console.ReadLine();
                CommandSeparator.OnNewCommand(commandLine);
            }
        }
    }
}
