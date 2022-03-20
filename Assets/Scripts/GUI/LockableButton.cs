using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HistSiGUI
{
    public class LockableButton : HistSiButton
    {
        [SerializeField]
        protected byte lockLayer = 0;
        public byte LockLayer { get => lockLayer; }
        public virtual void ActivateButton()
        {
            interactable = true;
        }
        public virtual void DeactivateButton()
        {
            interactable = false;
        }
        protected override void Awake()
        {
            base.Awake();
            ButtonLocker.AddButton(this);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            ButtonLocker.RemoveButton(this);
        }
    }
}
