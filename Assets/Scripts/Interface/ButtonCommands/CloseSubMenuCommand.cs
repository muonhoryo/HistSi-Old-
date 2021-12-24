using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HistSi;

namespace HistSiCommands
{
    public class CloseSubMenuCommand : TransitToSubMenuCommand
    {
        public override void CommandRun()
        {
            CommandScripts.UIScripts.CloseSubMenu(CallingSubMenu);
        }
    }
}
