using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HistSiGUI
{
    public class ButtonCommandsQueue : MonoBehaviour
    {
        [SerializeField]
        protected ButtonCommand[] commands;
        public ButtonCommand[] Commands { get => commands; }
    }
}
