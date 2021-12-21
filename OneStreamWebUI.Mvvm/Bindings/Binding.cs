using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using OneStreamWebUI.Mvvm.WeakEventListener;

namespace OneStreamWebUI.Mvvm.Bindings
{
    public class Binding : IBinding
    {
        private readonly IWeakEventManager weakEventManager;
        private INotifyCollectionChanged? boundCollection;
        private bool isCollection;

        public Binding(INotifyPropertyChanged isource, PropertyInfo propertyInfo, IWeakEventManager iweakEventManager)
        {
            weakEventManager = iweakEventManager ?? throw new ArgumentNullException(nameof(iweakEventManager));
            Source = isource ?? throw new ArgumentNullException(nameof(isource));
            PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
        }

        public INotifyPropertyChanged Source { get; }
        public PropertyInfo PropertyInfo { get; }

        public event EventHandler? BindingValueChanged;

        public void Initialize()
        {
            isCollection = typeof(INotifyCollectionChanged).IsAssignableFrom(PropertyInfo.PropertyType);
            weakEventManager.AddWeakEventListener(Source, SourceOnPropertyChanged);
            AddCollectionBindings();
        }

        public object GetValue()
        {
            return PropertyInfo.GetValue(Source, null)!;
        }

        private void AddCollectionBindings()
        {
            if (!isCollection || !(GetValue() is INotifyCollectionChanged collection))
            {
                return;
            }

            weakEventManager.AddWeakEventListener(collection, CollectionOnCollectionChanged);
            boundCollection = collection;
        }

        private void SourceOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is null)
            {
                BindingValueChanged?.Invoke(this, EventArgs.Empty);
                return;
            }

            if (e.PropertyName != PropertyInfo.Name)
            {
                return;
            }

            if (isCollection)
            {
                if (boundCollection != null)
                {
                    weakEventManager.RemoveWeakEventListener(boundCollection);
                }
                AddCollectionBindings();
            }
            BindingValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private void CollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            BindingValueChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (boundCollection != null)
                {
                    weakEventManager.RemoveWeakEventListener(boundCollection);
                }
                weakEventManager.RemoveWeakEventListener(Source);
            }
        }

        public override string ToString()
        {
            return $"{PropertyInfo?.DeclaringType?.Name}.{PropertyInfo?.Name}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Binding b && ReferenceEquals(b.Source, Source) && b.PropertyInfo.Name == PropertyInfo.Name;
        }

        public override int GetHashCode()
        {
            var hash = 13;
            hash = hash * 7 + Source.GetHashCode();
            hash = hash * 7 + PropertyInfo.Name.GetHashCode(StringComparison.InvariantCulture);
            return hash;
        }

    }
}