using System;
using System.IO;
using static System.Console;

namespace TerminalEmulator
{
    class Program
    {
        static void Dir(DirectoryInfo directory)
        {
            var files = directory.GetFileSystemInfos();
                foreach(var file in files)
                {
                    WriteLine(file.CreationTime + "       " + file.Name);
                }
        }

        static void MadeDir(string name)
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

        static void DeleteDir(string name)
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

        static void MadeFile(string name)
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

        static void DeleteFile(string name)
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
                    case "DIR":
                        if (command.Length == 1) 
                        { Dir(directory); WriteLine(); } else goto default;
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

                    case "MADEDIR":
                        if (command.Length==2) 
                        { MadeDir(command[1]); WriteLine(); } 
                        else goto default;
                        break;

                    case "MADEFILE":
                        if (command.Length==2) 
                        { MadeFile(command[1]); WriteLine(); } 
                        else goto default;
                        break;

                    case "COPY":
                        if (command.Length == 3) 
                        { CopyFile(command[1], command[2]); WriteLine(); }
                        else goto default;
                        break;

                    case "DELDIR":
                        if (command.Length == 2) 
                        { DeleteDir(command[1]); WriteLine(); }
                        else goto default;
                        break;

                    case "DELFILE":
                        if (command.Length == 2) 
                        { DeleteFile(command[1]); WriteLine(); }
                        else goto default;
                        break;

                    case "READ":
                        if (command.Length == 2)
                        { Read(command[1]); WriteLine(); }
                        else goto default;
                        break;

                    case "CLS":
                        if (command.Length == 1) 
                        Clear(); 
                        else goto default;
                        break;

                    case "HELP":
                        if (command.Length == 2)
                        {
                            switch (command[1].ToUpper())
                            {
                                case "DIR":
                                    WriteLine("Выводит список файлов и подкаталогов в текущем каталоге\nDIR\n");
                                    break;
                                case "MADEDIR":
                                    WriteLine("Создаёт подкаталог в текущем каталоге\nMADEDIR [<название директории>]\n<название директории> - имя директории.\n");
                                    break;
                                case "DELDIR":
                                    WriteLine("Удаляет подкаталог в текущем каталоге\nDELDIR [<название директории>]\n<название директории> - имя директории.\n");
                                    break;
                                case "MADEFILE":
                                    WriteLine("Создаёт файл в текущем каталоге\nMADEFILE [<название файла>]\n<название файла> - имя файла.\n");
                                    break;
                                case "DELFILE":
                                    WriteLine("Удаляет файл в текущем каталоге\nDELFILE [<название файла>]\n<название файла> - имя файла.\n");
                                    break;
                                case "CLS":
                                    WriteLine("Очищает экран консоли\nCLS\n");
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
                                    WriteLine("Читает файл, если он существует\nREAD [<название файла>]\n<название файла> - файл, который нужно прочитать");
                                    break;
                                default:
                                    WriteLine($"{msg} не является командой");
                                    break;
                            }
                        }
                        else if (command.Length == 1)
                        {
                            WriteLine("Для получения сведений об определенной команде наберите HELP <имя команды>");
                            WriteLine("CD                   Переход в родительский каталог");
                            WriteLine("CLS                  Очистка экрана");
                            WriteLine("COPY                 Копирование текстового файла в указанный каталог\n");
                            WriteLine("DELDIR               Удаление каталога");
                            WriteLine("DELFILE              Удаление текстового файла");
                            WriteLine("DIR                  Вывод списка файлов и подкаталогов в текущем каталоге\n");
                            WriteLine("EXIT                 Завершение программы\n");
                            WriteLine("HELP                 Вывод справочной информации о командах\n");
                            WriteLine("MADEDIR              Создание подкаталога");
                            WriteLine("MADEFILE             Создание текстового файла");
                            WriteLine("MOVETO               Переход в указанный подкаталог\n");
                            WriteLine("READ                 Вывод содержимого текстового файла\n");
                            
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
