using System;
using System.Collections.Generic;
using System.Text;

namespace TerminalEmulator
{
    public class Catalog : IAct
    {
        public string path;
        private List<IAct> acts;
        bool flagExit = false;//Флаг выхода из каталога

        public string Name { get; set; }

        //TODO дописать конструкторы
        public Catalog(string name)
        {
            Name = name;
            path = $"path: {name}";
            StandardСommands();
        }
        public Catalog(string name, string path) : this(name)
        {
            this.path = $"{path}/{name}";
            StandardСommands();
        }
        private void StandardСommands()
        {
            acts = new List<IAct>();
            acts.Add(new Command("help", () => { CommandHelp(); }));
            acts.Add(new Command("path", () => { Console.WriteLine(this.path); }));
            acts.Add(new Command("exit", () => { flagExit = true; }));
        }

        private void CommandHelp()
        {
            Console.WriteLine($"Команды каталога {path}");
            foreach (var i in acts)
                Console.WriteLine(i.Name);
        }
        public void AddCommand(string name, Action operation)
        {
            acts.Add(new Command(name, operation));
        }


        public void OpenDirectory(string name)
        {
            bool flagAct = false;
            foreach (var i in acts)
            {
                if (i.Name == name)
                {
                    i.Operation();
                    flagAct = true;
                }
            }
            if (flagAct == false)
                Console.WriteLine($"Такой команды не существует, help показать все команды.");
        }

        public void Operation()
        {
            while (flagExit == false)
            {
                Console.Write($"{this.Name}:");
                string enter = Console.ReadLine();
                OpenDirectory(enter);
            }
            flagExit = false;
        }
    }
}
