using System;

using UnityEngine;

using UnityUtility.Attributes;

public class NoFoldoutTests : MonoBehaviour
{
    [Serializable] 
    private class FoldoutClass
    {
        [SerializeField] private int m_int0;
    }


    [Title("EditInline Tests")]
    [Button(nameof(Foo))]
    [SerializeField, NoFoldout] private FoldoutClass m_scriptable0;
    [SerializeField, NoFoldout] private FoldoutClass m_scriptable1;
    [SerializeField, NoFoldout] private int m_int;
    [SerializeField, NoFoldout] private FoldoutClass[] m_array;

    private void Foo()
    {

    }
}
