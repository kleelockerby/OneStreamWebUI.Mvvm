using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace OneStreamWebUI.Mvvm.WeakEventListener
{
    public interface IWeakEventManager
    {
        void AddWeakEventListener<T, TArgs>(T source, string eventName, Action<T, TArgs> handler) where T : class where TArgs : EventArgs;
        void AddWeakEventListener<T>(T source, Action<T, PropertyChangedEventArgs> handler) where T : class, INotifyPropertyChanged;
        void AddWeakEventListener<T>(T source, Action<T, NotifyCollectionChangedEventArgs> handler) where T : class, INotifyCollectionChanged;
        void RemoveWeakEventListener<T>(T source) where T : class;
        void ClearWeakEventListeners();
    }
}
