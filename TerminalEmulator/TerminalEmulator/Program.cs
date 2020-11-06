using System;

namespace TerminalEmulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Terminal terminal = new Terminal();
            while (true)
            {
                Console.WriteLine("1.Войти в терминал");
                Console.WriteLine("2.Выход из программы");
                Console.Write("[Enter]");
                int input = Convert.ToInt32(Console.ReadLine());

                if (input == 1)
                    terminal.CustomMode();
                if (input == 2)
                    break;
            }
        }
    }
}
