using System;
using System.IO;
using System.Management;

namespace Командная_Строка
{

    class Program
    {
        static void print()
        {
            Console.WriteLine("/diskinfo - информация о диске", "\n");
            Console.WriteLine("/cls - очистить консоль", "\n");
            Console.WriteLine("/catalog - информация о файле", "\n");
            Console.WriteLine("/create - создание файла", "\n");
            Console.WriteLine("/copyfile - создание файла", "\n");
            Console.WriteLine("/delfile - удаление файла", "\n");
            Console.WriteLine("/wrfile - запись данных в файл", "\n");
            Console.WriteLine("/rdfile - чтение из файла", "\n");
        }
        static void cls()
        {
            Console.Clear();
        }
        static void Catalog()
        {
            string PathToTheFile = Environment.CurrentDirectory;
            Console.WriteLine(PathToTheFile);
            Console.WriteLine("Что вы хотите сделать?");
            Console.WriteLine("/goto - переход в другую директорию");
        }
        static void AnotherDirectory()
        {
            Console.WriteLine("Введите новую директорию");
            string newPath;
            newPath = Console.ReadLine();
            string oldPath = Environment.CurrentDirectory;
            try
            {
                Directory.Move(newPath, oldPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
        }
        static void DiskInfo()
        {
            System.IO.DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Название: {drive.Name}");
                Console.WriteLine($"Свободного места на диске:{drive.AvailableFreeSpace}");
                Console.WriteLine($"Тип: {drive.DriveFormat}");
            }
        }
        static void CreateFile()
        {
            Console.WriteLine("Введите путь и название файла");
            string file = Console.ReadLine();
            File.Create(file);
        }
        static void CopyFIle()
        {
            Console.WriteLine("Укажите путь к файлу и путь, в который нужно скопировать");
            string sourse;
            sourse = Console.ReadLine();
            string destination;
            destination = Console.ReadLine();
            File.Copy(sourse, destination);
        }
        static void DeleteFile()
        {
            Console.WriteLine("Укажите путь к файлу который хотите удалить");
            string path;
            path = Console.ReadLine();
            File.Delete(path);

        }
        static void WriteFile()
        {
            Console.WriteLine("Введите текст");
            string text = Console.ReadLine();
            Console.WriteLine("Укажите путь к файлу, в который записать информацию");
            string path = Console.ReadLine();
            File.WriteAllText(path, text);
        }
        static void ReadFile()
        {
            Console.WriteLine("Введите путь к файлу, из которого нужно читать данные");
            string path = Console.ReadLine();
            string[] lines = File.ReadAllLines(path);
            foreach (String line in lines)
            {
                Console.WriteLine(line);
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введите команду /info, чтобы узнать больше о доступных программах");
            string command;
            do
            {
                command = Console.ReadLine();
                switch (command)
                {
                    case "/info":
                        print();
                        break;
                    case "/cls":
                        cls();
                        break;
                    case "/catalog":
                        Catalog();
                        break;
                    case "/diskinfo":
                        DiskInfo();
                        break;
                    case "/goto":
                        AnotherDirectory();
                        break;
                    case "/create":
                        CreateFile();
                        break;
                    case "/copyfile":
                        CopyFIle();
                        break;
                    case "/delfile":
                        DeleteFile();
                        break;
                    case "/wrfile":
                        WriteFile();
                        break;
                    case "/rdfile":
                        ReadFile();
                        break;
                }

            } while (command != "/exit");

        }
    }
}
