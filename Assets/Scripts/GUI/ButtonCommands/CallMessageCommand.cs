using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HistSi;
using HistSiGUI;

namespace HistSiCommands
{
    public class CallMessageCommand : ButtonCommand
    {
        [SerializeField]
        protected HistSiMessage CallingMessage;
        public override void CommandRun()
        {
            CommandScripts.UIScripts.CallMessage(CallingMessage);
        }
    }
}
