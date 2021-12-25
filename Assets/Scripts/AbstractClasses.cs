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
            DefaultMethods.InterfaceInitialization(ref valueGetter, ref ValueSourceBehavior);
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
            if (FirstOperandIsFunction)
            {
                DefaultMethods.InterfaceInitialization(ref FirstSourceOperand, ref FirstOperandSourseBehavior);
            }
            if (SecondOperandIsFunction)
            {
                DefaultMethods.InterfaceInitialization(ref SecondSourceOperand, ref SecondOperandSourseBehavior);
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
            if (OperandIsFunction)
            {
                DefaultMethods.InterfaceInitialization(ref SourceOperand, ref OperandSourseBehavior);
            }
        }
    }
    public abstract class Converter<TInput, TOutput> : MonoBehaviour, IGetterValue<TOutput>
    {
        public MonoBehaviour ValueSourceBehavior;
        public IGetterValue<TInput> ValueSource;
        public abstract TOutput Value { get; }
        public event Action OnValueChanged
        {
            add { ValueSource.OnValueChanged += value; }
            remove { ValueSource.OnValueChanged -= value; }
        }
        protected virtual void Awake()
        {
            DefaultMethods.InterfaceInitialization(ref ValueSource,ref ValueSourceBehavior);
        }
    }
}