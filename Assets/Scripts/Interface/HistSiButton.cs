using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HistSiGUI
{
    public class HistSiButton : Selectable, ICommandRuner, IRemovable
    {
        public virtual void DefaultRun()
        {
            HistSi.HistSi.ThrowError("Have not command to run");
        }
        [SerializeField]
        protected Animation onDestroyAnimation;
        public virtual Animation OnDestroyAnimation => onDestroyAnimation;
        public GameObject DestroyedObject => gameObject;
        public void Remove()
        {
            HistSiInterfaces.DefaultMethods.IRemovableRemove(this,delegate { interactable = false; });
        }
        public virtual void RunCommand(ButtonCommand command)
        {
            command.CommandRun();
        }
        public virtual void RunCommandList(ButtonCommandsQueue CommandList)
        {
            HistSiInterfaces.DefaultMethods.ICommandRunerRunCommandList(CommandList);
        }
        Coroutine IRemovable.StartCoroutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }
    }
}
