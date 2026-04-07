using UnityEngine;

[CreateAssetMenu(fileName = "InlineScriptable", menuName = "Scriptable Objects/InlineScriptable")]
public class InlineScriptable : ScriptableObject
{
    [SerializeField] private int m_int0;
    [SerializeField, Range(0.0f, 1.0f)] private float m_range0;
}
