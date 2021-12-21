using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using OneStreamWebUI.Mvvm.WeakEventListener;

namespace OneStreamWebUI.Mvvm.Bindings
{
    public interface IBinding : IDisposable
    {
        INotifyPropertyChanged Source { get; }
        PropertyInfo PropertyInfo { get; }
        event EventHandler? BindingValueChanged;
        void Initialize();
        object GetValue();
    }
}
