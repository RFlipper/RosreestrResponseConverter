using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosreestrResponseConverter
{
    public static class Tools
    {
        /// <summary>
        /// Возвращает полный путь к файлу, отбрасывая расширение файла
        /// </summary>
        /// <param name="path"> путь к файлу</param>
        /// <returns></returns>
        public static string GetFilePathWithoutExtention(string path)
        {
            int i = path.LastIndexOf('.');
            return path.Substring(0, i);
        }

        /// <summary>
        /// Возвращает путь к файлу без его имени
        /// </summary>
        /// <param name="path"> путь к файлу</param>
        /// <returns></returns>
        public static string GetFilePath(string path)
        {
            int start = path.LastIndexOf('\\') + 1;
            return path.Substring(0, start);
        }
    }
}
