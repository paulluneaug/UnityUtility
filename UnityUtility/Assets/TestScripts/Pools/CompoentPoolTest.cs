using System;
using System.Collections;

using UnityEngine;

using UnityUtility.Pools;

public class CompoentPoolTest : MonoBehaviour
{
    [SerializeField] private ComponentPoolParameters<FieldsFiltersTest> m_poolSettings;
    [NonSerialized] private ComponentPool<FieldsFiltersTest> m_pool;

    private void Awake()
    {
        m_pool = new ComponentPool<FieldsFiltersTest>(m_poolSettings);
    }

    private void FixedUpdate()
    {
        PooledObject<FieldsFiltersTest> a = m_pool.Request();
        a.Object.gameObject.SetActive(true);
        _ = StartCoroutine(ReleaseAfter(a, 0.5f));
    }

    private IEnumerator ReleaseAfter(PooledObject<FieldsFiltersTest> obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.Release();
    }
}
