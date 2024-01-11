using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtility.Pools;

public class CompoentPoolTest : ComponentPool<Transform>
{
    private void FixedUpdate()
    {
        PooledObject<Transform> a = Request();
        a.Object.gameObject.SetActive(true);
        StartCoroutine(ReleaseAfter(a, 0.5f));
    }

    private IEnumerator ReleaseAfter(PooledObject<Transform> obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.Release();
    }
}
