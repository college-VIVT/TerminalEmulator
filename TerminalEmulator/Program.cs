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

        static void Madedir(string name)
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

        static void Madefile(string name)
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
                        if (command.Length > 1) goto default;
                        Dir(directory); WriteLine();
                        break;

                    case "MOVETO":

                        break;

                    case "MADEDIR":
                        if (command.Length==1) goto default;
                        Madedir(command[1]);
                        break;

                    case "MADEFILE":
                        if (command.Length==1) goto default;
                        Madefile(command[1]);
                        break;

                    case "COPY":

                        break;

                    case "DEL":

                        break;

                    case "READ":

                        break;

                    case "CLS":
                        if (command.Length > 1) goto default;
                        Clear();
                        break;

                    case "HELP":
                        if (command.Length > 1) goto default;
                        WriteLine("Для получения сведений об определенной команде наберите HELP <имя команды>");
                        WriteLine("CLS                  Очистка экрана");
                        WriteLine("COPY                 Копирование текстового файла в указанный каталог\n");
                        WriteLine("DEL                  Удаление текстового файла");
                        WriteLine("DIR                  Выводит список файлов и подкаталогов в текущем каталоге\n");
                        WriteLine("EXIT                 Завершает программу\n");
                        WriteLine("HELP                 Выводит справочную информацию о командах\n");
                        WriteLine("MADEDIR              Создание подкаталога");
                        WriteLine("MADEFILE             Создание текстового файла");
                        WriteLine("MOVETO               Переход в указанный каталог\n");
                        WriteLine("READ                 Выводит содержимое текстового файла\n");
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
