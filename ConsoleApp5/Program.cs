using Microsoft.VisualBasic;
using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Threading;
using System.IO;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
       //exit(выход)y,cls(очистка)y,help(команды)y,mkdir(создание)y
       //rmdir(удаление)y,dirinfo(содержимое и информация папки)y,WTF(самбади)y,cd(смена папки)y
       //showfileinfo(вывод текста из файла в консоли)y
       //filecopy(копирование файла)y
        {
            bool consoleEmulate = true;//пока true консоль работает
            string fullCommand;//ввод полной команды пользователем
            string commandCdWay = @"C:\Program Files";
            while (consoleEmulate)
            {
                //текущая директория
                Console.Write($@"{commandCdWay}>");
                fullCommand = Console.ReadLine();//ввод полной команды пользователем
                string command="g", commandCharacter="g";
                string copyFileDirectoryWay="g";
                bool firstSymbol = true;//т.к. запись команды идет посимвольно и я просто складываю тип string,
                //первый символ присваивается без +
                bool part2 = false;//переход на посимвольную запись второй части - параметров команды - commandChar
                bool part3 = false;//доп проверка для команды copyfile, т.к. остальные команды имеют лишь 1 пробел, а она 2
               for(int i=0;i<fullCommand.Length;i++)//запись полной команды в 2-3 части
                {

                    if (fullCommand[i] != ' '&&!part2)//запись command
                    {
                        if(firstSymbol)
                        {
                            command = Convert.ToString(fullCommand[i]);
                            firstSymbol = false;
                        }
                        else
                        {
                            command += fullCommand[i];
                        }
                       
                    }
                    else if(!part3)//запись commandCharacter
                    {
                        if (fullCommand[i] == ' '&&part2)
                        {
                            part3 = true;
                        }
                        if (!part2)
                        {
                            firstSymbol = true;
                        }
                        part2 = true;
                      
                        if(firstSymbol&&fullCommand[i] != ' ')
                        {
                            commandCharacter = Convert.ToString(fullCommand[i]);
                            firstSymbol = false;
                        }
                        else if(!firstSymbol)
                        {
                            commandCharacter += fullCommand[i];
                        }
                    }
                    else if(part3)//copyfile directory way
                    {
                        if(fullCommand[i]==' ')
                        {
                            firstSymbol = true;
                        }
                        if (firstSymbol && fullCommand[i] != ' ')
                        {
                            copyFileDirectoryWay = Convert.ToString(fullCommand[i]);
                            firstSymbol = false;
                        }
                        else if(!firstSymbol)
                        {
                            copyFileDirectoryWay += fullCommand[i];
                        }
                        
                    }
                    
                }
                
                if (command == "exit")//выход из консоли
                {
                    consoleEmulate = false;
                }
                else if (command == "cls")//очистка консоли
                {
                    Console.Clear();
                }
                else if (command == "help")//список команд
                {
                    Console.WriteLine("mkdir [file name] - создание папки");
                    Console.WriteLine("rmdir [file name] - удаление папки");
                    Console.WriteLine("dirinfo - содержимое и информация папки");
                    Console.WriteLine("cd [directory way] - смена текущей директории");
                    Console.WriteLine("showfileinfo [file name] - вывод текста из файла в консоли");
                    Console.WriteLine("filecopy [file name] [directory way copy] - копирование файла");
                    Console.WriteLine("(при выборе копируемого файла вы должны находиться в его папке)");
                    Console.WriteLine("exit - выход из консоли");
                    Console.WriteLine("cls - очистка консоли");
                    Console.WriteLine("WTF - All Star");

                }
                else if (command == "mkdir")//создание файла
                {
                    FileInfo fileInf = new FileInfo(@$"{commandCdWay}\{commandCharacter}");

                    if (!fileInf.Exists)
                    {
                      var fileInf1= File.Create(@$"{commandCdWay}\{commandCharacter}");
                        fileInf1.Close();
                    }
                    

                }
                else if (command == "rmdir")//удаление файла
                {
                    FileInfo fileInf = new FileInfo(@$"{commandCdWay}\{commandCharacter}");
                    if (fileInf.Exists)
                    {
                        fileInf.Delete();
                       // File.Delete(@$"{commandCdWay}\{commandCharacter}");
                    }

                }

                else if (command == "dirinfo")//содержимое папки
                {
                    Console.WriteLine($"Folders and files in {commandCdWay} :");
                    string[] dirs = Directory.GetDirectories(commandCdWay);
                    foreach (string subdir in dirs)
                    {
                        Console.WriteLine(subdir);
                    }
                    string[] files = Directory.GetFiles(commandCdWay);
                    foreach (string file1 in files)
                    {
                        Console.WriteLine(file1);
                    }

                }
                else if (command == "cd")//переход между директориями
                {
                    if (fullCommand == "cd")//при вводе cd без доп параметров переход на старшую папку
                    {
                        int count = 0;
                        int i = commandCdWay.Length-1;
                        bool deleteWay = true;
                        while(deleteWay)
                        {
                            if (commandCdWay[i] != '\\')
                                {
                                count++;
                            }
                            else
                            {
                                deleteWay = false;
                            }
                            i--;
                        }
                        commandCdWay=commandCdWay.Remove(commandCdWay.Length - count,count);
                    }
                    else
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(commandCharacter);
                        if (dirInfo.Exists)//запись новой текущей директории
                        {
                            commandCdWay = commandCharacter;
                        }
                        else//если путь введен неверно
                        {
                            string dateTime = DateTime.Now.ToString();
                            Console.WriteLine($"{dateTime} ERROR. Incorrect way, folder cannot found.");
                        }

                    }
                }
                else if (command == "showfileinfo")//вывод содержимого txt файла
                {
                    FileInfo fileExist = new FileInfo($@"{commandCdWay}\{commandCharacter}");
                    if(fileExist.Exists)
                    {
                        StreamReader fileReader = new StreamReader($@"{commandCdWay}\{commandCharacter}");
                        Console.WriteLine(fileReader.ReadToEnd());
                        fileReader.Close();
                    }
                    else//если путь введен неверно
                    {
                        string dateTime = DateTime.Now.ToString();
                        Console.WriteLine($"{dateTime} ERROR. File cannot found.");
                    }
                   
                }
                else if(command=="filecopy")//копирование файла в другую папку
                {
                    copyFileDirectoryWay=copyFileDirectoryWay.Remove(0,1);
                    commandCharacter = commandCharacter.Trim();
                    FileInfo fileInfo = new FileInfo(@$"{commandCdWay}\{commandCharacter}");
                    DirectoryInfo copyDirInfo = new DirectoryInfo(copyFileDirectoryWay);

                    if (!fileInfo.Exists)//создание пустой копии файла
                    {
                        File.Create(@$"{copyFileDirectoryWay}\{commandCharacter}");
                    }
                    if (fileInfo.Exists&&copyDirInfo.Exists)//копирование текста
                    {
                         fileInfo.CopyTo(@$"{copyFileDirectoryWay}\{commandCharacter}", true);
                        

                    }
                    else
                    {
                       string dateTime = DateTime.Now.ToString();
                      Console.WriteLine($"{dateTime} ERROR. File cannot found or incorrect copy way");
                    }
                }
                else if (command == "WTF")//самбади онс толд ми...
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Somebody once told me");
                    Thread.Sleep(1000);
                    Console.WriteLine("The world is gonna roll me");
                    Thread.Sleep(1000);
                    Console.WriteLine("I ain't the sharpest tool in the shed");
                    Thread.Sleep(1000);
                    Console.WriteLine("She was lookin kinda dumb with her finger and her thumb");
                    Thread.Sleep(1000);
                    Console.WriteLine("In the shape of an 'L' on her forehead");
                    Thread.Sleep(1000);
                    Console.WriteLine("Well the years start coming and they don't stop coming");
                    Thread.Sleep(1000);
                    Console.WriteLine("Fed to the rules and I hit the ground running");
                    Thread.Sleep(1000);
                    Console.WriteLine("Didn't make sense not to live for fun");
                    Thread.Sleep(1000);
                    Console.WriteLine("Your brain gets smart but your head gets dumb.");
                    Thread.Sleep(1000);
                    Console.WriteLine("So much to do so much to see");
                    Thread.Sleep(1000);
                    Console.WriteLine("So what's wrong with taking the back streets?");
                    Thread.Sleep(1000);
                    Console.WriteLine("You'll never know if you don't go");
                    Thread.Sleep(1000);
                    Console.WriteLine("You'll never shine if you don't glow");


                }
                else//если команда введена некорректно выводится ошибка
                {
                    string dateTime = DateTime.Now.ToString();
                    Console.WriteLine($"{dateTime} ERROR. Incorrect command, enter 'help' to see command list.");
                }
            }
            
        }
    }
}
