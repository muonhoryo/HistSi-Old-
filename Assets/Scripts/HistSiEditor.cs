using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HistSiGUI;
using HistSiValueSources;

namespace HistSiCustomEditor
{
    [CustomEditor(typeof(HistSiSlider))]
    public class HistSiSliderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }
    [CustomEditor(typeof(TextDependence_Float))]
    public class TextDependenceEditor_Float : Editor
    {
        public override void OnInspectorGUI()
        {
            DefaultMethods.TextDependenceOnInspectorGUI<float>(this);
        }
    }
    [CustomEditor(typeof(TextDependence_Int32))]
    public class TextDependenceEditor_Int32 : Editor
    {
        public override void OnInspectorGUI()
        {
            DefaultMethods.TextDependenceOnInspectorGUI<int>(this);
        }
    }
    [CustomEditor(typeof(PairMathOperation_Float))]
    public class PairMathOperationEditor_Float:Editor
    {
        public override void OnInspectorGUI()
        {
            DefaultMethods.PairMathOperationOnInspectorGUI<float>(this);
        }
    }
    [CustomEditor(typeof(SingleMathOperation_Float))]
    public class SingleMathOperationEditor_Float:Editor
    {
        public override void OnInspectorGUI()
        {
            DefaultMethods.SingleMathOperationOnInspectorGUI<float>(this);
        }
    }
}
