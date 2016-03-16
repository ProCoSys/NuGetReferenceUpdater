using System.IO;

namespace NugetReferenceUpdater.Classes
{
    public class PathTo
    {
        private readonly FileInfo _file;
        public PathTo(FileInfo file)
        {
            _file = file;
        }

        public string RelativeTo(DirectoryInfo directory)
        {
            return _file.Directory.FullName.Substring(directory.FullName.Length);
        }
    }
}
