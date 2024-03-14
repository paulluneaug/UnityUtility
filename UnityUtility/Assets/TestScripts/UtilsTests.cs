using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtility.Utils;

public class UtilsTests : MonoBehaviour
{
    [ContextMenu(nameof(TestUtils))]
    public void TestUtils()
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
}