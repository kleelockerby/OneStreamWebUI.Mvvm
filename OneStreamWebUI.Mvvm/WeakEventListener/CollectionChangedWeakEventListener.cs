using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;

namespace OneStreamWebUI.Mvvm.WeakEventListener
{
    public class CollectionChangedWeakEventListener<T> : WeakEventListenerBase<T, NotifyCollectionChangedEventArgs> where T : class, INotifyCollectionChanged
    {
        public CollectionChangedWeakEventListener(T source, Action<T, NotifyCollectionChangedEventArgs> handler) : base(source, handler)
        {
            source.CollectionChanged += HandleEvent!;
        }

        protected override void StopListening(T source)
        {
            source.CollectionChanged -= HandleEvent!;
        }
    }
}
