using ATL;
using System;
using System.IO;

namespace MetaSlam
{
    class Program
    {
        private static char[] illegalChar = { '<', '>', ':', '"', '/', '\\', '|', '?', '*' };

        static void Main(string[] args)
        {

        }
        /// <summary>
        /// Name all audio files in a directory based on the file's metadata
        /// </summary>
        /// <param name="path">Path to File</param>
        private static void NameAudioDirectory(string path)
        {
            foreach(string file in Directory.EnumerateFiles(path))
                NameAudioFile(file);
        }
        /// <summary>
        /// Name Audio File Based on File's Metadata
        /// </summary>
        /// <param name="path">Path to File</param>
        private static void NameAudioFile(string path)
        {
            string newPath = path.Substring(0, path.Length - Path.GetFileName(path).Length);
            Track track = new Track(path);
            string name = "";
            string extension = Path.GetExtension(path);

            if(track.TrackNumber < 10)
                name += $"0{track.TrackNumber} - ";
            else
                name += $"{track.TrackNumber} - ";
            name += track.Title;
            if(track.Artist != track.AlbumArtist)
                name += $" - {track.Artist}";
            name = checkFixFilename(name);
            name += extension;
            newPath += name;

            File.Move(path, newPath);
        }
        /// <summary>
        /// Check the filename for illegal characters and the replace them with legal characters or remove them
        /// </summary>
        /// <param name="name">Filename Without Extension</param>
        /// <returns></returns>
        private static string checkFixFilename(string name)
        {
            for(int i = 0; i < illegalChar.Length; i++)
            {
                if(name.Contains(illegalChar[i]))
                {
                    switch(illegalChar[i])
                    {
                        case '<':
                        case '>':
                        case '/':
                        case '\\':
                        case '|':
                        case '*':
                            name = name.Replace(illegalChar[i], '-');
                            break;
                        case '"':
                            name = name.Replace(illegalChar[i], '\'');
                            break;
                        case '?':
                            name = name.Remove(name.IndexOf(illegalChar[i]), 1);
                            break;
                    }
                }
            }
            return name;
        }
    }
}
