using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace TerminalApp
{
    public class Terminal
    {
        public void start()
        {
            bool exit = true;
            while (exit)
            {
                string userComand, way = @"user\";

                Console.Write("[User]: ");
                userComand = Console.ReadLine();

                switch (userComand)
                {
                    case "ShowTheWay":
                        Console.WriteLine($"[Console]: {way}");
                        Console.WriteLine();
                        break;

                    case "Eneble":
                        Console.WriteLine("[Console]: Access is allowed");
                        Console.WriteLine();
                        adminCat();
                        break;

                    case "Help":
                        Console.WriteLine("[Console]: ShowTheWay Eneble Exit Help");
                        Console.WriteLine();
                        break;

                    case "Exit":
                        exit = false;
                        break;

                    default:
                        Console.WriteLine("[Console]: Введена неверная команда! Напишите Help, что бы узнать список команд.");
                        Console.WriteLine();
                        break;
                }
            }
            Console.ReadKey();
        }

        private void adminCat()
        {
            string userComand, way = @"user\admin\";

            
            string path = @"D:\Doc\doc.txt", choice;
            FileInfo fileInf = new FileInfo(path);


            bool exit = true;

            while (exit)
            {
                Console.Write("[Admin]: ");
                userComand = Console.ReadLine();
                

                switch (userComand)
                {
                    case "ShowTheWay":
                        Console.WriteLine($"[Console]: {way}");
                        Console.WriteLine();
                        break;

                    case "Disable":
                        Console.WriteLine();
                        exit = false;
                        break;

                    case "Help":
                        Console.WriteLine("[Console]: ShowTheWay ExtraEneble Disable CreateNewFile CopyFile DeleteFile OpenFile Help");
                        Console.WriteLine();
                        break;

                    case "ExtraEneble":
                        Console.WriteLine("[Console]: Access is allowed");
                        Console.WriteLine();
                        extraAdminCat();
                        break;

                    case "CreateNewFile":
                        Console.WriteLine($"[Console]: Текущий маршрут файла: {path}");
                        Console.WriteLine("[Console]: Хотите поменять маршрут файла? (Yes | No) \n[Admin]: ");
                        choice = Console.ReadLine();
                        if (choice == "Yes")
                        {
                            Console.Write("[Console]: Введите новый маршрут файла: ");
                            path = Console.ReadLine();
                            fileInf.Create();
                            Console.WriteLine("[Console]: Файл успешно создан");
                            Console.WriteLine();
                        }
                        else if (choice == "No") 
                        {
                            fileInf.Create();
                            Console.WriteLine("[Console]: Файл успешно создан");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("[Console]: Ошибка! Указано неверное значение");
                            Console.WriteLine();
                        }
                        break;

                    case "CopyFile":
                        if (fileInf.Exists)
                        {
                            Console.Write("[Console]: Введите мaршрут копирования: ");
                            path = Console.ReadLine();
                            fileInf.CopyTo(path);
                            Console.WriteLine("[Console]: Файл успешно скопирован!");
                        }
                        else
                        {
                            Console.WriteLine("[Console]: Ошибка при копировании! Файла не существует");
                        }
                        break;

                    case "DeleteFile":
                        Console.WriteLine("[Console]: Вы точно хотите удалить файл? (Yes | No)\n[Admin]: ");
                        choice = Console.ReadLine();
                        if (choice == "Yes" && fileInf.Exists)
                        {
                            fileInf.Delete();
                            Console.WriteLine("[Console]: Файл успешно удалён");
                        }
                        else if (choice == "No")
                        {
                            Console.WriteLine("[Console]: Удаление файла отменено");
                        }
                        else if (!fileInf.Exists)
                        {
                            Console.WriteLine("[Console]: Ошибка при удалении! Файла не существует");
                        }
                        else
                        {
                            Console.WriteLine("[Console]: Ошибка! Указано неверное значение");
                            Console.WriteLine();
                        }
                        break;

                    case "OpenFile":
                        if (fileInf.Exists)
                        {
                            using (FileStream fstream = File.OpenRead($@"{path}"))
                            {
                                byte[] array = new byte[fstream.Length];
                                fstream.Read(array, 0, array.Length);
                                string textFromFile = System.Text.Encoding.Default.GetString(array);
                                Console.WriteLine($"[Console]: {textFromFile}");
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("[Console]: Ошибка при попытке чтения! Файла не существует");
                            Console.WriteLine();
                        }
                        break;

                    default:
                        Console.WriteLine("[Console]: Введена неверная команда! Напишите Help, что бы узнать список команд.");
                        Console.WriteLine();
                        break;
                }
            }
        }

        private void extraAdminCat()
        {
            string userComand, way = @"user\admin\extra\";

            bool exit = true;

            while (exit)
            {
                Console.Write("[Extra]: ");
                userComand = Console.ReadLine();

                switch (userComand)
                {
                    case "ShowTheWay":
                        Console.WriteLine($"[Console]: {way}");
                        Console.WriteLine();
                        break;

                    case "ExtraDisable":
                        Console.WriteLine();
                        exit = false;
                        break;

                    case "Help":
                        Console.WriteLine("[Console]: ShowTheWay ExtraDisable Help");
                        Console.WriteLine();
                        break;

                    default:
                        Console.WriteLine("[Console]: Введена неверная команда! Напишите Help, что бы узнать список команд.");
                        Console.WriteLine();
                        break;
                }
            }
        }

    }
}
