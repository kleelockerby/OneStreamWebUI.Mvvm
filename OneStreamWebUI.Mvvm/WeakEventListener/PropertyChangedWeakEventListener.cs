using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;

namespace OneStreamWebUI.Mvvm.WeakEventListener
{
    public class PropertyChangedWeakEventListener<T> : WeakEventListenerBase<T, PropertyChangedEventArgs> where T : class, INotifyPropertyChanged
    {
        public PropertyChangedWeakEventListener(T source, Action<T, PropertyChangedEventArgs> handler) : base(source, handler)
        {
            source.PropertyChanged += HandleEvent!;
        }

        protected override void StopListening(T source)
        {
            source.PropertyChanged -= HandleEvent!;
        }
    }
}
