using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TerminalEmulator
{

    class Terminal
    {
        private string name;
        private string password;
        private bool flagPassword;
        private Action CheckingErrorsMethod;
        public Terminal()
        {
            name = "Terminal";
            flagPassword = false;
        }
        public void CustomMode()
        {
            while (flagPassword)
            {
                if (password == CommandEntry(".password:"))
                    break;
            }
            bool exit = false;
            while (exit != true)
            {
                switch (CommandEntry(">"))
                {
                    case "enable":
                        PrivilegedMode();
                        break;
                    case "path":
                        Console.WriteLine($"{name}/CustomMode");
                        break;
                    case "newfile":
                        CheckingErrorsMethod = NewFile;
                        CheckingErrors(CheckingErrorsMethod);
                        break;
                    case "copyfile":
                        CheckingErrorsMethod = CopyFileTo;
                        CheckingErrors(CheckingErrorsMethod);
                        break;
                    case "deletefile":
                        CheckingErrorsMethod = DeleteFile;
                        CheckingErrors(CheckingErrorsMethod);
                        break;
                    case "help":
                        CustomModeHelp();
                        break;
                    case "exit":
                        exit = true;
                        break;
                    default:
                        break;
                }
            }
        }

        //Ввод команды
        private string CommandEntry(string label)
        {
            Console.Write($"{name}{label}");
            return Console.ReadLine();
        }
        private void CheckingErrors(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private void CustomModeHelp()
        {
            Console.WriteLine("enable - PrivilegedMode");
            Console.WriteLine("path - Вывод полного пути, где сейчас находитесь в структуре каталогов");
            Console.WriteLine("newfile - Создание нового файла");
            Console.WriteLine("copyfile - Копирование файла");
            Console.WriteLine("deletefile - Удаление файла");
            Console.WriteLine("help - Все команды");
            Console.WriteLine("exit - Выход из терминала");
        }
        private void NewFile()
        {
            string filename = CommandEntry(">Введите путь для нового файла:");
            using FileStream file = new FileStream(filename, FileMode.CreateNew);
        }
        private void CopyFileTo()
        {
            string path = CommandEntry(">Введите путь файла для копирования:");
            string newPath = CommandEntry(">Введите путь куда копировать файл:");
            FileInfo file = new FileInfo(path);
            if (file.Exists)
                file.CopyTo(newPath, true);
        }

        private void DeleteFile()
        {
            string path = CommandEntry(">Введите путь удаления файла:");
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
                fileInf.Delete();
        }

        //Привилегированный режим
        private void PrivilegedMode()
        {
            bool exit = false;
            while (exit != true)
            {
                switch (CommandEntry("#"))
                {
                    case "hostname":
                        TerminalNameEditor();
                        break;
                    case "password":
                        TerminalPasswordEditor();
                        break;
                    case "login":
                        TerminalFlagPassword();
                        break;
                    case "path":
                        Console.WriteLine($"{name}/CustomMode/PrivilegedMode");
                        break;
                    case "help":
                        PrivilegedModeHelp();
                        break;
                    case "disable":
                        exit = true;
                        break;
                    default:
                        break;
                }
            }
        }
        private void PrivilegedModeHelp()
        {
            Console.WriteLine("hostname - Новое имя для терминала");
            Console.WriteLine("password - Создание пароля для входа в пользовательский режим терминала");
            Console.WriteLine("login - Активация пароля для входа в пользовательский режим терминала");
            Console.WriteLine("path - Вывод полного пути, где сейчас находитесь в структуре каталогов");
            Console.WriteLine("help - Все команды");
            Console.WriteLine("disable - выход из PrivilegedMode");
        }
        //Редактирование имени терминала
        private void TerminalNameEditor()
        {
            name = CommandEntry("#Enter new name:");
        }
        //Редактирование пароля терминала при входе
        private void TerminalPasswordEditor()
        {
            string pas = CommandEntry("#Enter new password:");
            if (pas.Length > 0)
                password = pas;
            else
            {
                password = null;
                flagPassword = false;
            }

        }
        //Включение/выключение ввода пароля
        private void TerminalFlagPassword()
        {
            if (password != null)
            {
                if (password.Length > 0)
                {
                    if (flagPassword == false)
                    {
                        Console.WriteLine($"{name}#login ON");
                        flagPassword = true;

                    }
                    else
                    {
                        Console.WriteLine($"{name}#login OFF");
                        flagPassword = false;
                    }
                }
            }

        }
    }
}
