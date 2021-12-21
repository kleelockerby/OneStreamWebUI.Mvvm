using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using OneStreamWebUI.Mvvm.Bindings;
using OneStreamWebUI.Mvvm.WeakEventListener;
using OneStreamWebUI.Mvvm.ViewModel;

namespace OneStreamWebUI.Mvvm.Components
{
    public abstract class MvvmComponentBase : ComponentBase, IDisposable, IAsyncDisposable
    {
        private IBindingFactory bindingFactory = null!;
        private HashSet<IBinding> bindings = new();
        private IWeakEventManager weakEventManager = null!;
        private IWeakEventManagerFactory weakEventManagerFactory = null!;

        private int noStateChanged = 1;

        [Inject] protected IServiceProvider ServiceProvider { get; set; } = null!;
        
        public MvvmComponentBase() { }
        
        public MvvmComponentBase(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            weakEventManagerFactory = ServiceProvider.GetRequiredService<IWeakEventManagerFactory>();
            bindingFactory = ServiceProvider.GetRequiredService<IBindingFactory>();
            weakEventManager = weakEventManagerFactory.Create();
        }

        public TValue Bind<TViewModel, TValue>(TViewModel viewModel, Expression<Func<TViewModel, TValue>> property) where TViewModel : ViewModelBase
        {
            return AddBinding(viewModel, property);
        }

        public virtual TValue AddBinding<TViewModel, TValue>(TViewModel viewModel, Expression<Func<TViewModel, TValue>> propertyExpression) where TViewModel : ViewModelBase
        {
            var propertyInfo = ValidateAndResolveBindingContext(viewModel, propertyExpression);

            var binding = bindingFactory.Create(viewModel, propertyInfo, weakEventManagerFactory.Create());
            if (bindings.Contains(binding))
            {
                return (TValue)binding.GetValue();
            }

            weakEventManager.AddWeakEventListener<IBinding, EventArgs>(binding, nameof(Binding.BindingValueChanged), BindingOnBindingValueChanged);
            binding.Initialize();
            bindings.Add(binding);

            return (TValue) binding.GetValue();
        }

        protected override void OnInitialized()
        {
            InitializeDependencies();
        }

        internal virtual void BindingOnBindingValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine($"StateHasChanged - MvvmComponentBase [{noStateChanged++}]");
            InvokeAsync(StateHasChanged);
        }

        protected static PropertyInfo ValidateAndResolveBindingContext<TViewModel, TValue>(TViewModel viewModel, Expression<Func<TViewModel, TValue>> property)
        {
            if (viewModel is null)
            {
                throw new BindingException("ViewModelType is null");
            }

            if (property is null)
            {
                throw new BindingException("Property expression is null");
            }

            if (!(property.Body is MemberExpression m))
            {
                throw new BindingException("Binding member needs to be a property");
            }

            if (!(m.Member is PropertyInfo p))
            {
                throw new BindingException("Binding member needs to be a property");
            }

            if (typeof(TViewModel).GetProperty(p.Name) is null)
            {
                throw new BindingException($"Cannot find property {p.Name} in type {viewModel.GetType().FullName}");
            }
            return p;
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
                if (bindings is not null)
                {
                    DisposeBindings();
                    bindings = null!;
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();

            Dispose(false);
            GC.SuppressFinalize(this);
        }

        protected virtual ValueTask DisposeAsyncCore()
        {
            if (bindings is not null)
            {
                DisposeBindings();
                bindings = null!;
            }

            return default;
        }

        private void DisposeBindings()
        {
            foreach (var binding in bindings)
            {
                weakEventManager.RemoveWeakEventListener(binding);
                binding.Dispose();
            }
        }

        ~MvvmComponentBase()
        {
            Dispose(false);
        }
    }
}