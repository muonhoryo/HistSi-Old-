using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using HistSiGUI;
using HistSiSerialization;
using HistSiValueSources;

namespace HistSiInterfaces
{
    public static class Removable
    {
        /// <summary>
        /// Remove owner without animation
        /// </summary>
        /// <param name="owner"></param>
        public static void Remove(IRemovable owner)
        {
            Remove(owner,() => { });
        }
        /// <summary>
        /// Remove owner. If destroy animation not equal null-remove after an delay equal destroy animation's length.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="beforeAnimStartAction"></param>
        public static void Remove(IRemovable owner,Action beforeAnimStartAction)
        {
            if (owner.OnDestroyAnimation == null)
            {
                GameObject.Destroy(owner.DestroyedObject);
            }
            else
            {
                beforeAnimStartAction();
                owner.OnDestroyAnimation.Play();
                owner.StartCoroutine(DelayDestroy(owner));
            }
        }
        /// <summary>
        /// Destroy destroyedObject after an delay equal destroy animation length.
        /// </summary>
        /// <param name="destroyedObject"></param>
        /// <returns></returns>
        public static IEnumerator DelayDestroy(IRemovable destroyedObject)
        {
            yield return new WaitForSeconds(destroyedObject.OnDestroyAnimation.clip.length);
            GameObject.Destroy(destroyedObject.DestroyedObject);
        }
    }
}
namespace HistSiValueSources
{
    public static class Converter
    {
        /// <summary>
        /// Tries to parse value to T type. If cannot parse,return default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="parser"></param>
        /// <returns></returns>
        public static T StringToValue<T>(string value, HistSi.HistSi.TryParser<T> parser) where T : struct
        {
            if (parser(value, out T x))
            {
                return x;
            }
            else
            {
                HistSi.HistSi.ThrowError("Cannot convert " + value + " to " + typeof(T));
                return default;
            }
        }
    }
    public static class DefaultMethods
    {
        //Translate serialized interfaceComponent like a TInterfaceType in serializableInterface
        public static void InterfaceInitialization<TInterfaceType>(ref TInterfaceType serializableInterface, ref MonoBehaviour interfaceComponent)
            where TInterfaceType:class
        {
            if (serializableInterface == null)
            {
                serializableInterface = interfaceComponent as TInterfaceType;
                if (serializableInterface == null)
                {
                    HistSi.HistSi.ThrowError("Value Source Monobehavior does not inhert <" + typeof(TInterfaceType) + ">");
                    return;
                }
            }
        }
    }
}
namespace HistSiCustomEditor
{
    public static class DefaultMethods 
    {
        public static void DrawInterface<T>(ref T serializableInterface,ref MonoBehaviour sourceComponent,
            string inspectorLabelText = "") where T : class
        {
            T oldInterface = serializableInterface;
            sourceComponent = EditorGUILayout.ObjectField(inspectorLabelText,
                sourceComponent, typeof(MonoBehaviour), true) as MonoBehaviour;
            if (sourceComponent != null)
            {
                serializableInterface = sourceComponent as T;
                if (serializableInterface == null && !sourceComponent.TryGetComponent(out serializableInterface))
                {
                    sourceComponent = null;
                }
                else
                {
                    sourceComponent = serializableInterface as MonoBehaviour;
                }
            }
            if (oldInterface != serializableInterface)
            {
                EditorUtility.SetDirty(sourceComponent);
            }
        }
        public static void DrawObjectField<T>(ref T serializableObjField, string inspectorLabelText = "") where T : UnityEngine.Object
        {
            T oldObject = serializableObjField;
            serializableObjField = EditorGUILayout.ObjectField(inspectorLabelText, serializableObjField, typeof(T), true) as T;
            if (oldObject != serializableObjField)
            {
                EditorUtility.SetDirty(serializableObjField);
            }
        }
        public static void DrawDictionary<TKey, TValue>(Dictionary<TKey, TValue> showingDictionary,
            SerializedObject serialiabledObj, IDictionarySerializeHelper<TKey, TValue> dictHelper, ref bool isShowingList,
            string inspectorLabelText = "")
        {
            if (isShowingList = EditorGUILayout.BeginFoldoutHeaderGroup(isShowingList, inspectorLabelText))
            {
                showingDictionary = Serialization.DictionarySerializator.Read<TKey, TValue>(dictHelper.SerializationPath);
                bool isChanged = false;
                TKey[] keyArray = new TKey[showingDictionary.Count];
                showingDictionary.Keys.CopyTo(keyArray, 0);
                for (int i = 0; i < showingDictionary.Count; i++)
                {
                    dictHelper.TemporalValue = showingDictionary[keyArray[i]];
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PropertyField(serialiabledObj.FindProperty("temporalValue_" + typeof(TValue).Name),
                        new GUIContent(keyArray[i].ToString()));
                    serialiabledObj.ApplyModifiedProperties();
                    if (GUILayout.Button("Remove"))
                    {
                        showingDictionary.Remove(keyArray[i]);
                        isChanged = true;
                    }
                    else if (!dictHelper.TemporalValue.Equals(showingDictionary[keyArray[i]]))
                    {
                        showingDictionary[keyArray[i]] = dictHelper.TemporalValue;
                        isChanged = true;
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.PropertyField(serialiabledObj.FindProperty("temporalKey_" + typeof(TKey).Name), new GUIContent("New dictionary element"));
                serialiabledObj.ApplyModifiedProperties();
                if (GUILayout.Button("Add") && dictHelper.TemporalKey != null && !showingDictionary.ContainsKey(dictHelper.TemporalKey))
                {
                    showingDictionary.Add(dictHelper.TemporalKey, default);
                    dictHelper.TemporalKey = default;
                    isChanged = true;
                }
                if (isChanged)
                {
                    Serialization.DictionarySerializator.Write(dictHelper.SerializationPath, showingDictionary);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}
namespace HistSi
{
    public static class Singltone
    {
        /// <summary>
        /// If example of TSingltonType is exist-execute destroyAction,else-execute awakeAction.
        /// </summary>
        /// <typeparam name="TSingltoneType"></typeparam>
        /// <param name="script"></param>
        /// <param name="destroyAction"></param>
        /// <param name="awakeAction"></param>
        public static void Awake<TSingltoneType>(ISingltone<TSingltoneType> script, Action destroyAction,
            Action awakeAction)
            where TSingltoneType : UnityEngine.Object
        {
            if (script.Singltone != null)
            {
                destroyAction();
            }
            else
            {
                script.Singltone = (TSingltoneType)script;
                awakeAction();
            }
        }
    }
}
