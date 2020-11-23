using System.IO;
using Autofac;
using cAmp.Libraries.Common.Controllers;
using cAmp.Libraries.Common.Helpers;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Logging;
using cAmp.Libraries.Common.Managers;
using cAmp.Libraries.Common.Objects;
using cAmp.Libraries.Common.Repos;
using cAmp.Libraries.Common.Services;
using LiteDB;

namespace cAmp.Libraries.Common.Modules
{
    public class cAmpModule : Module
    {
        private readonly Config _config;

        public cAmpModule(Config config)
        {
            _config = config;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLogger>()
                .As<IcAmpLogger>();

            builder.RegisterType<MediaManager>()
                .WithParameter("musicFolder", _config.MusicFolder);

            builder.Register(c =>
                {
                    var mediaManager = c.Resolve<MediaManager>();
                    return mediaManager.LoadLibrary();
                }).As<Library>()
                .SingleInstance();

            //Register Services
            builder.RegisterType<AuthService>();
            builder.RegisterType<FileService>();
            builder.RegisterType<JwtService>()
                .WithParameter("jwtSecret", _config.JwtSecret);
            builder.RegisterType<LibraryService>();
            builder.RegisterType<UserService>();
            

            //Register Controllers
            builder.RegisterType<ArtistController>();
            builder.RegisterType<AlbumController>();
            builder.RegisterType<AuthController>();
            builder.RegisterType<GenreController>();
            builder.RegisterType<QueueController>();
            builder.RegisterType<SoundFileController>();
            builder.RegisterType<StatusController>();
            builder.RegisterType<UserController>();

            //Register LiteDB
            builder.Register<LiteDatabase>(c =>
                {
                    DirectoryHelper.EnsureDirectory(_config.DbFolder);
                    string dbName = Path.Combine(_config.DbFolder, "MusicData.db");
                    var db = new LiteDatabase(dbName);
                    return db;
                }).As<LiteDatabase>()
                .SingleInstance();

            //Register Repos
            builder.RegisterType<PlayHistoryRepo>();
            builder.RegisterType<UserRepo>();
        }
    }
}
