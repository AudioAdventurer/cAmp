using System;
using System.IO;
using cAmp.Libraries.Common.Security;

namespace cAmp.Libraries.Common.Objects
{
    public class Config
    {
        private static Config _config;

        public string JwtSecret { get; set; }
        public string LocalDataFolder { get; set; }
        public int PortNumber { get; set; }
        public string MusicFolder { get; set; }

        public string DbFolder => Path.Combine(_config.LocalDataFolder, "database");
        
        public static Config GetInstance()
        {
            if (_config == null)
            {
                _config = LoadConfig();
            }

            return _config;
        }

        private static Config LoadConfig()
        {
            string musicFolder = Environment.GetEnvironmentVariable("CAMP_MUSIC_FOLDER");
            string localFolder = Environment.GetEnvironmentVariable("CAMP_DATA_FOLDER");
            string jwtSecret = Environment.GetEnvironmentVariable("CAMP_JWT_SECRET");

            if (string.IsNullOrEmpty(jwtSecret))
            {
                string jwtFile = Path.Combine(localFolder, "jwt.txt");

                if (File.Exists(jwtFile))
                {
                    jwtSecret = File.ReadAllText(jwtFile);
                }
                else
                {
                    jwtSecret = JwtHelper.CreateJwtSecret();
                    File.WriteAllText(jwtFile, jwtSecret);
                }
            }

            var config = new Config
            {
                JwtSecret = jwtSecret,
                LocalDataFolder = localFolder,
                MusicFolder = musicFolder,
                PortNumber = 8000
            };

            string temp = Environment.GetEnvironmentVariable("CAMP_PORT_NUMBER");
            if (!String.IsNullOrEmpty(temp))
            {
                if (int.TryParse(temp, out int result))
                {
                    config.PortNumber = result;
                }
            }

            return config;
        }
    }
}
