using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;

namespace TerminalEmulator
{
    public static class OnCommand
    {
        public static void OnCommand_Ls_Dir(List<string> args, int argsCount)
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

        public static void OnCommand_Help()
        {
            Console.WriteLine("Help list: ");
            foreach (var line in CommandStats.HelpList)
                Console.WriteLine(line);
        }

        public static void OnCommand_Move_Mv(List<string> args, int argsCount)
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

        public static void OnCommand_Copy_Cp(List<string> args, int argsCount)
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
            if (path.StartsWith("~"))
                if (path.Length > 1)
                    path = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), path.Substring(2)));
                else
                    path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            else if (path.StartsWith(".."))
                path = Path.GetFullPath(Path.Combine(VStore.CurrentPath, path));
            else if (path[1] != ':')
                path = Path.Combine(VStore.CurrentPath, path);

            if (!Directory.Exists(path))
            {
                ConsoleHelper.LogError("Указан неверный путь. Проверьте правильность введенного пути.");
                return;
            }

            VStore.CurrentPath = path.Replace('/', '\\');
        }

        public static void OnCommand_Del_Delete_Rm_Rem_Remove(List<string> args, int argsCount)
        {
            var path = args[0];
            if (path.StartsWith("~"))
                if (path.Length > 1)
                    path = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), path.Substring(2)));
                else
                {
                    ConsoleHelper.LogError("Указан недопустимый путь");
                    return;
                }
            else if (path.StartsWith(".."))
                path = Path.GetFullPath(Path.Combine(VStore.CurrentPath, path));
            else if (path[1] != ':')
                path = Path.Combine(VStore.CurrentPath, path);

            try
            {
                File.Delete(path);
            }
            catch (DirectoryNotFoundException)
            {
                ConsoleHelper.LogError("Указан недопустимый путь");
            } 
            catch (IOException)
            {
                ConsoleHelper.LogError($"Файл {path} используется");
            } 
            catch (UnauthorizedAccessException)
            {
                if (Directory.Exists(path))
                    ConsoleHelper.LogError("Указана ссылка на каталог");
                else
                    ConsoleHelper.LogError("Недостаточно прав или файл запущен");
            }
        }

        public static void OnCommand_Touch_CreateFile(List<string> args, int argsCount)
        {
            var path = args[0];
            if (path.StartsWith("~"))
                if (path.Length > 1)
                    path = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), path.Substring(2)));
                else
                {
                    ConsoleHelper.LogError("Указан недопустимый путь");
                    return;
                }
            else if (path.StartsWith(".."))
                path = Path.GetFullPath(Path.Combine(VStore.CurrentPath, path));
            else if (path[1] != ':')
                path = Path.Combine(VStore.CurrentPath, path);

            try
            {
                File.Create(path).Close();
            } 
            catch (UnauthorizedAccessException)
            {
                ConsoleHelper.LogError("Недостаточно прав или файл скрыт");
            } 
            catch (DirectoryNotFoundException)
            {
                ConsoleHelper.LogError("Указан недопустимый путь");
            }
        }

        public static void OnCommand_Cat_Less(List<string> args, int argsCount)
        {
            var path = args[0];
            if (path.StartsWith("~"))
                if (path.Length > 1)
                    path = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), path.Substring(2)));
                else
                {
                    ConsoleHelper.LogError("Указан недопустимый путь");
                    return;
                }
            else if (path.StartsWith(".."))
                path = Path.GetFullPath(Path.Combine(VStore.CurrentPath, path));
            else if (path[1] != ':')
                path = Path.Combine(VStore.CurrentPath, path);

            try
            {
                var text = File.ReadAllText(path);
                Console.WriteLine($"Содержимое файла {path}:\n");
                Console.WriteLine(text);
            }
            catch (DirectoryNotFoundException)
            {
                ConsoleHelper.LogError("Указан недопустимый путь");
            }
            catch (FileNotFoundException)
            {
                ConsoleHelper.LogError($"Файл {path} не найден");
            }
            catch (IOException)
            {
                ConsoleHelper.LogError($"Ошибка при открытии файла {path}");
            }
            catch (UnauthorizedAccessException)
            {
                if (Directory.Exists(path))
                    ConsoleHelper.LogError("Указана ссылка на каталог");
                else
                    ConsoleHelper.LogError("Нет необходимого разрешения");
            }
            catch (SecurityException)
            {
                ConsoleHelper.LogError("Нет необходимого разрешения");
            }
        }

        public static void OnCommand_MD_MkDir(List<string> args, int argsCount)
        {
            var path = args[0];
            if (path.StartsWith("~"))
                if (path.Length > 1)
                    path = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), path.Substring(2)));
                else
                {
                    ConsoleHelper.LogError("Указан недопустимый путь");
                    return;
                }
            else if (path.StartsWith(".."))
                path = Path.GetFullPath(Path.Combine(VStore.CurrentPath, path));
            else if (path[1] != ':')
                path = Path.Combine(VStore.CurrentPath, path);

            try
            {
                Directory.CreateDirectory(path);
            }
            catch (DirectoryNotFoundException)
            {
                ConsoleHelper.LogError("Указан недопустимый путь");
            }
            catch (IOException)
            {
                ConsoleHelper.LogError($"{path} является файлом");
            }
            catch (UnauthorizedAccessException)
            {
                ConsoleHelper.LogError("Недостаточно прав");
            }
        }

        public static void OnCommand_RmDir(List<string> args, int argsCount)
        {
            var remAll = false;
            if (argsCount == 2)
            {
                if (args[1] == "-f")
                {
                    remAll = true;
                }
                else
                {
                    ConsoleHelper.LogError($"Unknown argument: {args[2]}. Используйте -f для удаления не пустых каталогов");
                    return;
                }
            }

            var path = args[0];
            if (path.StartsWith("~"))
                if (path.Length > 1)
                    path = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), path.Substring(2)));
                else
                {
                    ConsoleHelper.LogError("Указан недопустимый путь");
                    return;
                }
            else if (path.StartsWith(".."))
                path = Path.GetFullPath(Path.Combine(VStore.CurrentPath, path));
            else if (path[1] != ':')
                path = Path.Combine(VStore.CurrentPath, path);

            try
            {
                Directory.Delete(path, remAll);
            }
            catch (DirectoryNotFoundException)
            {
                ConsoleHelper.LogError("Указан недопустимый путь");
            }
            catch (IOException)
            {
                ConsoleHelper.LogError("Каталог используется или доступен только для чтения или же каталог не пустой. Для удаления не пустых каталогов используйте флаг -f в конце");
            } 
            catch (UnauthorizedAccessException)
            {
                ConsoleHelper.LogError("Недостаточно прав");
            }
        }

        public static void OnCommand_PWD()
        {
            Console.WriteLine(VStore.CurrentPath);
        }

        public static void OnCommand_Echo(List<string> args, int argsCount)
        {
            if (argsCount >= 1)
                foreach (var line in args)
                    Console.WriteLine(line);
            else
                Console.WriteLine();
        }
    }
}
