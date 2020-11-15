using System;
using System.IO;
using System.Threading.Tasks;

namespace TerminalEmulator
{
	class Program
	{
		static void Main(string[] args)
		{
			bool working = true;
			string dir_name = "C:\\";
			string log_name, command, catalogname, drive_name, file_name;

			do
			{
				Console.Write(dir_name);
				Console.Write("\\");
				command = Console.ReadLine();

				switch (command)
				{
					case "help":
						Console.WriteLine("Команда pwd:\tпоказывает содержимое каталога");
						Console.WriteLine("Команда open:\tоткрывает указанный каталог");
						Console.WriteLine("Команда dname:\tпоказывает имена имеющихся дисков");
						Console.WriteLine("Команда dopen:\tпозволяет перейти к каталогу указанного диска");
						Console.WriteLine("Команда cnew:\tпозволяет создать новый каталог");
						Console.WriteLine("Команда cdel:\tпозволяет удалить каталог");
						Console.WriteLine("Команда fnew:\tпозволяет создать новый файл");
						Console.WriteLine("Команда fread:\tпозволяет прочитать файл");
						Console.WriteLine("Команда сс:\tзавершает работу приложения");
						break;
					case "fread":
						Console.WriteLine("Укажите имя файла.txt");
						file_name = Console.ReadLine();
						using (FileStream fstream = File.OpenRead($"{dir_name + "\\"}{file_name}"))
						{
							
							byte[] array = new byte[fstream.Length];
							fstream.Read(array, 0, array.Length);
							string textFromFile = System.Text.Encoding.Default.GetString(array);
							Console.WriteLine($"Текст из файла: {textFromFile}");
						}
						break;
					case "cdel":
						try
						{
							new DirectoryInfo(dir_name).Delete(true);
							Console.WriteLine("Каталог удален");
							dir_name = "D:\\";
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
						}
						break;

					case "cnew":
						catalogname = Console.ReadLine();
						dir_name += catalogname;
						DirectoryInfo dirInfo = new DirectoryInfo(dir_name);
						if (!dirInfo.Exists)
						{
							dirInfo.Create();
							dir_name += "\\";
						}
						break;

					case "fnew":
						Console.WriteLine("Укажите имя файла.txt");
						file_name = Console.ReadLine();
						Console.WriteLine("Введите строку для записи в файл:");
						string text = Console.ReadLine();

						using (FileStream fstream = new FileStream($"{dir_name + "\\"}{file_name}", FileMode.OpenOrCreate))
						{
							byte[] array = System.Text.Encoding.Default.GetBytes(text);
							fstream.Write(array, 0, array.Length);
							Console.WriteLine("Текст записан в файл");
						}
						break;

					case "cc":
						working = false;
						break;

					case "dopen":
						drive_name = Console.ReadLine();
						dir_name = drive_name +":\\";
						if (!Directory.Exists(dir_name))
						{
							Console.WriteLine("Директория не найдена!");
							dir_name = "D:\\";
						}
						break;

					case "dname":
						string[] drives = Directory.GetLogicalDrives();
						foreach (var item in drives)
						{
							Console.WriteLine(item);
						}
						break;

					case "open":
						log_name = Console.ReadLine();
						dir_name += log_name + "\\";
						if (!Directory.Exists(dir_name))
						{
							Console.WriteLine("Директория не найдена!");
							dir_name = "D:\\";
						}
						break;

					case "pwd":
						if (Directory.Exists(dir_name))
						{
							Console.WriteLine("Подкаталоги:");
							string[] dirs = Directory.GetDirectories(dir_name);
							foreach (string s in dirs)
							{
								Console.WriteLine(s);
							}
							Console.WriteLine();
							Console.WriteLine("Файлы:");
							string[] files = Directory.GetFiles(dir_name);
							foreach (string s in files)
							{
								Console.WriteLine(s);
							}
						}
						break;

					default:
						Console.WriteLine("Неизвестная команда!\nЧтобы ознакомиться с полным списком команд воспользуйтесь командой help");
						break;
				}
			} while (working);
		}
	}
}
