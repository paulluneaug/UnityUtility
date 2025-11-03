using UnityEngine;

using UnityUtility.Attributes;

public class EditInlineTests : MonoBehaviour
{
    //[Title("EditInline Tests")]
    [Button(nameof(Foo))]
    [SerializeField, EditInline] private InlineScriptable m_scriptable0;
    [SerializeField, EditInline] private InlineScriptable m_scriptable1;
    [SerializeField, EditInline] private int m_int;
    [SerializeField, EditInline] private InlineScriptable[] m_array;
    [SerializeField, EditInline] private MonoBehaviour[] m_mb;

    private void Foo()
    {

    }
}
