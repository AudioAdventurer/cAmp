using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Logging;
using cAmp.Libraries.Common.Managers;
using cAmp.Libraries.Common.Objects;
using cAmp.Libraries.Common.Services;

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

            builder.RegisterType<FileService>();

        }
    }
}
