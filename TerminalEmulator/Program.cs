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
                    else WriteLine("Этот файл уже существует. Хотите перезаписать его?\nНажмите Enter для перезаписи или другую кнопку для отмены.");
                    if (ReadKey().Key == ConsoleKey.Enter)
                    {
                        using (StreamWriter writer = File.CreateText(name))
                        {
                            writer.WriteLine($"Это новый файл {name}");
                        }
                        WriteLine("Файл создан");
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

        static void Main(string[] args)
        {
            while (true)
            {
                begin:
                DirectoryInfo directory = new DirectoryInfo(Directory.GetCurrentDirectory());
                Write(directory.Name+">"); string msg = ReadLine();
                string[] command = msg.Split(' ');
                if (command.Length > 2)
                {
                    WriteLine($"{msg} не является командой"); goto begin;
                }
                switch (command[0].ToUpper())
                {
                    case "DIR":
                        if (command.Length > 1) goto default;
                        Dir(directory); WriteLine();
                        break;

                    case "MOVETO":

                        break;

                    case "MADEDIR":
                        if (command.Length==1) goto default;
                        MadeDir(command[1]);
                        break;

                    case "MADEFILE":
                        if (command.Length==1) goto default;
                        MadeFile(command[1]);
                        break;

                    case "COPY":

                        break;

                    case "DELDIR":
                        if (command.Length == 1) goto default;
                        DeleteDir(command[1]);
                        break;

                    case "DELFILE":
                        if (command.Length == 1) goto default;
                        DeleteFile(command[1]);
                        break;

                    case "READ":

                        break;

                    case "CLS":
                        if (command.Length > 1) goto default;
                        Clear();
                        break;

                    case "HELP":
                        //if (command.Length > 1) goto default;
                        if (command.Length > 1)
                        {
                            switch(command[1].ToUpper())
                            {
                                case "DIR":
                                    WriteLine("Выводит список файлов и подкаталогов в текущем каталоге\nDIR\n");
                                    break;
                                case "MADEDIR":
                                    WriteLine("Создаёт подкаталог в текущем каталоге\nMADEDIR [<название директории>]\n<название директории> - имя директории.\n");
                                    break;
                                case "MADEFILE":
                                    WriteLine("Создаёт файл в текущем каталоге\nMADEFILE [<название файла>]\n<название файла> - имя файла.\n");
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
                                default:
                                    WriteLine($"{msg} не является командой");
                                    break;
                            }
                        }
                        else
                        {
                            WriteLine("Для получения сведений об определенной команде наберите HELP <имя команды>");
                            WriteLine("CLS                  Очистка экрана");
                            WriteLine("COPY                 Копирование текстового файла в указанный каталог\n");
                            WriteLine("DELDIR               Удаление каталога");
                            WriteLine("DELFILE              Удаление текстового файла");
                            WriteLine("DIR                  Вывод списка файлов и подкаталогов в текущем каталоге\n");
                            WriteLine("EXIT                 Завершение программы\n");
                            WriteLine("HELP                 Вывод справочной информации о командах\n");
                            WriteLine("MADEDIR              Создание подкаталога");
                            WriteLine("MADEFILE             Создание текстового файла");
                            WriteLine("MOVETO               Переход в указанный каталог\n");
                            WriteLine("READ                 Вывод содержимого текстового файла\n");
                        }
                        break;

                    case "EXIT":
                        if (command.Length > 1) goto default;
                        return;

                    default:
                        WriteLine($"{msg} не является командой");
                        break;
                }
            }
        }
    }
}
