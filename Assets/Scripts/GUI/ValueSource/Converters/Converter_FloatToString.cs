using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HistSiValueSources
{
    public class Converter_FloatToString : Converter<float, string>
    {
        public override string Value => ValueSource.Value.ToString();
    }
}
