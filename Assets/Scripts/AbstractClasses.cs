using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HistSi;
using System;

namespace HistSiGUI
{
    /// <summary>
    /// Base of command component. Allows customize work behavior of ICommandRuner in inspector without coding.
    /// </summary>
    public abstract class ButtonCommand : MonoBehaviour
    {
        public abstract void CommandRun();
    }
}
namespace HistSiValueSources
{
    /// <summary>
    /// Base of source of value component. Allows customize,where to get the value to any action.
    /// </summary>
    /// <typeparam name="TValueType"></typeparam>
    public abstract class ValueSource<TValueType> : MonoBehaviour
    {
        public abstract TValueType Value { get; set; }
    }
    /// <summary>
    /// Base of text field with dependence of any value. When the value changed,text is updated with that value.
    /// In inspector assigned text field and ValueSource.
    /// </summary>
    /// <typeparam name="TValueType"></typeparam>
    public abstract class TextDependence<TValueType> : MonoBehaviour, IValueListener<TValueType>
    {
        /// <summary>
        /// Field to interface serialization.
        /// </summary>
        public MonoBehaviour GetterValueComponent;
        public Text ValueText;
        public IGetterValue<TValueType> valueGetter;
        public IGetterValue<TValueType> ValueSource => valueGetter;
        public void OnValueChanged()
        {
            ValueText.text = valueGetter.Value.ToString();
        }
        protected virtual void Awake()
        {
            DefaultMethods.InterfaceInitialization(ref valueGetter, ref GetterValueComponent);
        }
        protected virtual void Start()
        {
            valueGetter.ValueChangeEvent += OnValueChanged;
            OnValueChanged();
        }
        protected virtual void OnDestroy()
        {
            valueGetter.ValueChangeEvent -= OnValueChanged;
        }
    }
    /// <summary>
    /// Base of maths operations with two operand. Avaible operations are indicated in MathOperationType enum.
    /// In inspector assigned operands and type of operation. Operands can be IGetterValue(type of TValueType) or TValueType. 
    /// </summary>
    /// <typeparam name="TValueType"></typeparam>
    public abstract class PairMathOperation<TValueType> : MonoBehaviour,IGetterValue<TValueType> where TValueType:struct
    {
        /// <summary>
        /// Array of delegates of type "TValueType Func(TValueType first,TValueType second)".
        /// </summary>
        protected abstract Func<TValueType, TValueType, TValueType>[] MathOperations { get; }
        public enum MathOperationType
        {
            Add,
            Subtract,
            Multiply,
            Divide,
            Remainder,
            Pow,
            Root,
            RoundUpTo,
            RoundDownTo
        }
        public MathOperationType OperationType;
        //First operand
        public MonoBehaviour FirstGetterValueComponent;
        public TValueType FirstOperand;
        public IGetterValue<TValueType> FirstSourceOperand;
        public bool FirstOperandIsFunction;
        //Second operand
        public MonoBehaviour SecondGetterValueComponent;
        public TValueType SecondOperand;
        public IGetterValue<TValueType> SecondSourceOperand;
        public bool SecondOperandIsFunction;
        public TValueType Value 
        {
            get 
            {
                TValueType x, y;
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
        public event Action ValueChangeEvent
        {
            add
            {
                if(FirstOperandIsFunction && FirstSourceOperand != null)
                {
                    FirstSourceOperand.ValueChangeEvent += value;
                }
                if (SecondOperandIsFunction && SecondSourceOperand != null)
                {
                    SecondSourceOperand.ValueChangeEvent += value;
                }
            }
            remove
            {
                if (FirstOperandIsFunction && FirstSourceOperand != null)
                {
                    FirstSourceOperand.ValueChangeEvent -= value;
                }
                if (SecondOperandIsFunction && SecondSourceOperand != null)
                {
                    SecondSourceOperand.ValueChangeEvent -= value;
                }
            }
        }
        protected virtual void Awake()
        {
            if (FirstOperandIsFunction)
            {
                DefaultMethods.InterfaceInitialization(ref FirstSourceOperand, ref FirstGetterValueComponent);
            }
            if (SecondOperandIsFunction)
            {
                DefaultMethods.InterfaceInitialization(ref SecondSourceOperand, ref SecondGetterValueComponent);
            }
        }
    }
    /// <summary>
    /// Base of maths operations with one operand. Avaible operations are indicated in MathOperationType enum.
    /// In inspector assigned operand and type of operation. Operand can be IGetterValue(type of TValueType) or TValueType.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingleMathOperation<T>:MonoBehaviour,IGetterValue<T> where T : struct
    {
        /// <summary>
        /// Array of delegates of type "TValueType Func(TValueType value)".
        /// </summary>
        protected abstract Func<T, T>[] MathOperations { get; }
        public enum MathOperationType
        {
            RoundUp,
            RoundDown
        }
        public MathOperationType OperationType;

        public MonoBehaviour OperandSourseComponent;
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
        public event Action ValueChangeEvent
        {
            add
            {
                if (OperandIsFunction && SourceOperand != null)
                {
                    SourceOperand.ValueChangeEvent += value;
                }
            }
            remove
            {
                if (OperandIsFunction && SourceOperand != null)
                {
                    SourceOperand.ValueChangeEvent -= value;
                }
            }
        }
        protected virtual void Awake()
        {
            if (OperandIsFunction)
            {
                DefaultMethods.InterfaceInitialization(ref SourceOperand, ref OperandSourseComponent);
            }
        }
    }
    /// <summary>
    /// Base of converter component. Allows get value from ValueSource in type of TOutput.
    /// In inspector assigned ValueSource.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public abstract class Converter<TInput, TOutput> : MonoBehaviour, IGetterValue<TOutput>
    {
        public MonoBehaviour ValueSourceComponent;
        public IGetterValue<TInput> ValueSource;
        public abstract TOutput Value { get; }
        public event Action ValueChangeEvent
        {
            add { ValueSource.ValueChangeEvent += value; }
            remove { ValueSource.ValueChangeEvent -= value; }
        }
        protected virtual void Awake()
        {
            DefaultMethods.InterfaceInitialization(ref ValueSource,ref ValueSourceComponent);
        }
    }
}