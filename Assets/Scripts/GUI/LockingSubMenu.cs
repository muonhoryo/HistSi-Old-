using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HistSiGUI
{
    public class LockingSubMenu : HistSiSubMenu,IButtonLocker
    {
        [SerializeField]
        protected byte lockedLayer;
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
