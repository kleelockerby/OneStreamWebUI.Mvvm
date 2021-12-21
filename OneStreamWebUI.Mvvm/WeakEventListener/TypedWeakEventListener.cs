using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;

namespace OneStreamWebUI.Mvvm.WeakEventListener
{
    public class TypedWeakEventListener<T, TArgs> : WeakEventListenerBase<T, TArgs> where T : class where TArgs : EventArgs
    {
        private readonly Action<T, EventHandler<TArgs>> unregister;

        public TypedWeakEventListener(T source, Action<T, EventHandler<TArgs>> register, Action<T, EventHandler<TArgs>> unregisterT, Action<T, TArgs> handler) : base(source, handler)
        {
            if (register == null)
            {
                throw new ArgumentNullException(nameof(register));
            }
            unregister = unregisterT ?? throw new ArgumentNullException(nameof(unregisterT));
            register(source, HandleEvent!);
        }

        protected override void StopListening(T source)
        {
            unregister(source, HandleEvent!);
        }
    }
}
