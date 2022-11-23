using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using cAmp.Libraries.Common.Helpers;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Objects;
using cAmp.Libraries.Common.Records;

namespace cAmp.Libraries.Common.Managers
{
    public class MediaManager
    {
        private readonly string _musicFolder;
        private readonly IcAmpLogger _logger;

        public MediaManager(
            string musicFolder,
            IcAmpLogger logger)
        {
            _musicFolder = musicFolder;
            _logger = logger;
        }

        public Library LoadLibrary()
        {
            Library output = new Library();

            Task.Run(() =>
            {
                PopulateLibraryFromFolder(output, _musicFolder);
            });

            return output;
        }

        private void PopulateLibraryFromFolder(Library library, string musicFolder)
        {
            _logger.Info("Starting library load");
            
            List<string> files = Directory
                .GetFiles(musicFolder, "*.mp3", SearchOption.AllDirectories)
                .ToList();

            foreach (var file in files)
            {
                try
                {
                    var tagInfo = TagHelper.GetOrBuildTag(file, true);

                    if (tagInfo != null)
                    {
                        Artist artistObject;
                        if (!library.ContainsArtist(tagInfo.Artist))
                        {
                            artistObject = new Artist { Name = tagInfo.Artist };

                            library.Add(artistObject);
                        }
                        else
                        {
                            artistObject = library.GetArtistByName(tagInfo.Artist);
                        }

                        Album albumObject = null;
                        if (tagInfo.Album != null)
                        {
                            string folder = Path.GetDirectoryName(file).Trim();

                            albumObject = library.Albums.FirstOrDefault(a => a.Name.Equals(tagInfo.Album)
                                                                             && a.Folder.Equals(folder));

                            if (albumObject == null)
                            {
                                albumObject = new Album
                                {
                                    Name = tagInfo.Album,
                                    Artist = artistObject,
                                    Folder = folder
                                };

                                artistObject.Albums.Add(albumObject);
                                library.Add(albumObject);
                            }
                        }

                        SoundFile soundFile = new SoundFile
                        {
                            Album = albumObject,
                            Artist = artistObject,
                            Filename = file,
                            Title = tagInfo.Title,
                            TrackNumber = tagInfo.TrackNumber,
                            Year = tagInfo.Year
                        };

                        if (tagInfo.Genre?.Length > 0)
                        {
                            soundFile.Genre.AddRange(tagInfo.Genre);
                        }

                        library.Add(soundFile);

                        albumObject?.Songs.Add(soundFile);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                    _logger.Warning($"Unable to process file ({file})");
                }                
            }

            _logger.Info("Completed library load");
            _logger.Info($"  - {library.Artists.Count} Artists, {library.Albums.Count} Albums, {library.SoundFiles.Count} Songs");
        }
    }
}
