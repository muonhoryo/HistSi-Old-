using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HistSiSerialization;

namespace HistSiValueSources
{
    [CreateAssetMenu]
    public sealed class HistSiCustomValues:ScriptableObject, IDictionarySerializeHelper<string,int>,IDictionarySerializeHelper<string,float>,
        IDictionarySerializeHelper<string,string>,IDictionarySerializeHelper<string,bool>
    {
        public string temporalKey_String;
        public string TemporalKey { get=> temporalKey_String; set=> temporalKey_String = value; }
        const string serializationPath_Int32 = HistSi.HistSi.SerializationDirectory + "/CustomValues_Int32.json";
        string IDictionarySerializeHelper<string,int>.SerializationPath{ get => serializationPath_Int32; }
        [SerializeField]
        private int temporalValue_Int32;
        public int TemporalValue { get => temporalValue_Int32; set => temporalValue_Int32 = value; }
        public Dictionary<string, int> CustomValues_Int32;

        [SerializeField]
        private float temporalValue_Single;
        float IDictionarySerializeHelper<string, float>.TemporalValue
        { get => temporalValue_Single; set => temporalValue_Single=value; }
        const string serializationPath_Float = HistSi.HistSi.SerializationDirectory + "/CustomValues_Float.json";
        string IDictionarySerializeHelper<string, float>.SerializationPath { get => serializationPath_Float; }
        public Dictionary<string, float> CustomValues_Float;

        [SerializeField]
        private string temporalValue_String;
        string IDictionarySerializeHelper<string, string>.TemporalValue 
        { get => temporalValue_String; set => temporalValue_String=value; }
        const string serializationPath_String = HistSi.HistSi.SerializationDirectory + "/CustomValues_String.json";
        string IDictionarySerializeHelper<string, string>.SerializationPath { get => serializationPath_String; }
        public Dictionary<string, string> CustomValues_String;

        [SerializeField]
        private bool temporalValue_Boolean;
        bool IDictionarySerializeHelper<string, bool>.TemporalValue
        { get => temporalValue_Boolean; set => temporalValue_Boolean=value; }
        const string serializationPath_Bool = HistSi.HistSi.SerializationDirectory + "/CustomValues_Bool.json";
        string IDictionarySerializeHelper<string, bool>.SerializationPath { get => serializationPath_Bool; }
        public Dictionary<string, bool> CustomValues_Bool;
    }
}
