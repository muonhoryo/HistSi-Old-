using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HistSiInterfaces;
using HistSiGUI;

namespace HistSiGUI
{
    public interface IFinalCommand { }
    public interface ICommandRuner
    {
        void DefaultRun();
        void RunCommandList(ButtonCommandsQueue commandList);
        void RunCommand(ButtonCommand command);
    }
    public interface IButtonLocker
    {
        byte LockedLayer { get; }
    }
    public interface IRemovable
    {
        Animation OnDestroyAnimation { get; }
        GameObject DestroyedObject { get; }
        void Remove();
        Coroutine StartCoroutine(IEnumerator routine);
    }
}
namespace HistSiValueSources
{
    public interface IValueListener<T>
    {
        IGetterValue<T> ValueSource { get; }
        void OnValueChangedAction();
    }
    public interface IGetterValue<T>
    {
        event Action OnValueChanged;
        T Value { get; }
    }
    public interface ISetterValue<T>
    {
        T Value {set; }
    }
}
