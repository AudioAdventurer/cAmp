using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
                .GetFiles(_musicFolder, "*.mp3", SearchOption.AllDirectories)
                .ToList();

            foreach (var file in files)
            {
                var tagFile = TagLib.File.Create(file);

                if (tagFile != null
                    && tagFile.Tag != null)
                {
                    var tag = tagFile.Tag;

                    string title = null;
                    string artist = "Unknown";
                    string album = null;
                    string[] genre = null;
                    uint trackNumber = 0;
                    uint year = 0;

                    if (tag?.Title != null)
                    {
                        title = tag.Title.Trim();
                        artist = string.Join(",", tag.Performers);
                        album = tag.Album.Trim();
                        genre = tag.Genres;
                        trackNumber = tag.Track;
                        year = tag.Year;
                    }
                    else
                    {
                        string filename = Path.GetFileNameWithoutExtension(file);

                        if (filename.Contains("-"))
                        {
                            string[] parts = filename.Split("-");

                            if (parts.Length == 1)
                            {
                                title = parts[0].Trim();
                            }
                            else if (parts.Length == 2)
                            {
                                artist = parts[0].Trim();
                                title = parts[1].Trim();
                            }
                            else
                            {
                                artist = parts[0].Trim();
                                album = parts[1].Trim();
                                title = parts[2].Trim();
                            }
                        }
                        else
                        {
                            string directory = Path.GetDirectoryName(file);
                            string[] parts = directory.Split(Path.DirectorySeparatorChar);

                            title = filename;

                            if (parts.Length > 2)
                            {
                                album = parts[parts.Length - 1];
                                artist = parts[parts.Length - 2];
                            }
                        }
                    }

                    Artist artistObject;
                    if (!library.ContainsArtist(artist))
                    {
                        artistObject = new Artist { Name = artist };

                        library.Add(artistObject);
                    }
                    else
                    {
                        artistObject = library.GetArtistByName(artist);
                    }

                    Album albumObject = null;
                    if (album != null)
                    {
                        string folder = Path.GetDirectoryName(file).Trim();

                        albumObject = library.Albums.FirstOrDefault(a => a.Name.Equals(album)
                                                                         && a.Folder.Equals(folder));

                        if (albumObject == null)
                        {
                            albumObject = new Album
                            {
                                Name = album, 
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
                        Title = title,
                        TrackNumber = trackNumber,
                        Year = year
                    };

                    if (genre?.Length > 0)
                    {
                        soundFile.Genre.AddRange(genre);
                    }

                    library.Add(soundFile);

                    albumObject?.Songs.Add(soundFile);
                }
            }

            _logger.Info("Completed library load");            
        }
    }
}
