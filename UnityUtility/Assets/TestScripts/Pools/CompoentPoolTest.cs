using System.Collections;

using UnityEngine;

using UnityUtility.Pools;

public class CompoentPoolTest : ComponentPool<FieldsFiltersTest>
{
    private void FixedUpdate()
    {
        PooledObject<FieldsFiltersTest> a = Request();
        a.Object.gameObject.SetActive(true);
        _ = StartCoroutine(ReleaseAfter(a, 0.5f));
    }

    private IEnumerator ReleaseAfter(PooledObject<FieldsFiltersTest> obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.Release();
    }
}
