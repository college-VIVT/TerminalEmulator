using TerminalEmulator.Сommands;
using System;
using System.IO;

namespace TerminalEmulator
{
    public class Program
    {
        static void Main()
        {
            /* Указываем начальную директорию */
            CommandHandler.ToDirectory(Environment.CurrentDirectory);

            /* Регистрируем комманды */
            CommandHandler.Register(new ExitCommand()); // exit
            CommandHandler.Register(new PrintWorkingDirectoryCommand()); // pwd
            CommandHandler.Register(new ChangeDirectoryCommand()); // cd <PATH>
            CommandHandler.Register(new ListCommand()); // ls

            CommandHandler.Register(new TouchCommand()); // touch <FILE_NAME>
            CommandHandler.Register(new CopyCommand()); // cp <FILE_SRC> <FILE_DST>
            CommandHandler.Register(new RemoveCommand()); // rm <FILE_NAME>
            CommandHandler.Register(new CatenateCommand()); // cat <FILE_NAME>


            while (true)
            {
                Console.Write($"{CommandHandler.Path}> ");
                string command = Console.ReadLine();
                Console.WriteLine(CommandHandler.Execute(command));
            }
        }
    }
}
