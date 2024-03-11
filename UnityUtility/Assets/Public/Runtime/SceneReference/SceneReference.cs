// Author: JohannesMP (2018-08-12)
// https://gist.github.com/JohannesMP/ec7d3f0bcf167dab3d0d3bb480e0e07b

using UnityEditor;
using UnityEngine;

namespace UnityUtility.SceneReference
{
    /// <summary>
    /// A wrapper that provides the means to safely serialize Scene Asset References.
    /// </summary>
    [System.Serializable]
    public class SceneReference : ISerializationCallbackReceiver
    {
#if UNITY_EDITOR
        // What we use in editor to select the scene
        [SerializeField] private Object m_sceneAsset = null;
        bool IsValidSceneAsset
        {
            get
            {
                if (m_sceneAsset == null)
                {
                    return false;
                }
                return m_sceneAsset.GetType().Equals(typeof(SceneAsset));
            }
        }
#endif

        // This should only ever be set during serialization/deserialization!
        [SerializeField]
        private string m_scenePath = string.Empty;

        // Use this when you want to actually have the scene path
        public string ScenePath
        {
            get
            {
#if UNITY_EDITOR
                // In editor we always use the asset's path
                return GetScenePathFromAsset();
#else
            // At runtime we rely on the stored path value which we assume was serialized correctly at build time.
            // See OnBeforeSerialize and OnAfterDeserialize
            return m_scenePath;
#endif
            }
            set
            {
                m_scenePath = value;
#if UNITY_EDITOR
                m_sceneAsset = GetSceneAssetFromPath();
#endif
            }
        }

        public static implicit operator string(SceneReference sceneReference)
        {
            return sceneReference.ScenePath;
        }

        // Called to prepare this data for serialization. Stubbed out when not in editor.
        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            HandleBeforeSerialize();
#endif
        }

        // Called to set up data for deserialization. Stubbed out when not in editor.
        public void OnAfterDeserialize()
        {
#if UNITY_EDITOR
            // We sadly cannot touch assetdatabase during serialization, so defer by a bit.
            EditorApplication.update += HandleAfterDeserialize;
#endif
        }



#if UNITY_EDITOR
        private SceneAsset GetSceneAssetFromPath()
        {
            if (string.IsNullOrEmpty(m_scenePath))
            {
                return null;
            }
            return AssetDatabase.LoadAssetAtPath<SceneAsset>(m_scenePath);
        }

        private string GetScenePathFromAsset()
        {
            if (m_sceneAsset == null)
            {
                return string.Empty;
            }
            return AssetDatabase.GetAssetPath(m_sceneAsset);
        }

        private void HandleBeforeSerialize()
        {
            // Asset is invalid but have Path to try and recover from
            if (!IsValidSceneAsset && !string.IsNullOrEmpty(m_scenePath))
            {
                m_sceneAsset = GetSceneAssetFromPath();
                if (m_sceneAsset == null)
                {
                    m_scenePath = string.Empty;
                }

                UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
            }
            // Asset takes precendence and overwrites Path
            else
            {
                m_scenePath = GetScenePathFromAsset();
            }
        }

        private void HandleAfterDeserialize()
        {
            EditorApplication.update -= HandleAfterDeserialize;
            // Asset is valid, don't do anything - Path will always be set based on it when it matters
            if (IsValidSceneAsset) { return; }

            // Asset is invalid but have path to try and recover from
            if (!string.IsNullOrEmpty(m_scenePath))
            {
                m_sceneAsset = GetSceneAssetFromPath();
                // No asset found, path was invalid. Make sure we don't carry over the old invalid path
                if (m_sceneAsset == null)
                {
                    m_scenePath = string.Empty;
                }

                if (!Application.isPlaying)
                {
                    UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
                }
            }
        }
#endif
    }
}




