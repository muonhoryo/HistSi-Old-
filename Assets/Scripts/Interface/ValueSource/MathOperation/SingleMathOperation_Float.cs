using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HistSiValueSources
{
    public class SingleMathOperation_Float:SingleMathOperation<float>
    {
        private readonly Func<float, float>[] mathOperations = new Func<float, float>[2]
        {
            (x)=>(x+1)-x%1,
            (x)=>x-x%1
        };
        protected override Func<float, float>[] MathOperations => mathOperations;
    }
}
