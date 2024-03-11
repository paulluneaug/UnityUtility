using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor.UIElements;

namespace UnityUtility.CustomAttributes.Editor
{
    [CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
    public class MinMaxSliderAttributeDrawer : PropertyDrawer
    {
        private const float FIELDS_WIDTH = 40;
        private const float SLIDER_OFFSET = 5;

        private float m_minValue;
        private float m_maxValue;

        #region IMGUI
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Vector2)
            {
                return GetBoxHeight(MinMaxSliderAttribute.WRONG_TYPE_ERROR + property.propertyType);
            }
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.isEditingMultipleObjects)
            {
                return;
            }

            if (property.propertyType != SerializedPropertyType.Vector2)
            {
                Rect labelRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);
                EditorGUI.LabelField(labelRect, label);

                Rect boxRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, position.width - EditorGUIUtility.labelWidth, position.height);
                EditorGUI.HelpBox(boxRect, MinMaxSliderAttribute.WRONG_TYPE_ERROR + property.propertyType, MessageType.Error);
            }
            else
            {
                m_minValue = property.vector2Value.x;
                m_maxValue = property.vector2Value.y;

                MinMaxSliderAttribute minMaxSliderAttribute = attribute as MinMaxSliderAttribute;

                _ = EditorGUI.BeginProperty(position, label, property);

                Rect labelRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);

                EditorGUI.LabelField(labelRect, label);

                Rect sliderRect;
                GUIContent sliderLabel = new GUIContent(label)
                {
                    text = string.Empty
                };

                Rect propertyRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, position.width - EditorGUIUtility.labelWidth, position.height);

                if (minMaxSliderAttribute.ShowFields)
                {
                    Rect fieldMinRect = new Rect(
                        propertyRect.x + 2,
                        propertyRect.y,
                        FIELDS_WIDTH - 1,
                        EditorGUIUtility.singleLineHeight);

                    Rect fieldMaxRect = new Rect(
                        propertyRect.x + propertyRect.width - FIELDS_WIDTH + 1,
                        propertyRect.y,
                        FIELDS_WIDTH - 1,
                        EditorGUIUtility.singleLineHeight);

                    sliderRect = new Rect(
                        propertyRect.x + FIELDS_WIDTH + SLIDER_OFFSET,
                        propertyRect.y,
                        propertyRect.width - ((FIELDS_WIDTH + SLIDER_OFFSET) * 2),
                        EditorGUIUtility.singleLineHeight);

                    m_minValue = EditorGUI.FloatField(fieldMinRect, (float)Math.Round(m_minValue, minMaxSliderAttribute.RoundDigits));
                    m_maxValue = EditorGUI.FloatField(fieldMaxRect, (float)Math.Round(m_maxValue, minMaxSliderAttribute.RoundDigits));

                }
                else
                {
                    sliderRect = propertyRect;
                }


                EditorGUI.MinMaxSlider(sliderRect, sliderLabel, ref m_minValue, ref m_maxValue, minMaxSliderAttribute.MinValue, minMaxSliderAttribute.MaxValue);

                EditorGUI.EndProperty();

                SetMinMax(property, minMaxSliderAttribute);
            }
        }

        private void SetMinMax(SerializedProperty property, MinMaxSliderAttribute minMaxSlider)
        {
            property.vector2Value = ClampMinMax(new Vector2(m_minValue, m_maxValue), minMaxSlider.MinValue, minMaxSlider.MaxValue);
        }

        private Vector2 ClampMinMax(Vector2 minMax, float min, float max)
        {
            float y = Mathf.Clamp(minMax.y, minMax.x, max);
            float x = Mathf.Clamp(minMax.x, min, y);

            return new Vector2(x, y);
        }

        private float GetBoxHeight(string message)
        {
            GUIStyle helpBoxStyle = (GUI.skin != null) ? GUI.skin.GetStyle("helpbox") : null;

            return Mathf.Max(EditorGUIUtility.singleLineHeight * 2, helpBoxStyle.CalcHeight(new GUIContent(message), EditorGUIUtility.currentViewWidth) + 4);
        }
        #endregion

        #region UIElements
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            MinMaxSliderAttribute minMaxSliderAttribute = attribute as MinMaxSliderAttribute;
            int roundDigits = minMaxSliderAttribute.RoundDigits;

            VisualElement container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;

            Label propertyLabel = new Label(property.displayName);
            propertyLabel.style.width = AttributeUtils.LabelWidth;
            container.Add(propertyLabel);

            if (property.propertyType != SerializedPropertyType.Vector2)
            {
                container.Add(new HelpBox(MinMaxSliderAttribute.WRONG_TYPE_ERROR + property.propertyType, HelpBoxMessageType.Error));
                return container;
            }

            Vector2 currentVal = Round(property.vector2Value, roundDigits);
            property.vector2Value = currentVal;
            property.serializedObject.ApplyModifiedProperties();

            VisualElement sliderContainer = new VisualElement
            {
                name = "Slider Container"
            };
            sliderContainer.style.flexDirection = FlexDirection.Row;
            sliderContainer.style.justifyContent = Justify.SpaceBetween;
            sliderContainer.style.flexGrow = 1;

            MinMaxSlider slider = new MinMaxSlider(currentVal.x, currentVal.y, minMaxSliderAttribute.MinValue, minMaxSliderAttribute.MaxValue);
            slider.style.flexGrow = 1;
            slider.style.paddingLeft = SLIDER_OFFSET;
            slider.style.paddingRight = SLIDER_OFFSET;

            FloatField minField = null;
            FloatField maxField = null;
            if (minMaxSliderAttribute.ShowFields)
            {
                minField = new FloatField
                {
                    value = currentVal.x
                };
                minField.style.width = FIELDS_WIDTH - 1;

                maxField = new FloatField
                {
                    value = currentVal.y
                };
                maxField.style.width = FIELDS_WIDTH - 1;

                minField.RegisterCallback<FocusOutEvent>(OnMinFocusOut);
                maxField.RegisterCallback<FocusOutEvent>(OnMaxFocusOut);
            }

            slider.RegisterValueChangedCallback(OnSliderChanged);

            void OnMinFocusOut(FocusOutEvent evt)
            {
                float minValue = Round(minField.value, roundDigits);
                float maxValue = Mathf.Max(minValue, maxField.value);
                maxField.value = maxValue;
                slider.value = new Vector2(minValue, maxValue);
            }

            void OnMaxFocusOut(FocusOutEvent evt)
            {
                float maxValue = Round(maxField.value, roundDigits);
                float minValue = Mathf.Min(maxValue, minField.value);
                minField.value = minValue;
                slider.value = new Vector2(minValue, maxValue);
            }

            void OnSliderChanged(ChangeEvent<Vector2> evt)
            {
                Vector2 newValue = Round(evt.newValue, roundDigits);
                if (minMaxSliderAttribute.ShowFields)
                {
                    minField.value = newValue.x;
                    maxField.value = newValue.y;
                }
                property.vector2Value = newValue;
                property.serializedObject.ApplyModifiedProperties();
            }

            if (minMaxSliderAttribute.ShowFields)
            {
                sliderContainer.Add(minField);
                sliderContainer.Add(slider);
                sliderContainer.Add(maxField);
            }
            else
            {
                sliderContainer.Add(slider);
            }

            container.Add(sliderContainer);

            return container;
        }

        private static float Round(float value, int roundDigits)
        {
            return MathF.Round(value, roundDigits);
        }
        private static Vector2 Round(Vector2 value, int roundDigits)
        {
            return new Vector2(MathF.Round(value.x, roundDigits), MathF.Round(value.y, roundDigits));
        }
        #endregion
    }
}
