using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HistSiValueSources
{
    public class Converter_StringToInt32 : Converter<string, int>
    {
        public override int Value
        {
            get
            {
                return Converter.StringToValue(ValueSource.Value, delegate (string x, out int y)
                {
                    return int.TryParse(x, out y);
                });
            }
        }
    }
}
