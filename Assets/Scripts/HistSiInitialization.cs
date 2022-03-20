using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HistSiValueSources;

namespace HistSi
{
    public class HistSiInitialization : MonoBehaviour,ISingltone<HistSiInitialization>
    {
        protected static HistSiInitialization singltone;
        public HistSiCustomValues CustomValues;
        public GameObject UICanvas;
        HistSiInitialization ISingltone<HistSiInitialization>.Singltone { get => singltone; set => singltone = value; }
        protected virtual void Awake()
        {
            Singltone.Awake(this, delegate { Destroy(this); }, delegate
              {
                  HistSi.CustomValues = CustomValues;
                  HistSi.UICanvas = UICanvas;
              });
        }
    }
}
