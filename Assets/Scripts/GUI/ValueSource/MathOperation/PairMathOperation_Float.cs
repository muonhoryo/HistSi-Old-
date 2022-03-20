using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HistSiValueSources
{
    public class PairMathOperation_Float : PairMathOperation<float>
    {
        private readonly Func<float, float, float>[] mathOperations = new Func<float, float, float>[9]
        {
            ( x, y)=>x+y,
            (x, y)=>x-y,
            (x,y)=>x*y,
            (x,y)=>x/y,
            (x,y)=>x%y,
            (x,y)=>(float)Math.Pow(x,y),
            (x,y)=>(float)Math.Pow(x,1/y),
            delegate(float x,float y)
            {
                if (x % y == 0)
                {
                    return x;
                }
                else
                {
                    return x+1-x%y;
                }
            },
            delegate(float x,float y)
            {
                if (x % y == 0)
                {
                    return x;
                }
                else
                {
                    return x-x%y;
                }
            }
        };
        protected override Func<float, float, float>[] MathOperations { get => mathOperations;}
    }
}
