using System.IO;
using System.Text;

namespace NugetReferenceUpdater.Classes
{
    public class Converter
    {
        public static Is Inspect(FileInfo file)
        {
            var convertableFile = new ConvertableFile();
            var action = Is.Inspected;

            string line;
            var stream = new StreamReader(file.FullName);
            var changes = false;
            while ((line = stream.ReadLine()) != null)
            {
                if (!line.ToLower().Contains(Options.Current.PackageName.ToLower()))
                {
                    convertableFile.AppendLine(line);
                }
                else if (line.Contains(Options.Current.OldVersion))
                {
                    convertableFile.AppendTransformedLine(line);
                    changes = true;
                    action = Is.Updated;
                }
                else
                {
                    convertableFile.AppendLine(line);
                }
            }
            
            stream.Close();

            if (changes)
            {
                convertableFile.OverWrite(file);
            }

            return action;
        }
    }
}
