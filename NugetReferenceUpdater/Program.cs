using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NugetReferenceUpdater.Classes;

namespace NugetReferenceUpdater
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (!Options.ReadFrom(args).MisingArguments)
            {
                new FileTree()
                    .ParseFiles()
                    .Then()
                    .WriteHowManyFilesWereModified();
            }
        }
    }
}
