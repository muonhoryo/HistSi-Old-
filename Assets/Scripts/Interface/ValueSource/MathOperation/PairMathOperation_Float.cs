using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HistSiValueSources
{
    public class PairMathOperation_Float : PairMathOperation<float>
    {
        private readonly Func<float, float, float>[] mathOperations = new Func<float, float, float>[7]
        {
            ( x, y)=>x+y,
            (x, y)=>x-y,
            (x,y)=>x*y,
            (x,y)=>x/y,
            (x,y)=>x%y,
            (x,y)=>(float)Math.Pow(x,y),
            (x,y)=>(float)Math.Pow(x,1/y)
        };
        protected override Func<float, float, float>[] MathOperations { get => mathOperations;}
    }
}
