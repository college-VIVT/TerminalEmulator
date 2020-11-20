using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Terminal
{
    
    public class Terminal
    {
        Catalog users;
        Catalog admin;
        Catalog megaboss;

        public Terminal()
        {
            UserCatalogListCommand();
            AdminCatalogListCommand();
            MegabossCatalogListCommand();
        }
        private void UserCatalogListCommand()
        {
            users = new Catalog("User");
            users.AddCommand("admin", () => { admin.PerformingOperation(); });
        }

        private void AdminCatalogListCommand()
        {
            admin = new Catalog("Admin", users.path);
            admin.AddCommand("color", () => { Console.ForegroundColor = ConsoleColor.Green; });
            admin.AddCommand("megaboss", () => { megaboss.PerformingOperation(); });
            admin.AddCommand("newfile", () => { Erorr(NewFile); });
            admin.AddCommand("copyfile", () => { Erorr(CopyFile); });
            admin.AddCommand("deletefile", () => { Erorr(DeleteFile); });
            admin.AddCommand("openfile", () => { Erorr(OpenFile); });
            
        }
        public void OpenFile()
        {
            string path = InputPath("Введите путь:");
            using (FileStream fstream = File.OpenRead(@$"{path}"))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                Console.WriteLine($"Текст из файла: {textFromFile}");
            }
        }
        public string InputPath(string m)
        {
            Console.Write(m);
            return Console.ReadLine();
        }
        public void DeleteFile()
        {

            string path = InputPath("Введите путь:");
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
                fileInf.Delete();
        }
        public void CopyFile()
        {
            string path = InputPath("Введите путь:");
            string newPath= InputPath("Введите путь для копии:");
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.MoveTo(newPath);
            }
        }
        public void NewFile()
        {
            string path = InputPath("Введите путь:");
            FileStream fstream = new FileStream(@$"{path}\newfile.txt", FileMode.Create);
        }
        public void Erorr(Action act)
        {
            try
            {
                act();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private void MegabossCatalogListCommand()
        {
            megaboss = new Catalog("Megaboss", admin.path);
            megaboss.AddCommand("godmode", () => { Console.ForegroundColor = ConsoleColor.Red; });
        }

        public void TerminalEntrance()
        {
            users.PerformingOperation();
        }
    }
}
