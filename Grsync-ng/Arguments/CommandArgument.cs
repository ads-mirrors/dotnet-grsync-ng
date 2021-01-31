using System;
using System.Collections;
using System.Collections.Generic;

namespace Grsyncng
{

    public abstract class CommandArgument
    {
        private object _value;
        public object Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
                OnValueChanged?.Invoke(this);
            }
        }

        private bool _interactable;
        public bool Interactable
        {
            get
            {
                return this._interactable;
            }
            set
            {
                this._interactable = value;
                OnInteractableChanged?.Invoke(this);
            }
        }

        public delegate void ValueSetHandler(object sender);
        public event ValueSetHandler OnValueChanged;
        public event ValueSetHandler OnInteractableChanged;

        public abstract string GetCommandPart();

    }
}

