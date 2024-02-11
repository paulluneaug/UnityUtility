using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtility.Singletons;
[CreateAssetMenu(fileName = nameof(TestScriptableSingleton), menuName = nameof(TestScriptableSingleton))]
public class TestScriptableSingleton : ScriptableSingleton<TestScriptableSingleton>
{
}
