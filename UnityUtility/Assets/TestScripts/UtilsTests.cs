using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityUtility.CustomAttributes;
using UnityUtility.Hash;
using UnityUtility.MathU;
using UnityUtility.Recorders;
using UnityUtility.Extensions;

public class UtilsTests : MonoBehaviour
{
    [Button(nameof(TestHash))]
    [Button(nameof(MathTests))]
    [SerializeField] private uint m_count = 10_000;
    [SerializeField] private uint m_seed;
    [SerializeField] private float m_prob;
    [SerializeField] private Vector2Int m_range;

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

    [ContextMenu(nameof(MathTests))]
    private void MathTests()
    {
        PowerOfTwos();
    }

    private void PowerOfTwos()
    {
        ComparePowerMethods(0);
        ComparePowerMethods(-1);
        ComparePowerMethods(2);
        ComparePowerMethods((1 << 31) + 1);
        ComparePowerMethods(1 << 31);
        ComparePowerMethods(4);
        ComparePowerMethods(3);
    }

    private void ComparePowerMethods(int val)
    {
        Debug.Log($"For {val}");
        Debug.Log($"IsPowerOfTwo ? : {Mathf.IsPowerOfTwo(val)} => {MathUf.IsPowerOfTwo(val)}");
        Debug.Log($"Next : {Mathf.NextPowerOfTwo(val)} => {MathUf.NextPowerOfTwo(val)}");
        Debug.Log($"Closest : {Mathf.ClosestPowerOfTwo(val)} => {MathUf.ClosestPowerOfTwo(val)}");
        Debug.Log($"Previous : {MathUf.PreviousPowerOfTwo(val)}");
        //Debug.Log($"Range : {MathUf.GetPreviousAndNextPowerOfTwo(val)}");
        Debug.Log($"==========================");
    }

    private void TestRemap(float val, float inMin, float inMax, float outMin, float outMax)
    {

        Debug.Log($"{val} from [{inMin};{inMax}] <=> {val.Remap(inMin, inMax, outMin, outMax)} in the interval [{outMin};{outMax}]");
    }

    [ContextMenu(nameof(TestHashCode))]
    public void TestHashCode()
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
        Debug.Log($"===={1.CompareTo(2)}");
    }

    private static double Average(IEnumerable<int> ints)
    {
        double sum = 0;
        int count = 0;
        foreach (int i in ints)
        {
            sum += i;
            count++;
        }
        return  sum / count;
    }

    [ContextMenu(nameof(TestHash))]
    private void TestHash()
    {
        HierarchicalProfilingRecorder recorder = new HierarchicalProfilingRecorder("HashTests");
        recorder.BeginEvent("=====HashTests=====");

        recorder.BeginEvent("Init");
        Debug.LogError($"====={nameof(TestHash)}=====");
        IEnumerable<int> baseEnum = Enumerable.Repeat(0, (int)m_count);
        Hasher hasher = new Hasher(m_seed);

        recorder.BeginEvent("Random Ints");
        recorder.BeginEvent("Compute Ints");
        IEnumerable<int> intEnum = baseEnum.Select(i => hasher.RandomInt());
        recorder.EndEvent();
        recorder.BeginEvent("Log Ints");
        using (StreamWriter stream = File.CreateText(Path.Combine(Application.dataPath, "TestExports", "RandomInts.txt")))
        {
            stream.WriteLine(intEnum.EnumerableToString());
            stream.Close();
        }
        Debug.LogWarning($"{m_count} pseudo random ints mean = {Average(intEnum)}");

        recorder.EndEvent();
        recorder.EndEvent();

        recorder.BeginEvent("Random Ints in range");
        recorder.BeginEvent("Compute Ints in range");
        IEnumerable<int> intInRangeEnum = baseEnum.Select(i => hasher.RandomInt(m_range));
        recorder.EndEvent();
        recorder.BeginEvent("Log Ints");
        using (StreamWriter stream = File.CreateText(Path.Combine(Application.dataPath, "TestExports", "RandomIntsInRange.txt")))
        {
            stream.WriteLine(intInRangeEnum.EnumerableToString());
            stream.Close();
        }
        Debug.LogWarning($"{m_count} pseudo random ints mean = {Average(intInRangeEnum)}");

        recorder.EndEvent();
        recorder.EndEvent();

        recorder.BeginEvent("Random bools");
        recorder.BeginEvent("Compute bools");
        IEnumerable<bool> boolEnum = baseEnum.Select(i => hasher.RandomBool());
        recorder.EndEvent();
        recorder.BeginEvent("Log bools");
        Debug.LogWarning($"{m_count} pseudo random bools 'True' count = {boolEnum.Where(b => b).Count()}");
        Debug.Log($"{boolEnum.EnumerableToString()}");
        recorder.EndEvent();
        recorder.EndEvent();

        recorder.BeginEvent("Random bools Prob");
        recorder.BeginEvent("Compute bools Prob");
        IEnumerable<bool> boolProbEnum = baseEnum.Select(i => hasher.RandomBoolProb(m_prob));
        recorder.EndEvent();
        recorder.BeginEvent("Log boolsProb");
        int trueProbCount = boolProbEnum.Where(b => b).Count();
        Debug.LogWarning($"{m_count} pseudo random bools with prob : 'True' count = {trueProbCount} => actual prob = {(float)trueProbCount / m_count} (expected {m_prob})");
        recorder.EndEvent();
        recorder.EndEvent();

        recorder.BeginEvent("Random floats01");
        recorder.BeginEvent("Compute floats01");
        IEnumerable<float> float01Enum = baseEnum.Select(i => hasher.RandomFloat01());
        recorder.EndEvent();
        recorder.BeginEvent("Log floats 01");
        Debug.LogWarning($"{m_count} pseudo random floats 01 average = {float01Enum.Average()}");
        Debug.Log($"{float01Enum.EnumerableToString()}");
        recorder.EndEvent();
        recorder.EndEvent();

        recorder.EndEvent();

        recorder.BeginEvent("=====SortTests=====");

        recorder.BeginEvent("SortTests");

        recorder.BeginEvent("ToArray");
        int[] randomArray = intEnum.ToArray();
        recorder.EndEvent();

        recorder.BeginEvent("Sort");
        randomArray.Sort();
        recorder.EndEvent();

        recorder.BeginEvent("LogSorted");
        using (StreamWriter stream = File.CreateText(Path.Combine(Application.dataPath, "TestExports", "SortedInts.txt")))
        {
            stream.WriteLine(randomArray.EnumerableToString());
            stream.Close();
        }
        Debug.Log($"Is Sorted ? : {randomArray.IsSorted()}");
        recorder.EndEvent();

        recorder.BeginEvent("Shuffle");
        randomArray.Shuffle();
        recorder.EndEvent();

        recorder.BeginEvent("LogSorted");
        using (StreamWriter stream = File.CreateText(Path.Combine(Application.dataPath, "TestExports", "ReshuffledInts.txt")))
        {
            stream.WriteLine(randomArray.EnumerableToString());
            stream.Close();
        }

        recorder.EndEvent();
    }
}