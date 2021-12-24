using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HistSiValueSources
{
    public class ValueSource_Float : ValueSource<float>,IGetterValue<float>,ISetterValue<float>
    {
        class Source
        {
            public Source(Func<float> GetValueFunc,Action<float> SetValueAction)
            {
                this.GetValueFunc = GetValueFunc;
                this.SetValueAction = SetValueAction;
                OnValueChanged = delegate () { };
            }
            public readonly Func<float> GetValueFunc;
            public readonly Action<float> SetValueAction;
            public event Action OnValueChanged;
            public void RunEvent()
            {
                OnValueChanged();
            }
        }
        public enum SourceType_Float
        {
            MusicLevel,
            SoundLevel
        }
        readonly static Dictionary<SourceType_Float, Source> ValueSources_float = new Dictionary<SourceType_Float, Source>
        {
            {SourceType_Float.MusicLevel,new Source
                (   delegate{return HistSi.HistSi.MusicLevel; },
                    delegate(float value){HistSi.HistSi.MusicLevel=value; })},

            {SourceType_Float.SoundLevel,new Source
                (delegate{return HistSi.HistSi.SoundLevel; },
                delegate (float value){HistSi.HistSi.SoundLevel=value; }) }
        };
        [SerializeField]
        protected SourceType_Float ValueSourceType;
        public event Action OnValueChanged 
        {
            add=>ValueSources_float[ValueSourceType].OnValueChanged+=value;
            remove=>ValueSources_float[ValueSourceType].OnValueChanged -= value;
        }
        public override float Value 
        {
            get => ValueSources_float[ValueSourceType].GetValueFunc();
            set
            {
                ValueSources_float[ValueSourceType].SetValueAction(value);
                ValueSources_float[ValueSourceType].RunEvent();
            }

        }
    }
}
