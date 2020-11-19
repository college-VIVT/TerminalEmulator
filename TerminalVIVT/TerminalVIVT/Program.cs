using System;
using System.ComponentModel.Design;
using System.IO;

namespace Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**************");
            Console.WriteLine("*  Terminal  *");
            Console.WriteLine("**************");
            string command = " ", path = @"C:\";
            Console.WriteLine("Введите комманду");

            while (command != "exit" || command != "Exit")
            {
                Console.Write(path + "> "); ;
                command = Console.ReadLine();
                if (command == "ls") //просмотр содержимого директории
                {
                    Console.WriteLine("Содержимое директории: " + path);
                    Console.WriteLine("Подкаталоги:");
                    string[] dirs = Directory.GetDirectories(path);
                    foreach (string s in dirs)
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Файлы:");
                    string[] files = Directory.GetFiles(path);
                    foreach (string s in files)
                    {
                        Console.WriteLine(s);
                    }
                }
                if (command == "cd" || command == "cd ") //переход по директориям
                {
                    Console.Write("Укажите путь: ");
                    string way;
                    way = Console.ReadLine();
                    if (Directory.Exists(way))
                    {
                        path = way;
                    }
                    else
                    {
                        Console.WriteLine("Системе не удается найти указанный путь.");
                    }
                }
                if (command == "clear" || command == "cls") //очистка экрана
                {
                    Console.Clear();
                }
                if (command == "exit" || command == "Exit") //выход из терминала
                {
                    Environment.Exit(0);
                }
                if (command == "touch" || command == "touch ") //создание файла
                {
                    Console.Write("укажите имя: ");
                    string timeFile;
                    timeFile = Console.ReadLine();
                    FileInfo fi = new FileInfo(timeFile);
                    if (!fi.Exists)
                    {
                        var fileInf1 = File.Create(@$"{path}\{timeFile}");
                        Console.WriteLine("Текстовый файл создан.");
                        fileInf1.Close();
                    }
                    else
                    {
                        Console.WriteLine("Данный файл уже существует.");
                    }
                }
                if (command == "rm" || command == "rm ") //удаление файла
                {
                    Console.Write("Введите путь к файлу: ");
                    string timepath;
                    timepath = Console.ReadLine();
                    FileInfo fi = new FileInfo(timepath);
                    if (fi.Exists)
                    {
                        File.Delete(@$"{timepath}");
                        Console.WriteLine("Файл удален");
                    }
                    else
                    {
                        Console.WriteLine("Файл не найден");
                    }
                }
                if (command == "cp" || command == "cp ") //копирование файла в другой каталог
                {
                    Console.Write("Введите путь к файлу: ");
                    string newpath, oldpath;
                    oldpath = Console.ReadLine();
                    FileInfo fi = new FileInfo(oldpath);
                    if (fi.Exists)
                    {
                        Console.Write("Введите место перемещения: ");
                        newpath = Console.ReadLine();
                        File.Copy(oldpath, newpath, true);
                    }
                    else
                    {
                        Console.WriteLine("Такого файла нет");
                    }
                }
                if (command == "openfl" || command == "openfl ") //чтение файла
                {
                    Console.Write("Укажите путь до файла: ");
                    string filepath = Console.ReadLine();
                    FileInfo fi = new FileInfo(filepath);
                    if (fi.Exists)
                    {
                        Console.Clear();
                        FileStream file1 = new FileStream(@$"{filepath}", FileMode.Open);
                        StreamReader reader = new StreamReader(file1);
                        Console.WriteLine(reader.ReadToEnd());
                        reader.Close();
                    }
                }
                if (command == "Help" || command == "help")
                {
                    Console.WriteLine("ls - просмотр сожержимого директории");
                    Console.WriteLine("cd - переход по директориям");
                    Console.WriteLine("clear или cls - очистка экрана терминала");
                    Console.WriteLine("exit - выход из программы");
                    Console.WriteLine("touch - создание файла");
                    Console.WriteLine("rm - удаление файла");
                    Console.WriteLine("cp - копирование файла в указанную директорию");
                    Console.WriteLine("openfl - прочтение содержимого  файла");
                    Console.WriteLine("help - показать эту страницу");
                    Console.WriteLine("1. Нажмите 1 для очистки терминала");
                    Console.WriteLine("2. Нажмите 2 для сохранения на экране этой памятки");
                    int a;
                    a = Convert.ToInt32(Console.ReadLine());
                    if (a == 1)
                    {
                        Console.Clear();
                    }
                    else if (a == 2) { }
                    else
                    {
                        Console.WriteLine("нет такой команды, введите одну из предложенных");
                    }
                }
            }
        }
    }
}