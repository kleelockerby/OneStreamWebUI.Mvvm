using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace OneStreamWebUI.Mvvm.WeakEventListener
{
    public class WeakEventManager : IWeakEventManager
    {
        private readonly Dictionary<IWeakEventListener, Delegate> listeners = new();

        public void AddWeakEventListener<T, TArgs>(T source, string eventName, Action<T, TArgs> handler) where T : class where TArgs : EventArgs
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            listeners.Add(new WeakEventListener<T, TArgs>(source, eventName, handler), handler);
        }

        public void AddWeakEventListener<T>(T source, Action<T, PropertyChangedEventArgs> handler) where T : class, INotifyPropertyChanged
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            listeners.Add(new PropertyChangedWeakEventListener<T>(source, handler), handler);
        }

        public void AddWeakEventListener<T>(T source, Action<T, NotifyCollectionChangedEventArgs> handler) where T : class, INotifyCollectionChanged
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            listeners.Add(new CollectionChangedWeakEventListener<T>(source, handler), handler);
        }

        public void RemoveWeakEventListener<T>(T source) where T : class
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            List<IWeakEventListener> toRemove = new List<IWeakEventListener>();

            foreach (var listener in listeners.Keys)
            {
                if (!listener.IsAlive)
                {
                    toRemove.Add(listener);
                }
                else if (listener.Source == source)
                {
                    listener.StopListening();
                    toRemove.Add(listener);
                }
            }

            foreach (var item in toRemove)
            {
                listeners.Remove(item);
            }
        }

        public void ClearWeakEventListeners()
        {
            foreach (var listener in listeners.Keys)
            {
                if (listener.IsAlive)
                {
                    listener.StopListening();
                }
            }
            listeners.Clear();
        }
    }
}