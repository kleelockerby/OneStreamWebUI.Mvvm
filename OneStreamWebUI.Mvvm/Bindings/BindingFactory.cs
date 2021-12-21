using System.ComponentModel;
using System.Reflection;
using OneStreamWebUI.Mvvm.WeakEventListener;

namespace OneStreamWebUI.Mvvm.Bindings
{
    public interface IBindingFactory
    {
        IBinding Create(INotifyPropertyChanged source, PropertyInfo propertyInfo, IWeakEventManager weakEventManager);
    }

    public class BindingFactory : IBindingFactory
    {
        public IBinding Create(INotifyPropertyChanged source, PropertyInfo propertyInfo, IWeakEventManager weakEventManager)
        {
            return new Binding(source, propertyInfo, weakEventManager);
        }
    }
}