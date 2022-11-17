using System;
using System.IO;
using System.Text;
using cAmp.Utility.Renamer.Objects;

namespace cAmp.Utility.Renamer.Managers
{
    public class ExecuteManager
    {
        public string ExecuteChangePlan(ChangeFile file)
        {
            StringBuilder sb = new StringBuilder();
            string message;

            foreach (var change in file.Changes)
            {
                try
                {
                    var directory = Path.GetDirectoryName(change.NewFileName);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    if (!File.Exists(change.NewFileName))
                    {
                        File.Copy(change.OldFileName, change.NewFileName);

                        if (change.OldTag != null)
                        {
                            message = $"Info - Copied {change.OldTag.Title}";
                        }
                        else
                        {
                            message = $"Info - Copied {Path.GetFileName(change.NewFileName)}";
                        }
                    }
                    else
                    {
                        if (change.OldTag != null)
                        {
                            message = $"Skipped ({change.OldTag.Title})";
                        }
                        else
                        {
                            message = $"Skipped ({change.NewFileName})";
                        }
                    }
                }
                catch (Exception ex)
                {
                    message = $"Exc - {ex.Message}";
                }

                sb.AppendLine(message);
                Console.WriteLine(message);
            }

            return sb.ToString();
        }
    }
}
