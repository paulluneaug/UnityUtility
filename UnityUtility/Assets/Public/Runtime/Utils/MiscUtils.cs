using System.IO;

namespace UnityUtility.Utils
{
    public static class MiscUtils
    {
        /// <summary>
        /// Creates a directory at the given <paramref name="path"/> if it doesn't exist already
        /// </summary>
        /// <param name="path">The path of the directory</param>
        public static void CreateDirectoryIfNeeded(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
