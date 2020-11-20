using System;
using System.Collections.Generic;
using System.Text;

namespace Terminal
{
    public class Catalog : StructOperation
    {
        private List<StructOperation> commands;
        public Catalog(string name) : base(name)
        {
            this.path = $"path: {name}";
            DefoltCommand();
        }
        //2 Конструктор для создания каталога внутри каталога в него передаётся путь (path) 
        public Catalog(string name, string path) : base(name)
        {
            this.path = $"{path}/{name}";
            DefoltCommand();
        }
        private void DefoltCommand()
        {
            flagExit = false;
            commands = new List<StructOperation>();
            commands.Add(new Command("help", () => { CommandHelp(); }));
            commands.Add(new Command("path", () => { Console.WriteLine(this.path); }));
            commands.Add(new Command("exit", () => { flagExit = true; }));
        }
        private void CommandHelp()
        {
            Console.WriteLine($"Все команды в каталоге {path}");
            foreach (var item in commands)
                Console.WriteLine(item.name);
                
        }
        public void AddCommand(string name, Action operation)
        {
            commands.Add(new Command(name, operation));
        }

        
        public void OpenDirectory(string name)
        {
            bool flagAct = false;
            foreach (var i in commands)
            {
                if (i.name == name)
                {
                    i.PerformingOperation();
                    flagAct = true;
                }
            }
            if (flagAct == false)
                Console.WriteLine($"Команды '{name}' не существует, введите help что бы посмотреть все команды ");

        }
        bool flagExit; // Флаг для выхода из PerformingOperation
        public override void PerformingOperation()
        {
            while (flagExit == false)
            {
                Console.Write($"{this.name}:");
                string enter = Console.ReadLine();
                OpenDirectory(enter);
            }
            flagExit = false;
        }
    }
}
