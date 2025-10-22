using UnityEngine;

using UnityUtility.Attributes;

public class EditInlineTests : MonoBehaviour
{
    //[Title("EditInline Tests")]
    [SerializeField, EditInline] private InlineScriptable m_scriptable0;
    [SerializeField, EditInline] private InlineScriptable m_scriptable1;
}
