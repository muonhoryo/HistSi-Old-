using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using HistSiGUI;
using HistSiValueSources;

namespace HistSiInterfaces
{
    public static class DefaultMethods
    {
        public static void IRemovableRemove(IRemovable removable)
        {
            IRemovableRemove(removable, ()=> { });
        }
        public static void IRemovableRemove(IRemovable removable, Action beforeAnimStartAction)
        {
            if (removable.OnDestroyAnimation == null)
            {
                GameObject.Destroy(removable.DestroyedObject);
            }
            else
            {
                beforeAnimStartAction();
                removable.OnDestroyAnimation.Play();
                removable.StartCoroutine(HistSiGUI.DefaultMethods.DelayDestroy(removable));
            }
        }
        public static void ICommandRunerRunCommandList(ButtonCommandsQueue commandnList)
        {
            foreach (ButtonCommand command in commandnList.Commands)
            {
                command.CommandRun();
                if (command is IFinalCommand) break;
            }
        }
    }
}
namespace HistSiGUI
{
    public static class DefaultMethods
    {
        public static IEnumerator DelayDestroy(IRemovable removableObject)
        {
            yield return new WaitForSeconds(removableObject.OnDestroyAnimation.clip.length);
            GameObject.Destroy(removableObject.DestroyedObject);
        }
    }
}
namespace HistSiCustomEditor
{
    public static class DefaultMethods
    {
        static void SerializeInterface<T>(ref T serializableInterface,ref MonoBehaviour sourceBehavior,string inspectorLabelText) where T:class
        {
            T oldInterface = serializableInterface;
            sourceBehavior = EditorGUILayout.ObjectField(inspectorLabelText,
                sourceBehavior, typeof(MonoBehaviour), true) as MonoBehaviour;
            if (sourceBehavior != null)
            {
                serializableInterface = sourceBehavior as T;
                if (serializableInterface==null&&!sourceBehavior.TryGetComponent(out serializableInterface))
                {
                    sourceBehavior = null;
                }
                else
                {
                    sourceBehavior = serializableInterface as MonoBehaviour;
                }
            }
            if (oldInterface != serializableInterface)
            {
                EditorUtility.SetDirty(sourceBehavior);
            }
        }
        static void SerializeObjectField<T>(ref T serializableObjField,string inspectorLabelText) where T:UnityEngine.Object
        {
            T oldObject = serializableObjField;
            serializableObjField = EditorGUILayout.ObjectField(inspectorLabelText, serializableObjField, typeof(T), true) as T;
            if (oldObject != serializableObjField)
            {
                EditorUtility.SetDirty(serializableObjField);
            }
        }
        public static void TextDependenceOnInspectorGUI<T>(Editor editor)
        {
            TextDependence<T> obj = (TextDependence<T>)editor.target;
            SerializeInterface(ref obj.valueGetter,ref obj.ValueSourceBehavior, "Value Source " + typeof(T).Name);
            SerializeObjectField(ref obj.ValueText, "Text Field");
        }
        public static void PairMathOperationOnInspectorGUI<T>(Editor editor) where T:struct
        {
            PairMathOperation<T> obj = (PairMathOperation<T>)editor.target;
            SerializedObject serializedObject = new SerializedObject(obj);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OperationType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("FirstOperandIsFunction"));
            if (obj.FirstOperandIsFunction)
            {
                SerializeInterface(ref obj.FirstSourceOperand, ref obj.FirstOperandSourseBehavior, "First Operand " + typeof(T));
            }
            else
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("FirstOperand"));
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("SecondOperandIsFunction"));
            if (obj.SecondOperandIsFunction)
            {
                SerializeInterface(ref obj.SecondSourceOperand, ref obj.SecondOperandSourseBehavior, "Second Operand " + typeof(T));
            }
            else
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("SecondOperand"));
            }
            serializedObject.ApplyModifiedProperties();
        }
        public static void SingleMathOperationOnInspectorGUI<T>(Editor editor)where T:struct
        {
            SingleMathOperation<T> obj =(SingleMathOperation<T>)editor.target;
            SerializedObject serializedObject = new SerializedObject(obj);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OperationType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OperandIsFunction"));
            if (obj.OperandIsFunction)
            {
                SerializeInterface(ref obj.SourceOperand, ref obj.OperandSourseBehavior, "Operand " + typeof(T));
            }
            else
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Operand"));
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
