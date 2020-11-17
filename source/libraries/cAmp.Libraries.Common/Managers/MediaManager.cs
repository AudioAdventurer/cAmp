using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using cAmp.Libraries.Common.Objects;
using Id3;

namespace cAmp.Libraries.Common.Managers
{
    public class MediaManager
    {
        private readonly string _musicFolder;

        public MediaManager(
            string musicFolder)
        {
            _musicFolder = musicFolder;
        }

        public Library LoadLibrary()
        {
            return LoadLibraryFromFolder(_musicFolder);
        }

        private Library LoadLibraryFromFolder(string musicFolder)
        {
            Library output = new Library();

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
                    uint? trackNumber = null;

                    if (tag != null)
                    {
                        title = tag.Title;
                        artist = string.Join(",", tag.Performers);
                        album = tag.Album;
                        genre = tag.Genres;
                        trackNumber = tag.Track;
                    }
                    else
                    {
                        string filename = Path.GetFileNameWithoutExtension(file);

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

                    Artist artistObject;
                    if (!output.ContainsArtist(artist))
                    {
                        artistObject = new Artist { Name = artist };

                        output.Add(artistObject);
                    }
                    else
                    {
                        artistObject = output.GetArtist(artist);
                    }

                    Album albumObject = null;
                    if (album != null)
                    {
                        albumObject = artistObject.Albums.FirstOrDefault(a => a.Name == album);

                        if (albumObject == null)
                        {
                            albumObject = new Album { Name = album, Artist = artistObject };

                            artistObject.Albums.Add(albumObject);
                            output.Add(albumObject);
                        }
                    }

                    SoundFile soundFile = new SoundFile
                    {
                        Album = albumObject,
                        Artist = artistObject,
                        Filename = file,
                        Title = title,
                    };

                    if (genre.Length > 0)
                    {
                        soundFile.Genre.AddRange(genre);
                    }
                    
                    output.Add(soundFile);

                    albumObject?.Songs.Add(soundFile);
                }
            }

            return output;
        }

        
    }
}
