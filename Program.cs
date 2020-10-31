using ATL;
using System;
using System.IO;

namespace MetaSlam
{
    class Program
    {
        static void Main(string[] args)
        {
        }
		/// <summary>
        /// Name All Audio Files in a Directory Based on the Fiel's Metadata
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
            name += extension;
            newPath += name;

            File.Move(path, newPath);
        }
    }
}
