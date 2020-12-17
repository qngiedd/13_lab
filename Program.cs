using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Files
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите имя диска (например D, C...):");
            string DriveName = Console.ReadLine();

            DASLog.DASDiskInfo.ShowFreeSpace(DriveName);
            DASLog.DASDiskInfo.ShowFileSystem(DriveName);
            DASLog.DASDiskInfo.AllInfo();

            Console.WriteLine("Введите путь к файлу по которому будем получать информацию:");
            string path1 = Console.ReadLine();         
            DASLog.DASFileInfo.ShowFilePath(path1);
            DASLog.DASFileInfo.ShowFileSizeExtAndName(path1);
            DASLog.DASFileInfo.ShowCreationTime(path1);

            Console.WriteLine("Введите путь к папке по которой будем получать информацию:");
            string path2 = Console.ReadLine();
            DASLog.DASDirinfo.NumberOfFiles(path2);
            DASLog.DASDirinfo.ListOfDirectory(path2);
            DASLog.DASDirinfo.ParentDirectory(path2);

            DASLog.DASFileManager.Task_a();

            Console.WriteLine("Введите путь к папке, из которой будут скопированы все файлы с расширением docx:");
            string path3 = Console.ReadLine();

            DASLog.DASFileManager.Task_b(path3);

            DASLog.DASFileManager.Task_c();
        }
    }
}

namespace Files
{

    public delegate void userActions(string str);
    class DASLog
    {

        private const string path = "D:\\SomeDir\\12lab.txt";
        public static event userActions Observe = (str) =>  //событие для остлеживания действий пользователя

        {
            int number = 0;
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
                {

                    sw.WriteLine(str);
                    sw.WriteLine($"Дата использования:{DateTime.Now}");
                    sw.WriteLine("--------------------------------------------------------------------------------------------------------");
                    number++;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine($"В файле daslogfile уже {number} запись(ей)");
        };

        public static class DASDiskInfo
        {
            public static void ShowFreeSpace(string driveName)
            {
                DriveInfo drive = new DriveInfo(driveName);
                Console.WriteLine($"Свободное место на диске {drive.Name} {drive.TotalFreeSpace}");
                Observe("Пользователь использовал класс DASDiskInfo и метод ShowFreeSpace, чтобы узнать свободное место на диске.");

            }
            public static void ShowFileSystem(string driveName)
            {
                DriveInfo drive = new DriveInfo(driveName);
                Console.WriteLine($"Диск {drive.Name} c файловой системой:{drive.DriveFormat}");
                Observe("Пользователь использовал класс DASDiskInfo и метод ShowFileSystem, чтобы узнать файловую систему диска.");
            }


            public static void AllInfo()
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (drive.IsReady)
                    {

                        Console.WriteLine($"Имя: {drive.Name}");
                        Console.WriteLine($"Объём: {drive.TotalSize} байт");
                        Console.WriteLine($"Допустимый обьём:{drive.TotalFreeSpace} байт");
                        Console.WriteLine($"Метка тома: {drive.VolumeLabel}");

                    }
                }
                Observe("Пользователь использовал класс DASDiskInfo и метод AllInfo, чтобы узнать общую информацию про диск.");
            }

        }


        public static class DASFileInfo
        {

            public static void ShowFilePath(string path)
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    Console.WriteLine($"Имя файла: {file.Name}");
                    Console.WriteLine($"Полный путь к файлу: {file.FullName}");
                }
                else
                    Console.WriteLine("Файла по такому пути не существует...");


