using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HistSiValueSources
{
    public class Converter_FloatToInt32 : Converter<float, int>
    {
        public override int Value => (int)ValueSource.Value;
    }
}
