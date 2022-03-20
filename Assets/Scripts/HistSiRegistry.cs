using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HistSiGUI;
using HistSiValueSources;

namespace HistSi
{
    public static class CommandScripts
    {
        public static class UIScripts
        {
            public static HistSiMessage CallMessage(HistSiMessage message)
            {
                return GameObject.Instantiate(message.gameObject,HistSi.UICanvas.transform).GetComponent<HistSiMessage>();
            }
            public static HistSiMessage CallMessage(HistSiMessage message,Vector2 position)
            {
                HistSiMessage calledMessage = CallMessage(message);
                calledMessage.transform.position = position;
                return calledMessage;
            }
            public static void CloseMessage(HistSiMessage message)
            {
                message.Remove();
            }
            public static HistSiSubMenu TransitToSubMenu(HistSiSubMenu subMenu)
            {
                return GameObject.Instantiate(subMenu.gameObject, HistSi.UICanvas.transform).GetComponent<HistSiSubMenu>();
            }
            public static HistSiSubMenu TransitToSubMenu(HistSiSubMenu subMenu,Vector2 position)
            {
                HistSiSubMenu calledSubMenu = TransitToSubMenu(subMenu);
                calledSubMenu.transform.position = position;
                return calledSubMenu;
            }
            public static void CloseSubMenu(HistSiSubMenu subMenu)
            {
                subMenu.Remove();
            }
        }
        public static class MainMenuScripts
        {
            public static void Continue()
            {
                HistSi.ThrowError("Haven't game to continued");
            }
            public static void NewGame()
            {
                HistSi.ThrowError("New game cannot be started");
            }
            public static void Exit()
            {
                Application.Quit();
            }
            public static void GoToScene(string sceneName)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            }
        }
    }
    public static class HistSi
    {
        public delegate bool TryParser<T>(string x, out T y);
        public static HistSiCustomValues CustomValues;
        public static GameObject UICanvas;
        public const string SerializationDirectory = "Assets/Scripts/SerializationData";
        static float musicLevel;
        public static float MusicLevel 
        { 
            get=>musicLevel; 
            set
            {
                if (value < 0)
                {
                    musicLevel = 0;
                }
                else if (value > 1)
                {
                    musicLevel = 1;
                }
                else
                {
                    musicLevel = value;
                }
            } 
        }
        static float soundLevel;
        public static float SoundLevel
        {
            get => soundLevel;
            set
            {
                if (value < 0)
                {
                    soundLevel = 0;
                }
                else if (value > 1)
                {
                    soundLevel = 1;
                }
                else
                {
                    soundLevel = value;
                }
            }
        }
        public static void ThrowError(string errorMessage)
        {
            Debug.LogError(errorMessage);
        }
    }
}
namespace HistSiGUI
{
    public static class ButtonLocker
    {
        private readonly static List<byte> LockLayers = new List<byte> { };
        private readonly static Dictionary<byte, byte> LockersCount = new Dictionary<byte, byte> { };
        private readonly static Dictionary<byte, List<LockableButton>> Buttons = new Dictionary<byte, List<LockableButton>> { };
        public static bool IsExistLayer(byte layer)
        {
            return Buttons.ContainsKey(layer);
        }
        public static List<byte> GetLockLayers()
        {
            List<byte> list = new List<byte> { };
            list.AddRange(LockLayers);
            return list;
        }
        public static byte GetLockerCount(byte layer)
        {
            return LockersCount[layer];
        }
        public static List<LockableButton> GetButtons(byte layer)
        {
            List<LockableButton> buttons = new List<LockableButton> { };
            buttons.AddRange(Buttons[layer]);
            return buttons;
        }
        public static void AddButton(LockableButton button)
        {
            void CreateNewLayer(byte lockLayer)
            {
                LockersCount.Add(lockLayer, 0);
                Buttons.Add(lockLayer, new List<LockableButton> { });
                LockLayers.Add(lockLayer);
            }
            if (!Buttons.ContainsKey(button.LockLayer))
            {
                CreateNewLayer(button.LockLayer);
            }
            Buttons[button.LockLayer].Add(button);
        }
        public static void RemoveButton(LockableButton button)
        {
            if (Buttons.ContainsKey(button.LockLayer) && Buttons[button.LockLayer].Contains(button))
            {
                Buttons[button.LockLayer].Remove(button);
            }
        }
        public static void IncrementLockerCount(byte layer)
        {
            if (LockersCount[layer] == byte.MaxValue)
            {
                HistSi.HistSi.ThrowError("To many lockers on layer:" + layer);
            }
            else
            {
                LockersCount[layer]++;
            }
        }
        public static void DecrementLockerCount(byte layer)
        {
            if (LockersCount[layer] == 0)
            {
                HistSi.HistSi.ThrowError("Main menu buttons was not locked");
            }
            else
            {
                LockersCount[layer]--;
            }
        }
        public static void LockButtons(byte layer)
        {
            if (IsExistLayer(layer))
            {
                if (GetLockerCount(layer) == 0)
                {
                    foreach (LockableButton button in GetButtons(layer)) button.DeactivateButton();
                }
                IncrementLockerCount(layer);
            }
            else
            {
                HistSi.HistSi.ThrowError("Lock layer at index "+ layer + " does not exist" );
            }
        }
        public static void UnlockButtons(byte layer)
        {
            if (GetLockerCount(layer) == 1)
            {
                foreach (LockableButton button in GetButtons(layer)) button.ActivateButton();
            }
            DecrementLockerCount(layer);
        }
    }
    public static class SubMenuManager
    {
        private readonly static Dictionary<byte, HistSiSubMenu> SubMenu = new Dictionary<byte, HistSiSubMenu> { };
        public static void AddSubMenu(byte menuLayer,HistSiSubMenu subMenu)
        {
            if (!SubMenu.ContainsKey(menuLayer))
            {
                SubMenu.Add(menuLayer, null);
            }
            else if(SubMenu[menuLayer]!=null)
            {
                SubMenu[menuLayer].Remove();
            }
            SubMenu[menuLayer] = subMenu;
        }
        public static HistSiSubMenu GetSubMenu(byte menuLayer)
        {
            return SubMenu[menuLayer];
        }
    }
}