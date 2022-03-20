using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HistSi;
using HistSiGUI;

namespace HistSiCommands
{
    public class ContinueCommand : ButtonCommand, IFinalCommand
    {
        public override void CommandRun()
        {
            CommandScripts.MainMenuScripts.Continue();
        }
    }
}