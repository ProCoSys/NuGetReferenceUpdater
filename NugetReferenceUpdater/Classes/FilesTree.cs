﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NugetReferenceUpdater.Classes
{
    public class FileTree
    {
        private int _filesInspected;
        private int _filesUpdated;

        public DirectoryInfo BaseOfFileTree
        {
            get { return new DirectoryInfo(Directory.GetCurrentDirectory()); }
        }

        public FileTree ParseFiles()
        {
            ParseRecursive(BaseOfFileTree);

            return this;
        }

        private void ParseRecursive(DirectoryInfo directory)
        {
            var files = directory.EnumerateFiles("*.*")
                .Where(x => x.Name.ToLower() == "packages.config" || x.Extension == ".csproj");

            foreach (var file in files)
            {
                try
                {
                    var result = Converter.Inspect(file);
                    WriteToConsole(file, result);
                    if (result == Is.Updated)
                    {
                        _filesUpdated++;
                    }

                    _filesInspected++;
                }
                catch (Exception e)
                {
                    WriteToConsole(file, Is.Failed, e);
                }
            }

            Parallel.ForEach(directory.EnumerateDirectories(), ParseRecursive);
        }

        private void WriteToConsole(FileInfo file, Is action)
        {
            Console.WriteLine(action + ": " + file.Name + " under " + new PathTo(file).RelativeTo(BaseOfFileTree));
            Console.WriteLine();
        }

        private void WriteToConsole(FileInfo file, Is action, Exception e)
        {
            WriteToConsole(file, action);
            Console.WriteLine(e.Message);
            Console.WriteLine();
        }

        public FileTree Then()
        {
            return this;
        }

        public void WriteHowManyFilesWereModified()
        {
            Console.WriteLine(_filesInspected + " file(s) inspected");
            Console.WriteLine(_filesUpdated + " file(s) updated");
            Console.ReadLine();
        }
    }
}