using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityUtility.CustomAttributes.Utils;
using UnityUtility.CustomAttributes;

public class AttributeTests : MonoBehaviour
{
    protected enum SAU
    {
        CISSE,
        MURE,
        DIUM
    }

    private bool Condition => B > 2;

    [SerializeField] private bool WhatABool = true;
    [Title("Title Example", "With Subtitle (and underline)")]
    [ShowIf("so", SAU.CISSE)] public float[] A;
    public float B;
    [Title("No Subtitle nor underline (like a Header)", horizontalLine: false)]
    [ShowIf("so", SAU.CISSE)] public float C;
    public float D;
    [Title("They can be centered", titleAlignment: TitleAlignments.Centered)]
    public float E;
    public float F;
    [Title("Or on the right", "And not bold", titleAlignment: TitleAlignments.Right, bold: false)]
    public float G;
    public float H = 20;

    [Space]

    [Title("HelpBoxes")]
    [HelpBox("It's possible to display an information,"), ShowIf("so", SAU.CISSE)]
    public float I;
    public float J;
    [HelpBox("A Warning,", messageType: HelpBoxMessageType.Warning)/*, ShowIf("Condition")*/]
    public float K;
    [MinMaxSlider(-10, 10)]
    public Vector2 Sliderscsfc;
    public float L;
    [HelpBox("An Error,", messageType: HelpBoxMessageType.Error)]
    public float M;
    public float O;
    [HelpBox("Or just a sentence", messageType: HelpBoxMessageType.None)]
    public float P;
    public float Q;

    [SerializeField]
    protected SAU so = SAU.CISSE;

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

    [HelpBox("It also works on arrays")]
    [MinMaxSlider(-10, 10, roundDigits: 0)]
    public Vector2[] MinMax3;
    [MinMaxSlider(-10, 10, roundDigits: 0)]
    public Vector3[] MinMax2;

    [Range(1, 10)]
    public double a;
    [Range(1, 10)]
    public byte b;
    [Range(1, 10)]
    public long c;

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
}
