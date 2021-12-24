using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HistSi
{
    public class HistSiInitialization : MonoBehaviour
    {
        protected static HistSiInitialization Singltone;
        public GameObject UICanvas;
        protected virtual void Awake()
        {
            if (Singltone == null)
            {
                Singltone = this;
                HistSi.UICanvas = UICanvas;
            }
            else
            {
                Destroy(this);
            }
        }
    }
}
