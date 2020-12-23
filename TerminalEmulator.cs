using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TerminalEmulator
{
    public class TerminalEmulator
    {
        private Catalog users;
        private Catalog enable;
        private Catalog Reflection;

        public TerminalEmulator()
        {
            DirectoryInitializer();
        }


        private void DirectoryInitializer()
        {
            UserListCommand();
            EnableListCommand();
            ConfigListCommand();
        }
        private void UserListCommand()
        {
            users = new Catalog("User");
            users.AddCommand("B", () => Console.WriteLine("Back)"));
            users.AddCommand("Enable", () => enable.Operation());
        }
        private void EnableListCommand()
        {
            enable = new Catalog("Enable", users.path);
            enable.AddCommand("newfile", () => NewFile());
            enable.AddCommand("copyfile", () => CopyFile());
            enable.AddCommand("deletefile", () => DeleteFile());
            enable.AddCommand("Reflection", () => Reflection.Operation());
        }
        private void NewFile()
        {
            Console.Write("Введите путь для создания файла");
            string path = Console.ReadLine();
            FileInfo file = new FileInfo(path);
        }
        private void CopyFile()
        {
            Console.Write("Введите путь файла:");
            string path = Console.ReadLine();
            Console.Write("Введите путь для копии:");
            string newPath = Console.ReadLine();
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
                fileInf.MoveTo(newPath);
        }
        private void DeleteFile()
        {
            Console.Write("Введите путь файла:");
            string path = Console.ReadLine();
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
                fileInf.Delete();
        }
        private void ReflectionListCommand()
        {
            Reflection = new Catalog("Reflection", enable.path);
            Reflection.AddCommand("ColorWhite", () => Console.ForegroundColor = ConsoleColor.White);
        }
        public void RunningTerminal() { users.Operation(); }


    }

}
