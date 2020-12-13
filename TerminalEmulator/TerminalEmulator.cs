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
        private Catalog config;

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
            users.AddCommand("Hi", () => Console.WriteLine("Hello)"));
            users.AddCommand("Enable", () => enable.Operation());
        }
        private void EnableListCommand()
        {
            enable = new Catalog("Enable", users.path);
            enable.AddCommand("newfile", () => NewFile());
            enable.AddCommand("copyfile", () => CopyFile());
            enable.AddCommand("deletefile", () => DeleteFile());
            enable.AddCommand("config", () => config.Operation());
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
        private void ConfigListCommand()
        {
            config = new Catalog("Configurate terminal", enable.path);
            config.AddCommand("ColorRed", () => Console.ForegroundColor = ConsoleColor.Red);
        }
        public void RunningTerminal() { users.Operation(); }


    }

}
