using System.Text;

using UnityEngine;

using UnityUtility.CustomAttributes;
using UnityUtility.SerializedDictionary;

public class SerializedDictionaryTests : MonoBehaviour
{
    [Button(nameof(TestDict))]
    [SerializeField] private SerializedDictionary<string, float> dictionary;

    public void TestDict()
    {
        PrintDict();

        dictionary["fixed"] = 12.0f;
        dictionary["random"] = UnityEngine.Random.value;

        PrintDict();
    }

    private void PrintDict()
    {
        StringBuilder stBuilder = new StringBuilder("Pairs : \n");
        foreach (var key in dictionary.Keys)
        {
            stBuilder = stBuilder.AppendLine($"{key} => {dictionary[key]}");
        }

        Debug.Log(stBuilder.ToString());
    }
}
