using System;
using System.IO;
using static System.Console;


namespace xcmd
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("X CMD [Version 1.0]");
            WriteLine("(c) PRXPHET, 2020.");
            string cmd = " ", path = @"C:\";

            while (cmd != "exit" || cmd != "Exit")
            {
                WriteLine();
                Write(path + ">");
                cmd = Console.ReadLine();
                if (cmd == "dir")
                {
                    WriteLine("Содержимое папки " + path);
                    string[] dirs = Directory.GetDirectories(path);
                    foreach (string s in dirs)
                    {
                        WriteLine("<DIR>" + "     " + s);
                    }
                    string[] files = Directory.GetFiles(path);
                    foreach (string s in files)
                    {
                        WriteLine("          " + s);
                    }
                }
                if (cmd == "cd" || cmd == "cd ")
                {
                    Write("Укажите путь: ");
                    string way;
                    way = ReadLine();
                    if (Directory.Exists(way))
                    {
                        path = way;
                    }
                    else
                    {
                        WriteLine("Не удается найти указанный путь.");
                    }
                }
                if (cmd == "cls")
                {
                    Clear();
                }
                if (cmd == "exit")
                {
                    Environment.Exit(0);
                }
                if (cmd == "new")
                {
                    Write("укажите имя: ");
                    string fileName;
                    fileName = ReadLine();
                    FileInfo fi = new FileInfo(fileName);
                    if (!fi.Exists)
                    {
                        var fileInf1 = File.Create(@$"{path}\{fileName}");
                        WriteLine("Файл создан.");
                        fileInf1.Close();
                    }
                    else
                    {
                        WriteLine("Данный файл уже существует.");
                    }
                }
                if (cmd == "erase")
                {
                    Write("Введите путь к файлу: ");
                    string timepath;
                    timepath = ReadLine();
                    FileInfo fi = new FileInfo(timepath);
                    if (fi.Exists)
                    {
                        File.Delete(@$"{timepath}");
                    }
                    else
                    {
                        WriteLine("Файл не найден.");
                    }
                }
                if (cmd == "copy")
                {
                    Write("Введите путь к файлу: ");
                    string newpath, oldpath;
                    oldpath = ReadLine();
                    FileInfo fi = new FileInfo(oldpath);
                    if (fi.Exists)
                    {
                        Write("Введите место перемещения: ");
                        newpath = ReadLine();
                        File.Copy(oldpath, newpath, true);
                    }
                    else
                    {
                        WriteLine("Файл не существует.");
                    }
                }
                if (cmd == "open")
                {
                    Write("Укажите путь до файла: ");
                    string filepath = ReadLine();
                    FileInfo fi = new FileInfo(filepath);
                    if (fi.Exists)
                    {
                        FileStream file1 = new FileStream(@$"{filepath}", FileMode.Open);
                        StreamReader reader = new StreamReader(file1);
                        WriteLine(reader.ReadToEnd());
                        reader.Close();
                    }
                }
                if (cmd == "help")
                {
                    WriteLine("CD          Смена текущей директории.");
                    WriteLine("CLS          Очистка экрана.");
                    WriteLine("COPY          Копирование файла в другое место.");
                    WriteLine();
                    WriteLine("DIR          Просмотр содержимого директории.");
                    WriteLine();
                    WriteLine("ERASE          Удаление файла.");
                    WriteLine("EXIT          Завершает работу программы.");
                    WriteLine();
                    WriteLine("HELP          Выводит справочную информацию о командах.");
                    WriteLine();
                    WriteLine("NEW          Создание файла.");
                    WriteLine();
                    WriteLine("OPEN          Прочтение содержимого  файла.");
                }
            }
        }
    }

}
