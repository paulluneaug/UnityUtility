// Author: JohannesMP (2018-08-12)
// https://gist.github.com/JohannesMP/ec7d3f0bcf167dab3d0d3bb480e0e07b

using UnityEditor;

using UnityEngine;

namespace UnityUtility.SceneReference.Editor
{
    /// <summary>
    /// Display a Scene Reference object in the editor.
    /// If scene is valid, provides basic buttons to interact with the scene's role in Build Settings.
    /// </summary>
    [CustomPropertyDrawer(typeof(SceneReference))]
    public class SceneReferencePropertyDrawer : PropertyDrawer
    {
        // The exact name of the asset Object variable in the SceneReference object
        private const string SCENE_ASSET_PROPERTY = "m_sceneAsset";
        // The exact name of  the scene Path variable in the SceneReference object
        private const string SCENE_PATH_PROPERTY = "m_scenePath";

        private static readonly RectOffset s_boxPadding = EditorStyles.helpBox.padding;
        private static readonly float s_padSize = 2f;
        private static readonly float s_lineHeight = EditorGUIUtility.singleLineHeight;
        private static readonly float s_paddedLine = s_lineHeight + s_padSize;
        private static readonly float s_footerHeight = 10f;

        /// <summary>
        /// Drawing the 'SceneReference' property
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty sceneAssetProperty = GetSceneAssetProperty(property);

            // Draw the Box Background
            position.height -= s_footerHeight;
            GUI.Box(EditorGUI.IndentedRect(position), GUIContent.none, EditorStyles.helpBox);
            position = s_boxPadding.Remove(position);
            position.height = s_lineHeight;

            // Draw the main Object field
            label.tooltip = "The actual Scene Asset reference.\nOn serialize this is also stored as the asset's path.";

            _ = EditorGUI.BeginProperty(position, GUIContent.none, property);
            EditorGUI.BeginChangeCheck();
            int sceneControlID = GUIUtility.GetControlID(FocusType.Passive);
            Object selectedObject = EditorGUI.ObjectField(position, label, sceneAssetProperty.objectReferenceValue, typeof(SceneAsset), false);
            BuildUtils.BuildScene buildScene = BuildUtils.GetBuildScene(selectedObject);

            if (EditorGUI.EndChangeCheck())
            {
                sceneAssetProperty.objectReferenceValue = selectedObject;

                // If no valid scene asset was selected, reset the stored path accordingly
                if (buildScene.scene == null)
                {
                    GetScenePathProperty(property).stringValue = string.Empty;
                }
            }
            position.y += s_paddedLine;

            if (buildScene.assetGUID.Empty() == false)
            {
                // Draw the Build Settings Info of the selected Scene
                DrawSceneInfoGUI(position, buildScene, sceneControlID + 1);
            }

            EditorGUI.EndProperty();
        }

        /// <summary>
        /// Ensure that what we draw in OnGUI always has the room it needs
        /// </summary>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int lines = 2;
            SerializedProperty sceneAssetProperty = GetSceneAssetProperty(property);
            if (sceneAssetProperty.objectReferenceValue == null)
            {
                lines = 1;
            }

