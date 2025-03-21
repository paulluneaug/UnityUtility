using System;

using UnityEngine;
using UnityEngine.UIElements;

using UnityUtility.CustomAttributes;
using UnityUtility.SceneReference;

public class AttributeTests : MonoBehaviour
{
    [Serializable]
    public struct NestedStruct
    {
        private bool ShowName => value > 12;

        public int value;
        [ShowIf(nameof(ShowName))]
        public SuperNestedStruct child;
    }

    [Serializable]
    public struct SuperNestedStruct
    {
        private bool ShowName => value > 12;

        public int value;
        [ShowIf(nameof(ShowName))]
        public SuperSuperNestedStruct child;
    }

    [Serializable]
    public struct SuperSuperNestedStruct
    {
        private bool ShowName => value > 12;

        public int value;
        [ShowIf(nameof(ShowName))]
        public string child;
    }

    protected enum Sau
    {
        CISSE,
        MURE,
        DIUM
    }

    [SerializeField] private SceneReference m_sceneRef;

    private bool Condition => B > 2;
    [Button(nameof(TestMethod1), "MÃƒÂ¯Ã‚Â¿Ã‚Â½thode Test0")]
    [Button(nameof(TestMethod1), "MÃƒÂ¯Ã‚Â¿Ã‚Â½thode Test1")]
    [Button(nameof(TestMethod2), "MÃƒÂ¯Ã‚Â¿Ã‚Â½thode Test2")]
    [SerializeField] private bool m_whatABool = true;
    [Title("Title Example", "With Subtitle (and underline)")]
    [DisableIf(nameof(Condition)), MinMaxSlider(2, 250, roundDigits: 1)]
    public Vector2[] A;
    public float B;
    [Title("No Subtitle nor underline (like a Header)", separator: false)]
    [ShowIf(nameof(m_so), Sau.CISSE)]
    public float C;
    public float D;
    [Title("They can be centered", titleAlignment: TitleAlignments.Centered)]
    public float E;
    [Separator]
    public float F;
    [Title("Or on the right", "And not bold", titleAlignment: TitleAlignments.Right, bold: false)]
    public float G;
    [Separator, Disable]
    public float H = 20;
    [Separator]
    [Separator]
    [Separator]

    [Space]

    [Title("HelpBoxes")]
    [HelpBox("It's possible to display an information,"), ShowIf(nameof(m_so), Sau.CISSE)]
    public float I;
    [Label("Super name J", bold: true)]
    public float J;
    [HelpBox("A Warning,", messageType: HelpBoxMessageType.Warning)/*, ShowIf("Condition")*/]
    public float K;
    [MinMaxSlider(-10, 10)]
    public Vector2 Sliderscsfc;
    public float L;
    [HelpBox("An Error,", messageType: HelpBoxMessageType.Error)]
    [HelpBox("Or just a sentence", messageType: HelpBoxMessageType.None)]
    public float M;
    [Layer]
    public byte B0;
    public float O;
    public float P;
    [Label("Super name Q", bold: true, italic: true, fontSize: 20)]
    public NestedStruct Q;
    [Label("Super name Q[]", bold: true, italic: true, fontSize: 7)]
    public NestedStruct[] QArray;

    [SerializeField]
    protected Sau m_so = Sau.CISSE;

    [Space]

    [Title("Sliders")]

    [HelpBox("Vector2 can be displayed as MinMax sliders")]
    [MinMaxSlider(-10, 10)]
    public Vector2 Slider;


    [HelpBox("You can choose wether the values are displayed as well as the numbers of digits used to round the values")]
    [MinMaxSlider(-10, 10, showFields: false, roundDigits: 1)]
    public Vector2 SliderWithoutValues;

    [MinMaxSlider(-10, 10, showFields: false, roundDigits: 1)]
    public Vector3 SliderWithoutValuesV3;
    [Separator]
    [Separator]
    [Separator]
    [HelpBox("It also works on arrays")]
    [MinMaxSlider(-10, 10, roundDigits: 0)]
    public Vector2[] MinMax3;
    [MinMaxSlider(-10, 10, roundDigits: 0)]
    public Vector3[] MinMax2;

    [Range(1, 10)]
    public double a;
    [Range(1, 10), Layer]
    public byte b;
    [Range(1, 10)]
    public long c;

    [Layer]
    public int[] layer0;
    [Layer]
    public uint layer1;
    [Layer]
    public long layer3;

    [Tag]
    public string Tag0;
    [Tag]
    public int Tag1;

    private void Start()
    {
        int a = 20;
        float b = 20;

        Debug.LogError($"Equals : {IsEquals(a, b)}");
    }

    private bool IsEquals(object x, object y)
    {
        return Convert.ChangeType(x, y.GetType()).Equals(y);
    }

    private void TestMethod0()
    {
        Debug.Log("Invoked0");
    }
    private void TestMethod1()
    {
        Debug.Log("Invoked1");
    }
    private void TestMethod2()
    {
        Debug.Log("Invoked2");
    }
}