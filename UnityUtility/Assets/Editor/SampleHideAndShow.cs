using System.IO;
using UnityEditor;
using UnityEngine;
using UnityUtility.Utils;

public class SampleHideAndShow
{
    private const string SAMPLE_RELATIVE_PATH = "Public";
    private const string SHOWN_SAMPLES_DIR = "Samples";
    private const string HIDDEN_SAMPLES_DIR = "Samples~";
    private const string META_EXTENSION = ".meta";

    [MenuItem("Tools/Toggle Samples")]
    public static void HideShowSamples()
    {
        string containingDirPath = Path.Combine(Application.dataPath, SAMPLE_RELATIVE_PATH);
        if (!IOUtils.TryRenameDirectory(Path.Combine(containingDirPath, SHOWN_SAMPLES_DIR), HIDDEN_SAMPLES_DIR))
        {
            if (!IOUtils.TryRenameDirectory(Path.Combine(containingDirPath, HIDDEN_SAMPLES_DIR), SHOWN_SAMPLES_DIR))
            {
                IOUtils.CreateDirectoryIfNeeded(Path.Combine(containingDirPath, SHOWN_SAMPLES_DIR));
            }
            else
            {
                File.Delete(Path.Combine(containingDirPath, HIDDEN_SAMPLES_DIR + META_EXTENSION));
            }
        }
        else
        {
            File.Delete(Path.Combine(containingDirPath, SHOWN_SAMPLES_DIR + META_EXTENSION));
        }
    }
}