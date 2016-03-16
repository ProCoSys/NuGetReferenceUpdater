using System.IO;
using System.Text;

namespace NugetReferenceUpdater.Classes
{
    public class Converter
    {
        public static Is Inspect(FileInfo file)
        {
            var packageFile = new PackageFile();
            var action = Is.Inspected;

            string line;
            var stream = new StreamReader(file.FullName);
            var changes = false;
            while ((line = stream.ReadLine()) != null)
            {
                if (!line.Contains(Options.Current.PackageName))
                {
                    packageFile.AppendLine(line);
                }
                else if(line.Contains(Options.Current.OldVersion))
                {
                    packageFile.AppendTransformedLine(line);
                    changes = true;
                    action = Is.Updated;
                }
            }

            stream.Close();

            if (changes)
            {
                packageFile.OverWrite(file);
            }

            return action;
        }
    }
}
