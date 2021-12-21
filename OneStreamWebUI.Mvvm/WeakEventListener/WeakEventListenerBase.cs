using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;

namespace OneStreamWebUI.Mvvm.WeakEventListener
{
    public abstract class WeakEventListenerBase<T, TArgs> : IWeakEventListener where T : class where TArgs : EventArgs
    {
        private readonly WeakReference<Action<T, TArgs>> handler;
        private readonly WeakReference<T> source;

        protected WeakEventListenerBase(T sourceT, Action<T, TArgs> handlerT)
        {
            source = new WeakReference<T>(sourceT ?? throw new ArgumentNullException(nameof(sourceT)));
            handler = new WeakReference<Action<T, TArgs>>(handlerT ?? throw new ArgumentNullException(nameof(handlerT)));
        }

        public bool IsAlive => handler.TryGetTarget(out _) && source.TryGetTarget(out _);

        public object? Source
        {
            get
            {
                if (source.TryGetTarget(out var sourceOut))
                {
                    return sourceOut;
                }
                return null;
            }
        }

        public Delegate? Handler
        {
            get
            {
                if (handler.TryGetTarget(out var handlerOut))
                {
                    return handlerOut;
                }
                return null;
            }
        }

        public void StopListening()
        {
            if (source.TryGetTarget(out var sourceOut))
            {
                StopListening(sourceOut);
            }
        }

        protected void HandleEvent(object sender, TArgs e)
        {
            if (handler.TryGetTarget(out var handlerOut))
            {
                handlerOut((T)sender, e);
            }
            else
            {
                StopListening();
            }
        }

        protected abstract void StopListening(T source);
    }
}
