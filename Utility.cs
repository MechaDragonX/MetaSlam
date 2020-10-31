using System;
using System.Collections.Generic;
using System.Text;

namespace MetaSlam
{
    class Utility
    {
        private static readonly char[] illegalChar = { '<', '>', ':', '"', '/', '\\', '|', '?', '*' };

        /// <summary>
        /// Check the filename for illegal characters and the replace them with legal characters or remove them
        /// </summary>
        /// <param name="name">Filename Without Extension</param>
        /// <returns></returns>
        public static string CheckFixFilename(string name)
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
