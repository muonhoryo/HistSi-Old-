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

    [CustomEditor(typeof(Converter_FloatToInt32))]
    public class ConverterEditor_FloatToInt32:Editor
    {
        public override void OnInspectorGUI()
        {
            DefaultMethods.ConverterOnInspectorGUI<float, int>(this);
        }
    }
    [CustomEditor(typeof(Converter_FloatToString))]
    public class ConverterEditor_FloatToString: Editor
    {
        public override void OnInspectorGUI()
        {
            DefaultMethods.ConverterOnInspectorGUI<float, string>(this);
        }
    }
    [CustomEditor(typeof(Converter_Int32ToFloat))]
    public class ConverterEditor_Int32ToFloat : Editor
    {
        public override void OnInspectorGUI()
        {
            DefaultMethods.ConverterOnInspectorGUI<int, float>(this);
        }
    }
    [CustomEditor(typeof(Converter_Int32ToString))]
    public class ConverterEditor_Int32ToString : Editor
    {
        public override void OnInspectorGUI()
        {
            DefaultMethods.ConverterOnInspectorGUI<int, string>(this);
        }
    }
    [CustomEditor(typeof(Converter_StringToFloat))]
    public class ConverterEditor_StringToFloat : Editor
    {
        public override void OnInspectorGUI()
        {
            DefaultMethods.ConverterOnInspectorGUI<string, float>(this);
        }
    }
    [CustomEditor(typeof(Converter_StringToInt32))]
    public class ConverterEditor_StringToInt32 : Editor
    {
        public override void OnInspectorGUI()
        {
            DefaultMethods.ConverterOnInspectorGUI<string, int>(this);
        }
    }
}
