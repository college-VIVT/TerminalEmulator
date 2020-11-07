using System;
using static System.Console;

namespace TerminalEmulator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Write("Terminal>"); string msg = ReadLine().ToUpper();
                switch (msg)
                {
                    case "DIR":

                        break;

                    case "MOVETO":

                        break;

                    case "MADE":

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
                        WriteLine("DIV                  Выводит список файлов и подкаталогов в текущем каталоге\n");
                        WriteLine("EXIT                 Завершает программу\n");
                        WriteLine("HELP                 Выводит справочную информацию о командах\n");
                        WriteLine("MADE                 Создание текстового файла");
                        WriteLine("MOVETO               Переход в указанный каталог\n");
                        WriteLine("READ                 Выводит содержимое текстового файла\n");
                        break;

                    case "EXIT":
                        return;
                }
            }
        }
    }
}
