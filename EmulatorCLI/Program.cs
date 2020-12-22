using System;
using System.IO;
using static System.Console;

namespace EmulatorCLI
{
    class Program
    {
        static void Ls(DirectoryInfo directory)
        {
            var files = directory.GetFileSystemInfos();
                foreach(var file in files)
                {
                    WriteLine(file.CreationTime + "       " + file.Name);
                }
        }

        static void MakeDir(string name)
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(name);
                if (directory.Exists)
                {
                    WriteLine("Этот каталог уже существует");
                    return;
                }
                else
                {
                    directory.Create();
                    WriteLine("Каталог создан");
                }
            }
            catch(Exception e)
            {
                WriteLine("ОШИБКА!" + e.Message);
            }
        }

        static void RemoveDir(string name)
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(name); directory.Delete(true);
                WriteLine($"Каталог {name} удалён\n");
            }
            catch(Exception e)
            {
                WriteLine("ОШИБКА!!!" + e.Message);
            }
        }

        static void MakeFile(string name)
        {
                try
                {
                if (!File.Exists(name))
                {
                    using (StreamWriter writer = File.CreateText(name))
                    {
                        writer.WriteLine($"Это файл {name}");
                    }
                    WriteLine("Файл создан");
                }
                else
                {
                    WriteLine("Этот файл уже существует. Хотите перезаписать его?\nНажмите Enter для перезаписи или другую кнопку для отмены.");
                    if (ReadKey().Key == ConsoleKey.Enter)
                    {
                        using (StreamWriter writer = File.CreateText(name))
                        {
                            writer.WriteLine($"Это новый файл {name}");
                        }
                        WriteLine("Файл создан");
                    }
                }
                }
                catch (Exception e)
                {
                    WriteLine("ОШИБКА!" + e.Message);
                }
        }

        static void RemoveFile(string name)
        {
            try
            {
                if (File.Exists(name))
                { File.Delete(name); WriteLine($"Файл {name} удалён\n"); }
                else WriteLine($"Файл {name} необнаружен в данной директории\n");
            }
            catch (Exception e)
            {
                WriteLine("ОШИБКА!!!" + e.Message);
            }
        }

        static void Read(string name)
        {
            try
            {
                if (File.Exists(name))
                {
                    WriteLine($"\nфайл {name} открыт\n");
                    using (var file = new StreamReader(name))
                    {
                        while (!file.EndOfStream)
                            WriteLine(file.ReadLine());
                    }
                    WriteLine();
                }
                else WriteLine($"Файл {name} не найден\n");
            }
            catch(Exception e)
            {
                WriteLine("ОШИБКА!!!" + e.Message);
            }
        }

        static void CopyFile(string name1, string name2)
        {
            try
            {
                if (Directory.Exists(name2))
                {
                    FileInfo f1 = new FileInfo(name1);
                    Directory.SetCurrentDirectory(name2);
                    FileInfo f2 = new FileInfo(name1);
                    File.Copy(f1.FullName, f2.FullName, true);
                    WriteLine("Файл скопирован\n");
                }
            }
            catch (Exception e)
            {
                WriteLine("ОШИБКА!!!" + e.Message);
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                DirectoryInfo directory = new DirectoryInfo(Directory.GetCurrentDirectory());
                Write(directory.Name+">"); string msg = ReadLine();
                string[] command = msg.Split(' ');
                switch (command[0].ToUpper())
                {
                    case "LS":
                        if (command.Length == 1) 
                        { Ls(directory); WriteLine(); } else goto default;
                        break;

                    case "MOVETO":
                        if (command.Length == 2) 
                        {
                            try
                            {
                                Directory.SetCurrentDirectory(command[1]);
                            }
                            catch (Exception e)
                            {
                                WriteLine("ОШИБКА!!!" + e.Message);
                            }
                            WriteLine();
                        } else goto default;
                        break;

                    case "CD":
                        if (command.Length == 1) 
                        {
                            try
                            {
                                Directory.SetCurrentDirectory(Convert.ToString(directory.Parent));
                            }
                            catch (Exception e)
                            {
                                WriteLine("ОШИБКА!!!" + e.Message);
                            }
                        }else goto default;
                        break;

                    case "MAKEDIR":
                        if (command.Length==2) 
                        { MakeDir(command[1]); WriteLine(); } 
                        else goto default;
                        break;

                    case "MAKEFILE":
                        if (command.Length==2) 
                        { MakeFile(command[1]); WriteLine(); } 
                        else goto default;
                        break;

                    case "COPY":
                        if (command.Length == 3) 
                        { CopyFile(command[1], command[2]); WriteLine(); }
                        else goto default;
                        break;

                    case "REMOVEDIR":
                        if (command.Length == 2) 
                        { RemoveDir(command[1]); WriteLine(); }
                        else goto default;
                        break;

                    case "REMOVEFILE":
                        if (command.Length == 2) 
                        { RemoveFile(command[1]); WriteLine(); }
                        else goto default;
                        break;

                    case "READ":
                        if (command.Length == 2)
                        { Read(command[1]); WriteLine(); }
                        else goto default;
                        break;

                    case "CLEAR":
                        if (command.Length == 1) 
                        Clear(); 
                        else goto default;
                        break;

                    case "HELP":
                        if (command.Length == 2)
                        {
                            switch (command[1].ToUpper())
                            {
                                case "LS":
                                    WriteLine("Выводит список файлов и подкаталогов в текущем каталоге\nLS\n");
                                    break;
                                case "MAKEDIR":
                                    WriteLine("Создаёт подкаталог в текущем каталоге\nMAKEDIR [<название директории>]\n<название директории> - имя директории.\n");
                                    break;
                                case "REMOVEDIR":
                                    WriteLine("Удаляет подкаталог в текущем каталоге\nREMOVEDIR [<название директории>]\n<название директории> - имя директории.\n");
                                    break;
                                case "MAKEFILE":
                                    WriteLine("Создаёт файл в текущем каталоге\nMAKEFILE [<название файла>]\n<название файла> - имя файла.\n");
                                    break;
                                case "REMOVEFILE":
                                    WriteLine("Удаляет файл в текущем каталоге\nREMOVEFILE [<название файла>]\n<название файла> - имя файла.\n");
                                    break;
                                case "CLEAR":
                                    WriteLine("Очищает экран консоли\nCLEAR\n");
                                    break;
                                case "HELP":
                                    WriteLine("Выводит справочную информацию о командах\nHELP [<команда>]\n<команда> - команда, интересующая пользователя.\n");
                                    break;
                                case "EXIT":
                                    WriteLine("Завершает программу\nEXIT\n");
                                    break;
                                case "COPY":
                                    WriteLine("Копирует текстовый файл в указанный каталог\nCOPY [<название файла источника> <название, куда нужно скопировать>]\n<название файла источника> - файл, который копируем,\n<название, куда нужно скопировать> - файл, куда копируем.\n");
                                    break;
                                case "CD":
                                    WriteLine("Переходит в родительский каталог\nCD\n");
                                    break;
                                case "MOVETO":
                                    WriteLine("Переходит на каталог ниже, если он есть\nMOVETO [<название подкаталога>]\n<название подкаталога> - имя подкаталога, куда нужно перейти.\n");
                                    break;
                                case "READ":
                                    WriteLine("Комманда читает файл, если он существует\nREAD [<название файла>]\n<название файла> - файл, который нужно прочитать");
                                    break;
                                default:
                                    WriteLine($"{msg} не является командой");
                                    break;
                            }
                        }
                        else if (command.Length == 1)
                        {
                            WriteLine("Для получения сведений о нужной команде наберите HELP <имя команды>");
                            WriteLine("CD                   Переходит в родительский каталог");
                            WriteLine("CLEAR                  Очищает экран");
                            WriteLine("COPY                 Копирует текстовые файлы в указанный каталог\n");
                            WriteLine("REMOVEDIR               Удаляет каталог");
                            WriteLine("REMOVEFILE              Удаляет текстовый файл");
                            WriteLine("LS                  Выводит список файлов и подкаталогов в текущем каталоге\n");
                            WriteLine("EXIT                 Завершает программу\n");
                            WriteLine("HELP                 Вывод справочной информации о командах\n");
                            WriteLine("MAKEDIR              Создает подкаталог");
                            WriteLine("MAKEFILE             Создает текстовый файлы");
                            WriteLine("MOVETO               Переходит в указанный подкаталог\n");
                            WriteLine("READ                 Выводит содержимое текстового файла\n");
                            
                        }
                        else goto default;
                        break;

                    case "EXIT":
                        if (command.Length == 1) 
                        return;
                        else goto default;

                    default:
                        WriteLine($"{msg} не является командой");
                        break;
                }
            }
        }
    }
}