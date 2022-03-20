using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HistSiGUI;
using HistSiValueSources;
using HistSiSerialization;

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
    //TextDependence's editors
    public abstract class TextDependenceEditor<TValueType> : Editor
    {
        public override void OnInspectorGUI()
        {
            TextDependence<TValueType> obj = target as TextDependence<TValueType>;
            DefaultMethods.DrawInterface(ref obj.valueGetter, ref obj.GetterValueComponent, "Value Source " + typeof(TValueType).Name);
            DefaultMethods.DrawObjectField(ref obj.ValueText, "Text Field");
        }
    }
    [CustomEditor(typeof(TextDependence_Float))]
    public sealed class TextDependenceEditor_Float : TextDependenceEditor<float> { }
    [CustomEditor(typeof(TextDependence_Int32))]
    public sealed class TextDependenceEditor_Int32 : TextDependenceEditor<int> { }
    //Math operations editors
    public abstract class PairMathOperationEditor<TValueType>:Editor where TValueType:struct
    {
        public override void OnInspectorGUI()
        {
            PairMathOperation<TValueType> obj = target as PairMathOperation<TValueType>;
            SerializedObject serializedObject = new SerializedObject(obj);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OperationType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("FirstOperandIsFunction"));
            if (obj.FirstOperandIsFunction)
            {
                DefaultMethods.DrawInterface(ref obj.FirstSourceOperand, ref obj.FirstGetterValueComponent,
                    "First Operand " + typeof(TValueType));
            }
            else
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("FirstOperand"));
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("SecondOperandIsFunction"));
            if (obj.SecondOperandIsFunction)
            {
                DefaultMethods.DrawInterface(ref obj.SecondSourceOperand, ref obj.SecondGetterValueComponent,
                    "Second Operand " + typeof(TValueType));
            }
            else
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("SecondOperand"));
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
    public abstract class SingleMathOperationEditor<TValueType> : Editor where TValueType:struct
    {
        public override void OnInspectorGUI()
        {
            SingleMathOperation<TValueType> obj = target as SingleMathOperation<TValueType>;
            SerializedObject serializedObject = new SerializedObject(obj);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OperationType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OperandIsFunction"));
            if (obj.OperandIsFunction)
            {
                DefaultMethods.DrawInterface(ref obj.SourceOperand, ref obj.OperandSourseComponent, "Operand " + typeof(TValueType));
            }
            else
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Operand"));
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
    [CustomEditor(typeof(PairMathOperation_Float))]
    public sealed class PairMathOperationEditor_Float : PairMathOperationEditor<float> { }
    //Converter's editors
    public abstract class ConverterEditor<TInput,TOutput> : Editor
    {
        public override void OnInspectorGUI()
        {
            Converter<TInput, TOutput> obj = target as Converter<TInput, TOutput>;
            DefaultMethods.DrawInterface(ref obj.ValueSource, ref obj.ValueSourceComponent, "Value Source " + typeof(TInput));
        }
    }
    [CustomEditor(typeof(Converter_FloatToInt32))]
    public class ConverterEditor_FloatToInt32 : ConverterEditor<float, int> { }
    [CustomEditor(typeof(Converter_FloatToString))]
    public class ConverterEditor_FloatToString : ConverterEditor<float, string> { }
    [CustomEditor(typeof(Converter_Int32ToFloat))]
    public class ConverterEditor_Int32ToFloat : ConverterEditor<int, float> { }
    [CustomEditor(typeof(Converter_Int32ToString))]
    public class ConverterEditor_Int32ToString : ConverterEditor<int, string> { }
    [CustomEditor(typeof(Converter_StringToFloat))]
    public class ConverterEditor_StringToFloat : ConverterEditor<string, float> { }
    [CustomEditor(typeof(Converter_StringToInt32))]
    public class ConverterEditor_StringToInt32 : ConverterEditor<string, int> { }
    //CustomValues editor
    [CustomEditor(typeof(HistSiCustomValues))]
    public class HistSiCustomValuesEditor : Editor
    {
        public bool isShowing_Int32=true;
        public bool isShowing_Float=true;
        public bool isShowing_String = true;
        public bool isShowing_bool = true;
        public override void OnInspectorGUI()
        {
            HistSiCustomValues obj = target as HistSiCustomValues;
            SerializedObject serializedObj = new SerializedObject(obj);
            string text = "CustomValues ";
            {
                IDictionarySerializeHelper<string, int> dictHelper = obj;
                DefaultMethods.DrawDictionary(obj.CustomValues_Int32, serializedObj, dictHelper, ref isShowing_Int32,
                     text + typeof(int));
            }
            {
                IDictionarySerializeHelper<string, float> dictHelper = obj;
                DefaultMethods.DrawDictionary(obj.CustomValues_Float, serializedObj, dictHelper, ref isShowing_Float,
                    text + typeof(float));
            }
            {
                IDictionarySerializeHelper<string, string> dictHelper = obj;
                DefaultMethods.DrawDictionary(obj.CustomValues_String, serializedObj, dictHelper, ref isShowing_String,
                    text + typeof(string));
            }
            /*SerializeDictionary(ref obj.CustomValues_Bool,ref serializedObj, "CustomValues_Bool", text + typeof(bool), keyText, valueText,
                                "");*/
        }
    }
}
