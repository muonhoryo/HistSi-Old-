using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HistSiValueSources
{
    public class PairMathOperation_Int32 : PairMathOperation<int>
    {
        private readonly Func<int, int, int>[] mathOperations = new Func<int, int, int>[9]
        {
            ( x, y)=>x+y,
            (x, y)=>x-y,
            (x,y)=>x*y,
            (x,y)=>x/y,
            (x,y)=>x%y,
            (x,y)=>(int)Math.Pow(x,y),
            (x,y)=>(int)Math.Pow(x,1/y),
            delegate(int x,int y)
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
            delegate(int x,int y)
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
        protected override Func<int, int, int>[] MathOperations { get => mathOperations; }
    }
}
