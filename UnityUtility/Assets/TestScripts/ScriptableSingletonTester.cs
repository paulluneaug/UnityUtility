using UnityEngine;

public class ScriptableSingletonTester : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var a = TestScriptableSingleton.Instance;
        Debug.Log(a == null);
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