            return s_boxPadding.vertical + s_lineHeight * lines + s_padSize * (lines - 1) + s_footerHeight;
        }

        /// <summary>
        /// Draws info box of the provided scene
        /// </summary>
        private void DrawSceneInfoGUI(Rect position, BuildUtils.BuildScene buildScene, int sceneControlID)
        {
            bool readOnly = BuildUtils.IsReadOnly();
            string readOnlyWarning = readOnly ? "\n\nWARNING: Build Settings is not checked out and so cannot be modified." : "";

            // Label Prefix
            GUIContent iconContent;
            GUIContent labelContent = new GUIContent();

            // Missing from build scenes
            if (buildScene.buildIndex == -1)
            {
                iconContent = EditorGUIUtility.IconContent("d_winbtn_mac_close");
                labelContent.text = "NOT In Build";
                labelContent.tooltip = "This scene is NOT in build settings.\nIt will be NOT included in builds.";
            }
            // In build scenes and enabled
            else if (buildScene.scene.enabled)
            {
                iconContent = EditorGUIUtility.IconContent("d_winbtn_mac_max");
                labelContent.text = "BuildIndex: " + buildScene.buildIndex;
                labelContent.tooltip = "This scene is in build settings and ENABLED.\nIt will be included in builds." + readOnlyWarning;
            }
            // In build scenes and disabled
            else
            {
                iconContent = EditorGUIUtility.IconContent("d_winbtn_mac_min");
                labelContent.text = "BuildIndex: " + buildScene.buildIndex;
                labelContent.tooltip = "This scene is in build settings and DISABLED.\nIt will be NOT included in builds.";
            }

            // Left status label
            using (new EditorGUI.DisabledScope(readOnly))
            {
                Rect labelRect = DrawUtils.GetLabelRect(position);
                Rect iconRect = labelRect;
                iconRect.width = iconContent.image.width + s_padSize;
                labelRect.width -= iconRect.width;
                labelRect.x += iconRect.width;
                _ = EditorGUI.PrefixLabel(iconRect, sceneControlID, iconContent);
                _ = EditorGUI.PrefixLabel(labelRect, sceneControlID, labelContent);
            }

            // Right context buttons
            Rect buttonRect = DrawUtils.GetFieldRect(position);
            buttonRect.width = (buttonRect.width) / 3;

            string tooltipMsg;
            using (new EditorGUI.DisabledScope(readOnly))
            {
                // NOT in build settings
                if (buildScene.buildIndex == -1)
                {
                    buttonRect.width *= 2;
                    int addIndex = EditorBuildSettings.scenes.Length;
                    tooltipMsg = "Add this scene to build settings. It will be appended to the end of the build scenes as buildIndex: " + addIndex + "." + readOnlyWarning;
                    if (DrawUtils.ButtonHelper(buttonRect, "Add...", "Add (buildIndex " + addIndex + ")", EditorStyles.miniButtonLeft, tooltipMsg))
                    {
                        BuildUtils.AddBuildScene(buildScene);
                    }

                    buttonRect.width /= 2;
                    buttonRect.x += buttonRect.width;
                }
                // In build settings
                else
                {
                    bool isEnabled = buildScene.scene.enabled;
                    string stateString = isEnabled ? "Disable" : "Enable";
                    tooltipMsg = stateString + " this scene in build settings.\n" + (isEnabled ? "It will no longer be included in builds" : "It will be included in builds") + "." + readOnlyWarning;

                    if (DrawUtils.ButtonHelper(buttonRect, stateString, stateString + " In Build", EditorStyles.miniButtonLeft, tooltipMsg))
                    {
                        BuildUtils.SetBuildSceneState(buildScene, !isEnabled);
                    }

                    buttonRect.x += buttonRect.width;

                    tooltipMsg = "Completely remove this scene from build settings.\nYou will need to add it again for it to be included in builds!" + readOnlyWarning;
                    if (DrawUtils.ButtonHelper(buttonRect, "Remove...", "Remove from Build", EditorStyles.miniButtonMid, tooltipMsg))
                    {
                        BuildUtils.RemoveBuildScene(buildScene);
                    }
                }
            }

            buttonRect.x += buttonRect.width;

            tooltipMsg = "Open the 'Build Settings' Window for managing scenes." + readOnlyWarning;
            if (DrawUtils.ButtonHelper(buttonRect, "Settings", "Build Settings", EditorStyles.miniButtonRight, tooltipMsg))
            {
                BuildUtils.OpenBuildSettings();
            }

        }

        private static SerializedProperty GetSceneAssetProperty(SerializedProperty property)
        {
            return property.FindPropertyRelative(SCENE_ASSET_PROPERTY);
        }

        private static SerializedProperty GetScenePathProperty(SerializedProperty property)
        {
            return property.FindPropertyRelative(SCENE_PATH_PROPERTY);
        }

        private static class DrawUtils
        {
            /// <summary>
            /// Draw a GUI button, choosing between a short and a long button text based on if it fits
            /// </summary>
            public static bool ButtonHelper(Rect position, string msgShort, string msgLong, GUIStyle style, string tooltip = null)
            {
                GUIContent content = new GUIContent(msgLong)
                {
                    tooltip = tooltip
                };

                float longWidth = style.CalcSize(content).x;
                if (longWidth > position.width)
                {
                    content.text = msgShort;
                }

                return GUI.Button(position, content, style);
            }

            /// <summary>
            /// Given a position rect, get its field portion
            /// </summary>
            public static Rect GetFieldRect(Rect position)
            {
                position.width -= EditorGUIUtility.labelWidth;
                position.x += EditorGUIUtility.labelWidth;
                return position;
            }
            /// <summary>
            /// Given a position rect, get its label portion
            /// </summary>
            public static Rect GetLabelRect(Rect position)
            {
                position.width = EditorGUIUtility.labelWidth - s_padSize;
                return position;
            }
        }
    }
}
