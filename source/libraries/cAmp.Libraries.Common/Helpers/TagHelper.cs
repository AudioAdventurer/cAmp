using System;
using System.IO;
using cAmp.Libraries.Common.Objects;
using TagLib;

namespace cAmp.Libraries.Common.Helpers
{
    public static class TagHelper
    {
        public static TagInfo BuildTagInfoFromFilename(
            string file,
            bool isFolderOrganizedByArtistAlbum)
        {
            var tagInfo = new TagInfo()
            {
                Artist = "Unknown",
                TrackNumber = 0,
                Year = 0,
                HasTag = false
            };

            // Build tag Info from Filename
            string filename = Path.GetFileNameWithoutExtension(file);

            if (isFolderOrganizedByArtistAlbum)
            {
                string directory = Path.GetDirectoryName(file);
                string[] parts = directory.Split(Path.DirectorySeparatorChar);

                tagInfo.Title = filename;

                if (parts.Length > 2)
                {
                    tagInfo.Album = parts[parts.Length - 1];
                    tagInfo.Artist = parts[parts.Length - 2];
                }
            }
            else
            {
                if (filename.Contains("-"))
                {
                    string[] parts = filename.Split("-");

                    if (parts.Length == 1)
                    {
                        tagInfo.Title = parts[0].Trim();
                    }
                    else if (parts.Length == 2)
                    {
                        tagInfo.Artist = parts[0].Trim();
                        tagInfo.Title = parts[1].Trim();
                    }
                    else
                    {
                        tagInfo.Artist = parts[0].Trim();
                        tagInfo.Album = parts[1].Trim();
                        tagInfo.Title = parts[2].Trim();
                    }
                }
                else
                {
                    tagInfo.Title = filename;
                }
            }

            return tagInfo;
        }

        public static void SaveTag(string file, TagInfo tagInfo)
        {
            var tagFile = TagLib.File.Create(file);

            if (tagFile != null)
            {
                var tag = tagFile.Tag;

                if (tag == null)
                {
                    tag = tagFile.GetTag(TagTypes.Id3v2, true);
                }

                tag.Title = tagInfo.Title;
                tag.AlbumArtists = tagInfo.Artist.Split(",", StringSplitOptions.RemoveEmptyEntries);
                tag.Album = tagInfo.Album;
                tag.Genres = tagInfo.Genre;
                tag.Track = tagInfo.TrackNumber;
                tag.Year = tagInfo.Year;

                tagFile.Save();
            }
        }


        public static TagInfo GetTag(string file)
        {
            var tagFile = TagLib.File.Create(file);

            if (tagFile != null
                && tagFile.Tag != null)
            {
                var tag = tagFile.Tag;

                var tagInfo = new TagInfo()
                {
                    Artist = "Unknown",
                    TrackNumber = 0,
                    Year = 0
                };

                if (tag?.Title != null)
                {
                    tagInfo.Title = tag.Title.Trim();
                    tagInfo.Artist = string.Join(",", tag.AlbumArtists);
                    tagInfo.Album = tag.Album.Trim();
                    tagInfo.Genre = tag.Genres;
                    tagInfo.TrackNumber = tag.Track;
                    tagInfo.Year = tag.Year;

                    if (string.IsNullOrWhiteSpace(tagInfo.Artist))
                    {
                        tagInfo.Artist = string.Join(",", tag.Performers);
                    }

                    return tagInfo;
                }
            }

            return null;
        }

        public static TagInfo GetOrBuildTag(string filename, bool isOrganizedByArtist)
        {
            var tagInfo = GetTag(filename);

            if (tagInfo == null)
            {
                tagInfo = BuildTagInfoFromFilename(filename, isOrganizedByArtist);
            }

            return tagInfo;
        }
    }
}
