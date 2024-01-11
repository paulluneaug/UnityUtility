using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using UnityEditor;
using UnityEngine;

public class SampleHideAndShow : MonoBehaviour
{
    private const string SHOWN_SAMPLES_DIR = "Samples";
    private const string HIDDEN_SAMPLES_DIR = "Samples~";

    [MenuItem("Tools/Hide\\Show Samples")]
    public static void HideShowSamples()
    {
        IEnumerable<string> assetFolderDirectories = Directory.EnumerateDirectories(Application.dataPath, "*", SearchOption.TopDirectoryOnly).Select(path => path.Split('\\').Last()).ToList();
        if (assetFolderDirectories.Contains(SHOWN_SAMPLES_DIR))
        {
            Directory.Move(Path.Join(Application.dataPath, SHOWN_SAMPLES_DIR), Path.Join(Application.dataPath, HIDDEN_SAMPLES_DIR));
        }
        else if (assetFolderDirectories.Contains(HIDDEN_SAMPLES_DIR))
        {
            Directory.Move(Path.Join(Application.dataPath, HIDDEN_SAMPLES_DIR), Path.Join(Application.dataPath, SHOWN_SAMPLES_DIR));
        }
    }
}