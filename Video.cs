using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MetaSlam
{
    class Video
    {
        /// <summary>
        /// Name all video files in a directory using the provided metadata File
        /// </summary>
        /// <param name="dirPath">Path to Directory</param>
        /// <param name="dataPath">Path to Metadata File</param>
        public static async void NameDirectory(string dirPath, string dataPath)
        {
            List<string> validPaths = new List<string>();
            foreach(string file in Directory.EnumerateFiles(dirPath))
            {
                if(Path.GetExtension(file) != ".txt")
                    validPaths.Add(file);
            }
            string[] text = await File.ReadAllLinesAsync(dataPath);
            if (text[2].Split('\t').Length == 2)
                WriteSingleSeason(validPaths.ToArray(), text);
            else if (text[2].Split('\t').Length == 3)
                WriteMultiSeason(validPaths.ToArray(), text);
            else
                throw new Exception("The metadata file has improper format! Please check the \"readme.md\" on the GitHub repository for more details.");
        }
        private static void WriteSingleSeason(string[] paths, string[] text)
        {
            string[] mainMeta = new string[text.Length - 1];
            Array.Copy(text, 1, mainMeta, 0, text.Length - 1);
            string[] currentLine;

            string seriesTitle = text[0];
            string currentFilename = "";

            for (int i = 1; i < text.Length; i++)
            {
                currentLine = text[i].Split('\t');
                currentFilename = "";

                currentFilename += paths[i - 1].Substring(0, paths[i - 1].Length - Path.GetFileName(paths[i - 1]).Length);
                if(int.Parse(currentLine[0]) < 10)
                    currentFilename += $"{seriesTitle} 0{currentLine[0]} - {currentLine[1]}";
                else
                    currentFilename += $"{seriesTitle} {currentLine[0]} - {currentLine[1]}";
                currentFilename += Path.GetExtension(paths[i - 1]);

                File.Move(paths[i - 1], currentFilename);
            }
        }
        private static void WriteMultiSeason(string[] paths, string[] text)
        {
            string[] mainMeta = new string[text.Length - 1];
            Array.Copy(text, 1, mainMeta, 0, text.Length - 1);
            string[] currentLine;

            string seriesTitle = text[0];
            string currentFilename = "";

            for (int i = 1; i < text.Length; i++)
            {
                currentLine = text[i].Split('\t');
                currentFilename = "";

                currentFilename += paths[i - 1].Substring(0, paths[i - 1].Length - Path.GetFileName(paths[i - 1]).Length);
                if(int.Parse(currentLine[0]) < 10)
                    currentFilename += $"{seriesTitle} S0{currentLine[0]}";
                else
                    currentFilename += $"{seriesTitle} S{currentLine[0]}";
                if(int.Parse(currentLine[1]) < 10)
                    currentFilename += $"E0{currentLine[1]} - {currentLine[2]}";
                else
                    currentFilename += $"E{currentLine[1]} - {currentLine[2]}";
                currentFilename += Path.GetExtension(paths[i - 1]);

                File.Move(paths[i - 1], currentFilename);
            }
        }
    }
}
