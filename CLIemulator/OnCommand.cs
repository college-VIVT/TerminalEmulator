using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace CLIemulator
{
    public static class OnCommand
    {
        public static void OnCommand_LS_DIR(List<string> args, int argsCount)
        {
            var path = VStore.CurrentPath;
            if (argsCount == 1)
            {
                path = args[0];
                if (path == "~")
                    path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                else if (path.StartsWith(".."))
                    path = Path.GetFullPath(Path.Combine(VStore.CurrentPath, path));
            }

            var folderContentFiles = Directory.GetFiles(path);
            var folderContentDirectoryes = Directory.GetDirectories(path);

            Console.WriteLine("Время изменения\t\tТип\tРазмер (байт)\tИмя");
            foreach (var filePath in folderContentFiles)
            {
                var fileInfo = new FileInfo(filePath);
                Console.WriteLine($"{fileInfo.LastWriteTime}\t<FILE>\t{fileInfo.Length}\t{fileInfo.Name}");
            }
            foreach (var DirectoryPath in folderContentDirectoryes)
            {
                var dirInfo = new DirectoryInfo(DirectoryPath);
                Console.WriteLine($"{dirInfo.LastWriteTime}\t<Dir>\t\t{dirInfo.Name}");
            }
            Console.WriteLine($"Файлов: {folderContentFiles.Length}\nПапок: {folderContentDirectoryes.Length}");
        }

        public static void OnCommand_HELP()
        {
            Console.WriteLine("Help list: ");
            foreach (var line in CommandStats.HelpList)
                Console.WriteLine(line);
        }

        public static void OnCommand_MOVE_MV(List<string> args, int argsCount)
        {
            if (File.Exists(args[0]))
            {
                if (argsCount == 3)
                {
                    if (args[2] != "-f")
                    {
                        ConsoleHelper.LogError($"Unknown argument: {args[2]}. Use -f for overwrite file");
                        return;
                    }
                    if (File.Exists(args[1]))
                        File.Delete(args[1]);
                }

                try
                {
                    File.Move(args[0], args[1]);
                }
                catch (FileNotFoundException)
                {
                    ConsoleHelper.LogError($"Не удалось найти {args[0]}");
                }
                catch (IOException)
                {
                    ConsoleHelper.LogError($"Файл {args[1]} уже существует");
                }
                catch (UnauthorizedAccessException)
                {
                    ConsoleHelper.LogError("Недостаточно прав");
                }
            } else
            {
                ConsoleHelper.LogError($"Не удалось найти {args[0]}");
            }
        }

        public static void OnCommand_COPY_CP(List<string> args, int argsCount)
        {
            var overwrite = false;
            if (argsCount == 3)
            {
                if (args[2] == "-f")
                {
                    overwrite = true;
                }
                else
                {
                    ConsoleHelper.LogError($"Unknown argument: {args[2]}. Use -f for overwrite file");
                    return;
                }
            }

            try
            {
                File.Copy(args[0], args[1], overwrite);
            }
            catch (UnauthorizedAccessException)
            {
                ConsoleHelper.LogError("Недостаточно прав");
            }
            catch (DirectoryNotFoundException)
            {
                ConsoleHelper.LogError("Указан недопустимый путь");
            }
            catch (FileNotFoundException)
            {
                ConsoleHelper.LogError($"Не удалось найти {args[0]}");
            }
            catch (IOException)
            {
                ConsoleHelper.LogError($"{args[1]} уже существует. Для перезаписи используйте флаг -f");
            }
        }

        public static void OnCommand_CD(List<string> args, int argsCount)
        {
            var path = args[0];
            if (path == "~")
                path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            else if (path.StartsWith(".."))
                path = Path.GetFullPath(Path.Combine(VStore.CurrentPath, path));
            else if (!Directory.Exists(path))
            {
                ConsoleHelper.LogError("Указан неверный путь. Проверьте правильность введенного пути.");
                return;
            }
            VStore.CurrentPath = path.Replace('/', '\\');



        }

        public static void OnCommand_PWD()
        {
            Console.WriteLine(VStore.CurrentPath);
        }

        public static void OnCommand_ECHO(List<string> args, int argsCount)
        {
            if (argsCount >= 1)
                foreach (var line in args)
                    Console.WriteLine(line);
            else
                Console.WriteLine();
        }
    }
}
