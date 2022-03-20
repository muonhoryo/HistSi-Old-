using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HistSiValueSources
{
    public class Converter_Int32ToString : Converter<int, string>
    {
        public override string Value => ValueSource.Value.ToString();
    }
}
