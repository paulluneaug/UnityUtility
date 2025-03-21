using UnityEngine;

using UnityUtility.Singletons;
[CreateAssetMenu(fileName = nameof(TestScriptableSingleton), menuName = nameof(TestScriptableSingleton))]
public class TestScriptableSingleton : ScriptableSingleton<TestScriptableSingleton>
{
}
