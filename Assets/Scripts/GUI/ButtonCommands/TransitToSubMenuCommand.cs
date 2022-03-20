using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HistSiGUI;
using HistSi;

namespace HistSiCommands
{
    public class TransitToSubMenuCommand : ButtonCommand
    {
        [SerializeField]
        protected HistSiSubMenu CallingSubMenu;
        public override void CommandRun()
        {
            CommandScripts.UIScripts.TransitToSubMenu(CallingSubMenu);
        }
    }
}
