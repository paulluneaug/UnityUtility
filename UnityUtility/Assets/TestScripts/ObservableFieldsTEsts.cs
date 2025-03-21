using UnityEngine;

using UnityUtility.ObservableFields;

public class ObservableFieldsTEsts : MonoBehaviour
{
    [SerializeField] private ObservableField<Component> m_comp;
    [SerializeField] private ObservableList<Component> m_comps;
    [SerializeField] private Component m_comp2;

    [ContextMenu("Awake")]
    private void M_Awake()
    {
        m_comp.OnValueChanged += OnCompChanged;
        m_comps.OnListChanged += OnListChanged;
    }

    private void OnListChanged(ListChangeOperations operations)
    {
        Debug.Log($"CompList Changed : {operations}");
    }

    private void OnCompChanged(Component component)
    {
        Debug.Log($"Comp Changed to {component.name}");
    }

    [ContextMenu("ChangeComp")]
    private void ChangeComp()
    {
        m_comp.Value = m_comp2;
        m_comps.Add(m_comp2);
    }
}
