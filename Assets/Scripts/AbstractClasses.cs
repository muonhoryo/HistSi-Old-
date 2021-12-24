using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HistSi;
using System;

namespace HistSiGUI
{
    public abstract class ButtonCommand : MonoBehaviour
    {
        public abstract void CommandRun();
    }
}
namespace HistSiValueSources
{
    public abstract class ValueSource<T> : MonoBehaviour
    {
        public abstract T Value { get; set; }
    }
    public abstract class TextDependence<T> : MonoBehaviour, IValueListener<T>
    {
        public MonoBehaviour ValueSourceBehavior;
        public Text ValueText;
        public IGetterValue<T> valueGetter;
        public IGetterValue<T> ValueSource => valueGetter;
        public void OnValueChangedAction()
        {
            ValueText.text = valueGetter.Value.ToString();
        }
        protected virtual void Awake()
        {
            if (valueGetter == null)
            {
                valueGetter = (IGetterValue<T>)ValueSourceBehavior;
                if (valueGetter == null)
                {
                    HistSi.HistSi.ThrowError("Value Source Monobehavior does not inhert IGetterValue<" + typeof(T) + ">");
                    return;
                }
            }
        }
        protected virtual void Start()
        {
            valueGetter.OnValueChanged += OnValueChangedAction;
            OnValueChangedAction();
        }
        protected virtual void OnDestroy()
        {
            valueGetter.OnValueChanged -= OnValueChangedAction;
        }
    }
    public abstract class PairMathOperation<T> : MonoBehaviour,IGetterValue<T> where T:struct
    {
        protected abstract Func<T, T, T>[] MathOperations { get; }
        public enum MathOperationType
        {
            Add,
            Subtract,
            Multiply,
            Divide,
            Remainder,
            Pow,
            Root
        }
        public MathOperationType OperationType;

        public MonoBehaviour FirstOperandSourseBehavior;
        public T FirstOperand;
        public IGetterValue<T> FirstSourceOperand;
        public bool FirstOperandIsFunction;

        public MonoBehaviour SecondOperandSourseBehavior;
        public T SecondOperand;
        public IGetterValue<T> SecondSourceOperand;
        public bool SecondOperandIsFunction;
        public T Value 
        {
            get 
            {
                T x, y;
                if (FirstOperandIsFunction)
                {
                    x = FirstSourceOperand.Value;
                }
                else
                {
                    x = FirstOperand;
                }
                if (SecondOperandIsFunction)
                {
                    y = SecondSourceOperand.Value;
                }
                else
                {
                    y = SecondOperand;
                }
                return MathOperations[(int)OperationType](x, y);
            }
        }
        public event Action OnValueChanged
        {
            add
            {
                if(FirstOperandIsFunction && FirstSourceOperand != null)
                {
                    FirstSourceOperand.OnValueChanged += value;
                }
                if (SecondOperandIsFunction && SecondSourceOperand != null)
                {
                    SecondSourceOperand.OnValueChanged += value;
                }
            }
            remove
            {
                if (FirstOperandIsFunction && FirstSourceOperand != null)
                {
                    FirstSourceOperand.OnValueChanged -= value;
                }
                if (SecondOperandIsFunction && SecondSourceOperand != null)
                {
                    SecondSourceOperand.OnValueChanged -= value;
                }
            }
        }
        protected virtual void Awake()
        {
            if (FirstOperandIsFunction&& FirstSourceOperand == null)
            {
                FirstSourceOperand = (IGetterValue<T>)FirstOperandSourseBehavior;
                if (FirstSourceOperand == null)
                {
                    HistSi.HistSi.ThrowError("Value Source Monobehavior does not inhert IGetterValue<" + typeof(T) + ">");
                    return;
                }
            }
            if (SecondOperandIsFunction && SecondSourceOperand == null)
            {
                SecondSourceOperand = (IGetterValue<T>)SecondOperandSourseBehavior;
                if (SecondSourceOperand == null)
                {
                    HistSi.HistSi.ThrowError("Value Source Monobehavior does not inhert IGetterValue<" + typeof(T) + ">");
                    return;
                }
            }
        }
    }
    public abstract class SingleMathOperation<T>:MonoBehaviour,IGetterValue<T> where T : struct
    {
        protected abstract Func<T, T>[] MathOperations { get; }
        public enum MathOperationType
        {
            RoundUp,
            RoundDown
        }
        public MathOperationType OperationType;

        public MonoBehaviour OperandSourseBehavior;
        public T Operand;
        public IGetterValue<T> SourceOperand;
        public bool OperandIsFunction;
        public T Value 
        {
            get
            {
                T x;
                if (OperandIsFunction)
                {
                    x = SourceOperand.Value;
                }
                else
                {
                    x = Operand;
                }
                return MathOperations[(int)OperationType](x);
            }
        }
        public event Action OnValueChanged
        {
            add
            {
                if (OperandIsFunction && SourceOperand != null)
                {
                    SourceOperand.OnValueChanged += value;
                }
            }
            remove
            {
                if (OperandIsFunction && SourceOperand != null)
                {
                    SourceOperand.OnValueChanged -= value;
                }
            }
        }
        protected virtual void Awake()
        {
            if (OperandIsFunction && SourceOperand == null)
            {
                SourceOperand = (IGetterValue<T>)OperandSourseBehavior;
                if (SourceOperand == null)
                {
                    HistSi.HistSi.ThrowError("Value Source Monobehavior does not inhert IGetterValue<" + typeof(T) + ">");
                    return;
                }
            }
        }
    }
}