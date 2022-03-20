using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HistSiGUI;
using HistSi;

namespace HistSiCommands
{
    public class ThrowExceptionCommand : ButtonCommand
    {
        [SerializeField]
        protected string ExceptionMessage;
        public override void CommandRun()
        {
            HistSi.HistSi.ThrowError(ExceptionMessage);
        }
    }
}
