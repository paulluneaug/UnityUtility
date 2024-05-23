using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableSingletonTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var a = TestScriptableSingleton.Instance;
        Debug.Log(a == null);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
