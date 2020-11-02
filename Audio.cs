using ATL;
using System.IO;

namespace MetaSlam
{
    class Audio
    {
        /// <summary>
        /// Name all audio files in a directory based on the file's metadata
        /// </summary>
        /// <param name="path">Path to Directory</param>
        public static void NameDirectory(string path)
        {
            foreach(string file in Directory.EnumerateFiles(path))
            {
                if(Path.GetFileNameWithoutExtension(file) != "folder")
                    NameFile(file);
            }
        }
        /// <summary>
        /// Name audio file based on file's metadata
        /// </summary>
        /// <param name="path">Path to File</param>
        private static void NameFile(string path)
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
            name = Utility.CheckFixFilename(name);
            name += extension;
            newPath += name;

            File.Move(path, newPath);
        }
    }
}