                Observe("Пользователь использовал класс DASFileInfo и метод ShowFilePath, чтобы узнать имя файла и путь к нему .");
            }

            public static void ShowFileSizeExtAndName(string path)
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    Console.WriteLine($"Имя файла: {file.Name}");
                    Console.WriteLine($"Расширение: {file.Extension}");
                    Console.WriteLine($"Размер: {file.Length} байт");
                }
                else
                    Console.WriteLine("Файла по такому пути не существует...");
                Observe("Пользователь использовал класс DASFileInfo и метод ShowFileSizeExtAndName, чтобы узнать расширение и размер файла.");
            }

            public static void ShowCreationTime(string path)
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    Console.WriteLine($"Имя файла: {file.Name}");
                    Console.WriteLine($"Время создания: {file.CreationTime}");

                }
                else
                    Console.WriteLine("Файла по такому пути не существует...");
                Observe("Пользователь использовал класс DASFileInfo и метод ShowCreationTime, чтобы узнать дату создания файла.");
            }
        }



        public static class DASDirinfo
        {
            public static void ListOfDirectory(string path)
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (directory.Exists)
                {
                    Console.WriteLine($"Папка:{directory.Name}");
                    Console.WriteLine("Подкаталоги:");
                    string[] dirs = Directory.GetDirectories(path);
                    foreach (string s in dirs)
                    {
                        Console.WriteLine(s);
                    }
                }
                else
                    Console.WriteLine("Каталога по такому пути не существует...");

                Observe("Пользователь использовал класс  SAADirinfo и метод ListOfDirectory, чтобы просмотреть подкаталоги.");
            }

            public static void ParentDirectory(string path)
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (directory.Exists)
                {
                    Console.WriteLine($"Папка:{directory.Name}");
                    Console.WriteLine($"Родительский каталог:{directory.Parent}");


                }
                else
                    Console.WriteLine("Каталога по такому пути не существует...");

                Observe("Пользователь использовал класс  DASDirinfo и метод ParentDirectory, чтобы просмотреть родительский каталог.");
            }
            public static void NumberOfFiles(string path)
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (directory.Exists)
                {
                    int number = 0;
                    Console.WriteLine($"Папка:{directory.Name}");
                    Console.WriteLine("Файлы:");
                    string[] files = Directory.GetFiles(path);
                    foreach (string s in files)
                    {
                        Console.WriteLine(s);
                        number++;
                    }
                    Console.WriteLine($"Общее количество файлов в папке:{number}");
                }
                else
                    Console.WriteLine("Каталога по такому пути не существует...");

                Observe("Пользователь использовал класс  DASDirinfo и метод NumberOfFiles, чтобы просмотреть общее количество файлов в папке.");
            }

            public static void CreationTime(string path)
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (directory.Exists)
                {
                    Console.WriteLine($"Папка:{directory.Name}");
                    Console.WriteLine($"Дата создания:{directory.CreationTime}");

                }
                else
                    Console.WriteLine("Каталога по такому пути не существует...");

                Observe("Пользователь использовал класс  DASDirinfo и метод CreationTime, чтобы узнать дату создания папки.");

            }

        }

        public static class DASFileManager
        {
            public static void Task_a()
            {
                string path1 = "d:\\";
                string path2 = "d:\\DasInspect";
                string path3 = "d:\\DasInspect\\Dasdirinfo.txt";
                string copyPath = "d:\\DasInspect\\DasdirinfoCopiedAndRenamed.txt";

                if (Directory.Exists(path1))
                {
                    Console.WriteLine("Каталоги:");
                    string[] dirs = Directory.GetDirectories(path1);
                    foreach (string s in dirs)
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Файлы:");
                    string[] files = Directory.GetFiles(path1);
                    foreach (string s in files)
                    {
                        Console.WriteLine(s);
                    }
                }

                DirectoryInfo newDir = new DirectoryInfo(path2);
                if (!newDir.Exists)
                {
                    newDir.Create();
                    Console.WriteLine("Новая папка успешно создана");
                }



                try
                {
                    string[] dirs = Directory.GetDirectories(path1);
                    string[] files = Directory.GetFiles(path1);
                    using (StreamWriter sw = new StreamWriter(path3, true, Encoding.Default))
                    {
                        sw.WriteLine("Информация по диску D:");
                        sw.WriteLine("Каталоги:");
                        foreach (string s in dirs)
                        {
                            sw.WriteLine(s);
                        }
                        sw.WriteLine("Файлы:");
                        foreach (string s in files)
                        {
                            sw.WriteLine(s);
                        }
                    }
                    Console.WriteLine("Запись выполнена");
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                FileInfo file = new FileInfo(path3);
                if (file.Exists)
                {
                    file.CopyTo(copyPath);
                    file.Delete();
                    Console.WriteLine("Файл скопирован и удалён");
                }

                Observe("Пользователь использовал класс DASFileManager и метод Task_a.");
            }

            public static void Task_b(string path)
            {
                string path1 = "d:\\DasFiles";
                string path2 = "d:\\DasInspect\\DasFiles";
                DirectoryInfo newDir = new DirectoryInfo(path1);
                if (!newDir.Exists)
                {
                    newDir.Create();
                    Console.WriteLine("Новая папка успешно создана");
                }

                DirectoryInfo dir = new DirectoryInfo(path);

                if (dir.Exists)
                {
                    foreach (FileInfo item in dir.GetFiles())
                    {
                        if (item.Name.Contains(".docx"))
                        {
                            item.CopyTo($"d:\\DasFiles\\{item.Name}");
                        }

                    }

                }

                DirectoryInfo directory = new DirectoryInfo(path1);

                if (directory.Exists)
                {
                    directory.MoveTo(path2);
                    Console.WriteLine("Перемещение прошло успешно");
                }
                Observe("Пользователь использовал класс  DASFileManager и метод Task_b.");
            }


            public static void Task_c()
            {

                string path1 = "d:\\DasInspect\\DasFiles";
                string path2 = "d:\\DasInspect\\DasFiles\\compress.gz";
                DirectoryInfo dir = new DirectoryInfo(path1);
                foreach (FileInfo file in dir.GetFiles())
                {

                    // поток для чтения исходного файла
                    using (FileStream sourceStream = new FileStream(file.FullName, FileMode.OpenOrCreate))
                    {
                        // поток для записи сжатого файла
                        using (FileStream targetStream = File.Create(path2))
                        {
                            // поток архивации
                            using (GZipStream gz = new GZipStream(targetStream, CompressionMode.Compress))
                            {
                                sourceStream.CopyTo(gz);
                                Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}.",
                            file.FullName, sourceStream.Length.ToString(), targetStream.Length.ToString());
                            }
                        }
                    }


                }


                string path3 = "d:\\AngelinaEd";
                DirectoryInfo directory = new DirectoryInfo(path1);
                foreach (FileInfo file in directory.GetFiles())
                {
                    if (file.Name.Contains(".docx"))
                    {

                        using (FileStream sourceStream = new FileStream(path2, FileMode.OpenOrCreate))
                        {
                            // поток для записи восстановленного файла
                            using (FileStream targetStream = File.Create($"{path3}\\{file.Name}"))
                            {
                                // поток разархивации
                                using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                                {
                                    decompressionStream.CopyTo(targetStream);
                                    Console.WriteLine("Восстановлен файл: {0}", path2);
                                }
                            }
                        }
                    }
                }

                Observe("Пользователь использовал класс  DASFileManager и метод Task_c.");
            }
        }
    }

}