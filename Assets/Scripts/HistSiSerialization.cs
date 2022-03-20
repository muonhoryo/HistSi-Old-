using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;
using UnityEngine;

namespace HistSiSerialization 
{
    public static class Serialization
    {
        private static void Add<TKey, TValue>(this Dictionary<TKey, TValue> dict, KeyValuePair<TKey, TValue> pair)
        {
            dict.Add(pair.Key, pair.Value);
        }
        private const string EndText = "\"End\":\"\"";
        [Serializable]
        struct Pair<T1,T2>
        {
            public Pair(T1 first,T2 second)
            {
                this.first = first;
                this.second = second;
            }
            public Pair(KeyValuePair<T1,T2> keyValuePair)
            {
                first = keyValuePair.Key;
                second = keyValuePair.Value;
            }
            public T1 first;
            public T2 second;
        }
        public static class DictionarySerializator
        {
            public static string Serialize<TKey, TValue>(Dictionary<TKey, TValue> serializableDict)
            {
                Pair<TKey, TValue>[] pairs = new Pair<TKey, TValue>[serializableDict.Count];
                {
                    int i = 0;
                    foreach (KeyValuePair<TKey, TValue> item in serializableDict)
                    {
                        pairs[i++] =new Pair<TKey, TValue>(item);
                    }
                }
                StringBuilder serializedDict = new StringBuilder("{\n\"KeyValuePairList\":\t[\n");
                for (int i = 0; i < pairs.Length; i++)
                {
                    StringBuilder serializedItem = new StringBuilder(JsonUtility.ToJson(pairs[i],true));
                    serializedItem = serializedItem.Insert(serializedItem.Length - 1, EndText);
                    serializedDict.Append(serializedItem);
                    if (i < pairs.Length - 1)
                    {
                        serializedDict.Append(",\n\t\t");
                    }
                }
                serializedDict.Append("\n]\n}");
                return serializedDict.ToString();
            }
            public static void Write<TKey, TValue>(string path,Dictionary<TKey, TValue> serializableDict)
            {
                using StreamWriter stream = new StreamWriter(path,false);
                stream.Write(Serialize(serializableDict));
                stream.Close();
            }
            public static Dictionary<TKey,TValue> Read<TKey,TValue>(string path)
            {
                Dictionary<TKey, TValue> deserializedDictionary = new Dictionary<TKey, TValue> { };
                bool arrayIsOpen = false;
                int start=-1;
                int symbolCount = 0;
                if (!File.Exists(path))
                {
                    using FileStream stream=File.Create(path);
                    stream.Close();
                }
                else
                {
                    List<Pair<int,int>> diapasons = new List<Pair<int,int>> { };
                    foreach (string line in File.ReadLines(path))
                    {
                        if (arrayIsOpen)
                        {
                            int i;
                            if (start == -1)
                            {
                                i = line.IndexOf("{");
                                if (i != -1)
                                {
                                    start = i+symbolCount;
                                }
                            }
                            else
                            {
                                i = line.IndexOf(EndText);
                                if (i != -1)
                                {
                                    i += symbolCount;
                                    diapasons.Add(new Pair<int,int>(start, i));
                                    start = -1;
                                }
                            }
                        }
                        else
                        {
                            if (line.Contains("["))
                            {
                                arrayIsOpen = true;
                            }
                        }
                        symbolCount += line.Length+1;
                    }
                    using FileStream stream = new FileStream(path, FileMode.Open);
                    foreach (Pair<int,int> item in diapasons)
                    {
                        byte[] array = new byte[item.second-item.first];
                        stream.Seek(item.first, SeekOrigin.Begin);
                        stream.Read(array, 0, array.Length);
                        var keyValuePair = JsonUtility.FromJson<Pair<TKey, TValue>>(Encoding.UTF8.GetString(array) + "}");
                        if (!deserializedDictionary.ContainsKey(keyValuePair.first))
                        {
                            deserializedDictionary.Add(new KeyValuePair<TKey, TValue>(keyValuePair.first, keyValuePair.second));
                        }
                    }
                    stream.Close();
                }
                return deserializedDictionary;
            }
            public static Dictionary<TKey, TValue> Deserialize<TKey,TValue>(string serializedDict)
            {
                Dictionary<TKey, TValue> deserializedDictionary = new Dictionary<TKey, TValue> { };
                int start = serializedDict.IndexOf("[");
                while (true)
                {
                    start = serializedDict.IndexOf("{",start+1);
                    if (start == -1)
                    {
                        break;
                    }
                    var keyValuePair = JsonUtility.FromJson<Pair<TKey,TValue>>
                        (serializedDict.Substring(start, serializedDict.IndexOf(EndText, start))+"}");
                    deserializedDictionary.Add(new KeyValuePair<TKey,TValue>(keyValuePair.first,keyValuePair.second));
                }
                return deserializedDictionary;
            }
        }
    }
}
