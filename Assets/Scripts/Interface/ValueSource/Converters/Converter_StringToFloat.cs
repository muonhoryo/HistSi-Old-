using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HistSiValueSources
{
    public class Converter_StringToFloat : Converter<string, float>
    {
        public override float Value
        {
            get
            {
                return DefaultMethods.Converter_StringToTGetValue(ValueSource.Value, delegate (string x, out float y)
                 {
                     return float.TryParse(x, out y);
                 });
            }
        }
    }
}
