using System;
using System.IO;
using cAmp.Libraries.Common.Helpers;
using cAmp.Utility.Renamer.Managers;
using cAmp.Utility.Renamer.Objects;

namespace cAmp.Server.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var action = args[0];
            
            if (action.Equals("audit", StringComparison.InvariantCultureIgnoreCase))
            {
                var source = args[1];
                var destination = args[2];
                var changeFile = args[3];

                AuditManager am = new AuditManager();
                var changes = am.Audit(source, destination);
                var json = JsonHelper.Serialize(changes, true);
                File.WriteAllText(changeFile, json);
            }
            else if (action.Equals("execute", StringComparison.InvariantCultureIgnoreCase))
            {
                var changeFile = args[1];

                var json = File.ReadAllText(changeFile);
                var changes = JsonHelper.Deserialize<ChangeFile>(json);

                ExecuteManager em = new ExecuteManager();
                var log = em.ExecuteChangePlan(changes);

                var directory =Path.GetDirectoryName(changeFile);
                string logFile = Path.GetFileNameWithoutExtension(changeFile) + ".log";
                File.WriteAllText(Path.Combine(directory, logFile), log);
            }
        }
    }
}