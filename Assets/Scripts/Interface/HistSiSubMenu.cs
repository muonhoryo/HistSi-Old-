using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HistSiGUI
{
    public class HistSiSubMenu : MonoBehaviour,IRemovable
    {
        [SerializeField]
        protected HistSiButton[] ChildrenButtons;
        [SerializeField]
        protected byte MenuLayer;
        [SerializeField]
        protected Animation onDestroyAnimation;
        public Animation OnDestroyAnimation => onDestroyAnimation;
        public GameObject DestroyedObject => gameObject;
        public void Remove()
        {
            HistSiInterfaces.DefaultMethods.IRemovableRemove(this, delegate
            {
                foreach (HistSiButton button in ChildrenButtons)
                {
                    button.interactable = false;
                }
            });
        }
        protected virtual void Awake()
        {
            SubMenuManager.AddSubMenu(MenuLayer, this);
            ChildrenButtons = GetComponentsInChildren<HistSiButton>();
        }
        Coroutine IRemovable.StartCoroutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }
    }
}
