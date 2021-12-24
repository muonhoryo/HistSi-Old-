using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HistSiGUI
{
    public class HistSiMessage : MonoBehaviour, IRemovable
    {
        [SerializeReference]
        HistSiButton[] ChildrenButtons;
        [SerializeField]
        protected Animation onDestroyAnimation;
        public Animation OnDestroyAnimation => onDestroyAnimation;
        public GameObject DestroyedObject => gameObject;
        public void Remove()
        {
            HistSiInterfaces.DefaultMethods.IRemovableRemove(this, delegate ()
            {
                foreach (HistSiButton button in ChildrenButtons)
                {
                    button.interactable = false;
                }
            });
        }
        protected virtual void Awake()
        {
            ChildrenButtons = GetComponentsInChildren<HistSiButton>();
        }
        Coroutine IRemovable.StartCoroutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }
    }
}
