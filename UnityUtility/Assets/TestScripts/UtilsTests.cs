using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtility.Utils;

public class UtilsTests : MonoBehaviour
{
    [ContextMenu(nameof(TestRemaps))]
    public void TestRemaps()
    {
        TestRemap(0, 0, 1, 0, 1);
        TestRemap(1, 0, 1, 0, 1);
        TestRemap(0.5f, 0, 1, 0, 1);
        TestRemap(2, 0, 1, 0, 1);

        TestRemap(0, 0, 1, -50, 0);
        TestRemap(1, 0, 1, -50, 0);
        TestRemap(0.5f, 0, 1, -50, 0);
        TestRemap(2, 0, 1, -50, 0);

        TestRemap(0, 0, 200, 0, 1);
        TestRemap(1, 0, 200, 0, 1);
        TestRemap(0.5f, 0, 200, 0, 1);
        TestRemap(2, 0, 200, 0, 1);
    }

    private void TestRemap(float val, float inMin, float inMax, float outMin, float outMax)
    {

        Debug.Log($"{val} from [{inMin};{inMax}] <=> {val.Remap(inMin, inMax, outMin, outMax)} in the interval [{outMin};{outMax}]");
    }

    [ContextMenu(nameof(TestHash))]
    public void TestHash()
    {
        int a = 2;
        int b = 2;
        HashCode h = new HashCode();
        h.Add(a);
        Debug.Log(h.ToHashCode());
        h.Add(b);
        Debug.Log(h.ToHashCode());
        unchecked
        {
            Debug.Log($"0x9e3779b9 = {0x9e3779b9} => {(int)0x9e3779b9}");
        }
        Debug.Log(HashCode.Combine(a, b));

        List<int> list = new List<int>();
        List<int> list2 = new List<int>();

        Debug.LogWarning(list.GetHashCode());
        Debug.LogWarning(list2.GetHashCode());

        list.Add(1234654843);
        list2.Add(1234654843);
        Debug.LogWarning(list.GetHashCode());
        Debug.LogWarning(list2.GetHashCode());
    }
}