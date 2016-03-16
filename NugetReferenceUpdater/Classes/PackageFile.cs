using System.IO;
using System.Text;

namespace NugetReferenceUpdater.Classes
{
    public class PackageFile
    {
        private StringBuilder sb = new StringBuilder();

        public void AppendLine(string line)
        {
            sb.AppendLine(line);
        }

        public void AppendTransformedLine(string line)
        {
            sb.AppendLine(line.Replace(Options.Current.OldVersion, Options.Current.NewVersion));
        }

        public void OverWrite(FileInfo file)
        {
            File.WriteAllText(file.FullName, sb.ToString(), Encoding.UTF8);
        }
    }
}
