using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HistSi;

namespace HistSiCommands
{
    public class CallMessageAtPosCommand : CallMessageCommand
    {
        [SerializeField]
        protected Vector3 Position;
        public override void CommandRun()
        {
            CommandScripts.UIScripts.CallMessage(CallingMessage,Position);
        }
    }
}
