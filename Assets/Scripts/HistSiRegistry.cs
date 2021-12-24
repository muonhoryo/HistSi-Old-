using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HistSi;
using HistSiGUI;
using System;

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
        public static GameObject UICanvas;
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
        readonly static List<byte> LockLayers = new List<byte> { };
        readonly static Dictionary<byte, byte> LockerCount = new Dictionary<byte, byte> { };
        readonly static Dictionary<byte, List<LockableButton>> Buttons = new Dictionary<byte, List<LockableButton>> { };
        public static bool IsExistLayer(byte lockLayer)
        {
            return Buttons.ContainsKey(lockLayer);
        }
        public static List<byte> GetLockLayers()
        {
            List<byte> list = new List<byte> { };
            list.AddRange(LockLayers);
            return list;
        }
        public static byte GetLockerCount(byte lockedLayer)
        {
            return LockerCount[lockedLayer];
        }
        public static List<LockableButton> GetButtons(byte lockedLayer)
        {
            List<LockableButton> buttons = new List<LockableButton> { };
            buttons.AddRange(Buttons[lockedLayer]);
            return buttons;
        }
        public static void AddButton(LockableButton button)
        {
            void CreateNewLayer(byte lockLayer)
            {
                LockerCount.Add(lockLayer, 0);
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
        public static void IncrementLockerCount(byte lockedLayer)
        {
            if (LockerCount[lockedLayer] == byte.MaxValue)
            {
                HistSi.HistSi.ThrowError("To many lockers on layer:" + lockedLayer);
            }
            else
            {
                LockerCount[lockedLayer]++;
            }
        }
        public static void DecrementLockerCount(byte lockedLayer)
        {
            if (LockerCount[lockedLayer] == 0)
            {
                HistSi.HistSi.ThrowError("Main menu buttons was not locked");
            }
            else
            {
                LockerCount[lockedLayer]--;
            }
        }
        public static void LockButtons(byte lockedLayer)
        {
            if (IsExistLayer(lockedLayer))
            {
                if (GetLockerCount(lockedLayer) == 0)
                {
                    foreach (LockableButton button in GetButtons(lockedLayer)) button.DeactivateButton();
                }
                IncrementLockerCount(lockedLayer);
            }
            else
            {
                HistSi.HistSi.ThrowError("Lock layer at index "+ lockedLayer + " does not exist" );
            }
        }
        public static void UnlockButtons(byte lockedLayer)
        {
            if (GetLockerCount(lockedLayer) == 1)
            {
                foreach (LockableButton button in GetButtons(lockedLayer)) button.ActivateButton();
            }
            DecrementLockerCount(lockedLayer);
        }
    }
    public static class SubMenuManager
    {
        readonly static List<byte> SubMenuLockLayers = new List<byte> { };
        readonly static Dictionary<byte, HistSiSubMenu> SubMenu = new Dictionary<byte, HistSiSubMenu> { };
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
        public static List<byte> GetSubMenuLockLayers()
        {
            List<byte> lockLayer = new List<byte> { };
            lockLayer.AddRange(SubMenuLockLayers);
            return lockLayer;
        }
    }
}