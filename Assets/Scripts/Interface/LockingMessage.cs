using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HistSiGUI
{
    public class LockingMessage : HistSiMessage, IButtonLocker
    {
        [SerializeField] protected byte lockedLayer = 0;
        public byte LockedLayer => lockedLayer;
        protected override void Awake()
        {
            base.Awake();
            ButtonLocker.LockButtons(lockedLayer);
        }
        protected virtual void OnDestroy()
        {
            ButtonLocker.UnlockButtons(lockedLayer);
        }
    }
}
