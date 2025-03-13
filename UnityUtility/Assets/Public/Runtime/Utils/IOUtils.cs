using System.IO;
using UnityUtility.Extensions;

namespace UnityUtility.Utils
{
    public static class IOUtils
    {
        /// <summary>
        /// Creates a directory at the given <paramref name="path"/> if it doesn't exist already
        /// </summary>
        /// <param name="path">The path of the directory</param>
        public static void CreateDirectoryIfNeeded(string path)
        {
            if (!Directory.Exists(path))
            {
                _ = Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Tries to move a directory from <paramref name="currentPath"/> to <paramref name="newPath"/>
        /// </summary>
        /// <returns>Wether the directory was successfully moved</returns>
        public static bool TryMoveDirectory(string currentPath, string newPath)
        {
            if (!Directory.Exists(currentPath) || Directory.Exists(newPath))
            {
                return false;
            }
            Directory.Move(currentPath, newPath);
            return true;
        }

        /// <summary>
        /// Tries to rename the directory at <paramref name="path"/> to <paramref name="newName"/>
        /// </summary>
        /// <returns>Wether the directory was successfully renamed</returns>
        public static bool TryRenameDirectory(string path, string newName)
        {
            if (!Directory.Exists(path))
            {
                return false;
            }
            string newPath = path.ReplaceLast(Path.GetFileName(path), newName);
            return TryMoveDirectory(path, newPath);
        }
    }
}
