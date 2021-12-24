using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HistSi;

namespace HistSiCommands
{
    public class CloseMessageCommand : CallMessageCommand
    {
        public override void CommandRun()
        {
            CommandScripts.UIScripts.CloseMessage(CallingMessage);
        }
    }
}
