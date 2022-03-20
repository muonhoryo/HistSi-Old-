using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HistSiGUI;

namespace HistSiInterfaces
{
    public static class ExtensionMethods
    {
        public static void RunCommandQueue(this ButtonCommandsQueue commandsQueue)
        {
            foreach (ButtonCommand command in commandsQueue.Commands)
            {
                command.CommandRun();
                if (command is IFinalCommand) break;
            }
        }
    }
}
