using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using static System.Console;

namespace TerminalEmulator
{
    class Program
    {
        static void Dir(DirectoryInfo directory)
        {
            try
            {
                var files = directory.GetFileSystemInfos();
                foreach(var file in files)
                {
                    WriteLine(file.CreationTime + "       " + file.Name);
                }
            }
            catch (Exception e)
            {
                WriteLine("Ошибка: "+e.ToString());
            }
        }

        static void Madedir(string name)
        {
            DirectoryInfo directory = new DirectoryInfo(name);
            try
            {
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
            catch (Exception e)
            {
                WriteLine("Ошибка: " + e.ToString());
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                DirectoryInfo directory = new DirectoryInfo(Directory.GetCurrentDirectory());
                Write(directory.Name+">"); string msg = ReadLine();
                string[] command = new string[2];
                if (msg != "help" && msg != "cls" && msg != "dir" && msg != "exit")
                {
                    string[] buf = msg.Split(' ');
                    for (int i = 0; i < 2; i++)
                    {
                        command[i] = buf[i];
                    }
                }
                else command[0] = msg;
                switch (command[0].ToUpper())
                {
                    case "DIR":
                        Dir(directory); WriteLine();
                        break;

                    case "MOVETO":

                        break;

                    case "MADEDIR":
                        Madedir(command[1]); WriteLine();
                        break;

                    case "COPY":

                        break;

                    case "DEL":

                        break;

                    case "READ":

                        break;

                    case "CLS":
                        Clear();
                        break;

                    case "HELP":
                        WriteLine("Для получения сведений об определенной команде наберите HELP <имя команды>");
                        WriteLine("CLS                  Очистка экрана");
                        WriteLine("COPY                 Копирование текстового файла в указанный каталог\n");
                        WriteLine("DEL                  Удаление текстового файла");
                        WriteLine("DIR                  Выводит список файлов и подкаталогов в текущем каталоге\n");
                        WriteLine("EXIT                 Завершает программу\n");
                        WriteLine("HELP                 Выводит справочную информацию о командах\n");
                        WriteLine("MADEDIR              Создание текстового файла");
                        WriteLine("MOVETO               Переход в указанный каталог\n");
                        WriteLine("READ                 Выводит содержимое текстового файла\n");
                        break;

                    case "EXIT":
                        return;
                    default:
                        WriteLine($"{msg} не является командой");
                        break;
                }
            }
        }
    }
}
