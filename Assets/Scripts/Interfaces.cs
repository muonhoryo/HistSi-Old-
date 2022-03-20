using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HistSiInterfaces;
using HistSiGUI;
using HistSiSerialization;

namespace HistSiGUI
{
    /// <summary>
    /// Tag of Command. After execution this command in default realization
    /// ICommandRuner he is stops execution of commands in his commands list.
    /// </summary>
    public interface IFinalCommand { }
    /// <summary>
    /// Execute command or commands list.
    /// </summary>
    public interface ICommandRuner
    {
        void DefaultRun();
        /// <summary>
        /// Execute commandsList.
        /// </summary>
        /// <param name="commandsList"></param>
        void RunCommandList(ButtonCommandsQueue commandsList);
        /// <summary>
        /// Execute command.
        /// </summary>
        /// <param name="command"></param>
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
        void OnValueChanged();
    }
    public interface IGetterValue<T>
    {
        event Action ValueChangeEvent;
        T Value { get; }
    }
    public interface ISetterValue<T>
    {
        T Value {set; }
    }
    public interface IDictionarySerializeHelper<TKey,TValue>
    {
        string SerializationPath { get; }
        TKey TemporalKey { get; set; }
        TValue TemporalValue { get; set; }
    }
}
namespace HistSi
{
    public interface ISingltone<TSingltoneType> where TSingltoneType:UnityEngine.Object
    {
        public TSingltoneType Singltone { get; set; }
    }
}
