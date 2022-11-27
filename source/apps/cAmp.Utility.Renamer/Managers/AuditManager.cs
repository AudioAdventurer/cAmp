using System.IO;
using cAmp.Libraries.Common.Helpers;
using cAmp.Libraries.Common.Objects;
using cAmp.Utility.Renamer.Objects;

namespace cAmp.Utility.Renamer.Managers
{
    public class AuditManager
    {
        public ChangeFile Audit(
            string source,
            string destination)
        {
            var changePlan = new ChangeFile
            {
                Source = source,
                Destination = destination
            };

            var files = Directory.GetFiles(source, "*.mp3", SearchOption.AllDirectories);

            foreach (string sourceFile in files)
            {
                bool hasTag = true;

                var tagInfo = TagHelper.GetTag(sourceFile);
                if (tagInfo == null)
                {
                    tagInfo = TagHelper.BuildTagInfoFromFilename(sourceFile, true);
                    hasTag = false;
                }

                var change = new Change
                {
                    OldFileName = sourceFile,
                    NewFileName = GetNewFilename(destination, tagInfo)
                };

                if (hasTag)
                {
                    change.OldTag = tagInfo;
                }
                else
                {
                    change.NewTag = tagInfo;
                }

                changePlan.Changes.Add(change);
            }

            return changePlan;
        }

        private string GetNewFilename(string destination, TagInfo tagInfo)
        {
            string artistFolder = tagInfo.Artist;
            string albumFolder = tagInfo.Album;

            string destinationPath = Path.Combine(artistFolder, albumFolder);
            destinationPath = ReplaceBadChars(destinationPath);

            destinationPath = Path.Combine(destination, destinationPath);
            
            string destinationFile = tagInfo.Title.Trim();
            destinationFile = ReplaceBadChars(destinationFile);

            for (int i = 0; i < 24; i++)
            {
                string prefix = i.ToString("00") + " -";

                if (destinationFile.StartsWith(prefix))
                {
                    destinationFile = destinationFile.Substring(prefix.Length);
                }

                prefix = i.ToString("00") + " ";

                if (destinationFile.StartsWith(prefix))
                {
                    destinationFile = destinationFile.Substring(prefix.Length);
                }

                prefix = "- " + i.ToString("00") + " -";

                destinationFile = destinationFile
                    .Replace(prefix, "");
            }

            destinationFile = destinationFile
                .Replace("  ", " ")
                .Replace(" - - ", " ")
                .Trim();

            string target = Path.Combine(destinationPath, destinationFile);
            var output = target + ".mp3";

            return output.ToLower();
        }

        private string ReplaceBadChars(string source)
        {
            var destination = source
                .Replace("(", "")
                .Replace(")", "")
                .Replace(",", "")
                .Replace("'", "")
                .Replace(":", "")
                .Replace(".", "")
                .Replace("`", "")
                .Replace("#", "")
                .Replace("*", "")
                .Replace("[", "")
                .Replace("]", "")
                .Replace("!", "")
                .Replace("?", "")
                .Replace("/", "")
                .Replace("\"", "")
                .Replace("&", "and")
                .Replace("$", "");

            return destination;
        }
    }
}
