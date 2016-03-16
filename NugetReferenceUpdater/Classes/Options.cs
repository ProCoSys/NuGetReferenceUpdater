using System;

namespace NugetReferenceUpdater.Classes
{
    public class Options
    {
        private static volatile Options _singleTonObject;
        public string PackageName { get; set; }
        public string OldVersion { get; set; }
        public string NewVersion { get; set; }

        public bool MisingArguments { get; set; }

        public static Options ReadFrom(string[] args)
        {
            var arguments = Current;

            if (args.Length != 3)
            {
                Console.WriteLine("Missing arguments");
                Console.ReadLine();
                arguments.MisingArguments = true;
            }
            else { 
                arguments.PackageName = args[0];
                arguments.OldVersion = args[1];
                arguments.NewVersion = args[2];
            }
            return arguments;
        }

        public static Options Current
        {
            get
            {
                var lockingObject = new object();
                if (_singleTonObject == null)
                {
                    lock (lockingObject)
                    {
                        if (_singleTonObject == null)
                        {
                            _singleTonObject = new Options();
                        }
                    }
                }

                return _singleTonObject;
            }
        }
    }
}
