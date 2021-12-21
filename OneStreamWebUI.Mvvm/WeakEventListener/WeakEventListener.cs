using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;

namespace OneStreamWebUI.Mvvm.WeakEventListener
{
    public class WeakEventListener<T, TArgs> : WeakEventListenerBase<T, TArgs> where T : class where TArgs : EventArgs
    {
        private readonly EventInfo eventInfo;

        public WeakEventListener(T source, string eventName, Action<T, TArgs> handler) : base(source, handler)
        {
            eventInfo = source.GetType().GetEvent(eventName) ?? throw new ArgumentException("Unknown Event Name", nameof(eventName));
            if (eventInfo.EventHandlerType == typeof(EventHandler<TArgs>))
            {
                eventInfo.AddEventHandler(source, new EventHandler<TArgs>(HandleEvent!));
            }
            else
            {
                eventInfo.AddEventHandler(source, Delegate.CreateDelegate(eventInfo.EventHandlerType!, this, nameof(HandleEvent)));
            }
        }

        protected override void StopListening(T source)
        {
            if (eventInfo.EventHandlerType == typeof(EventHandler<TArgs>))
            {
                eventInfo.RemoveEventHandler(source, new EventHandler<TArgs>(HandleEvent!));
            }
            else
            {
                eventInfo.RemoveEventHandler(source, Delegate.CreateDelegate(eventInfo.EventHandlerType!, this, nameof(HandleEvent)));
            }
        }
    }
}