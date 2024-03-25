using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

using UnityUtility.ManagedMonoBehaviours;

public class PerfTests : MonoBehaviour
{
    [SerializeField] private int m_mbCountToSpawn = 1000;
    [SerializeField] private ManagedMonoBehaviour m_container;
    [SerializeField] private ManagedMonoBehaviour m_prefab;

    private ManagedMonoBehaviour[] m_spawnedMB;
    public void CreateMB()
    {
        DateTime strartTime = DateTime.Now;
        m_spawnedMB = new ManagedMonoBehaviour[m_mbCountToSpawn];
        for (int i = 0; i < m_mbCountToSpawn; i++)
        {
            ManagedMonoBehaviour spawned = Instantiate(m_prefab);
            spawned.gameObject.name = $"MMB{i}";
            spawned.transform.parent = m_container.transform;
        }
        TimeSpan spawnDeltaTime = DateTime.Now - strartTime;

        if (m_spawnedMB == null)
            return;

        Debug.LogError($"Took {spawnDeltaTime.TotalMilliseconds} ms to spawn {m_mbCountToSpawn} objects");
    }

    public void DestroyMB()
    {
        System.Random r = new System.Random();
        var spawnedMBs = m_container.GetComponentsInChildren<ManagedMonoBehaviour>().ToList();
        spawnedMBs.Remove(m_container);
        spawnedMBs.OrderBy((mmb) => r.Next());
        m_spawnedMB = spawnedMBs.ToArray();
        int childCount = m_spawnedMB.Length;
        DateTime strartTime = DateTime.Now;
        foreach (ManagedMonoBehaviour spawned in m_spawnedMB)
        {
            DestroyImmediate(spawned.gameObject);
        }

        TimeSpan destroDeltaTime = DateTime.Now - strartTime;
        Debug.LogError($"Took {destroDeltaTime.TotalMilliseconds} ms to destroy {childCount} objects");

    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PerfTests))]
public class PerfTestsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create MB"))
        {
            ((PerfTests)target).CreateMB();
        }
        if (GUILayout.Button("Destroy MB"))
        {
            ((PerfTests)target).DestroyMB();
        }
    }
}
#endif