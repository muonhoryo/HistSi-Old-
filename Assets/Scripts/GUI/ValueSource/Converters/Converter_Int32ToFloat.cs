using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HistSiValueSources
{
    public class Converter_Int32ToFloat : Converter<int, float>
    {
        public override float Value => ValueSource.Value;
    }
}
