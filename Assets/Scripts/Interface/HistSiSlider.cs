using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HistSiValueSources;

namespace HistSiGUI
{
    public class HistSiSlider : Slider, IRemovable,ICommandRuner
    {
        public ValueSource<float> ValueSource;
        [SerializeField]
        protected Animation onDestroyAnimation;
        public Animation OnDestroyAnimation => onDestroyAnimation;
        public GameObject DestroyedObject => gameObject;
        public void Remove()
        {
            HistSiInterfaces.DefaultMethods.IRemovableRemove(this, delegate { interactable = false; });
        }
        Coroutine IRemovable.StartCoroutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }
        public void DefaultRun()
        {
            HistSi.HistSi.ThrowError("Have not command to run");
        }
        public void RunCommandList(ButtonCommandsQueue commandList)
        {
            HistSiInterfaces.DefaultMethods.ICommandRunerRunCommandList(commandList);
        }
        public void RunCommand(ButtonCommand command)
        {
            command.CommandRun();
        }
        protected override void Awake()
        {
            base.Awake();
            if (ValueSource != null)
            {
                onValueChanged.AddListener(new UnityEngine.Events.UnityAction<float>(
                    delegate (float value) { ValueSource.Value = value; }));
            }
            else
            {
                HistSi.HistSi.ThrowError("Value Source does not assigned");
            }
        }
    }
}
