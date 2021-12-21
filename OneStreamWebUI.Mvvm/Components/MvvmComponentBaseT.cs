using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using OneStreamWebUI.Mvvm.Parameters;
using OneStreamWebUI.Mvvm.ViewModel;

namespace OneStreamWebUI.Mvvm.Components
{
    public abstract class MvvmComponentBase<T> : MvvmComponentBase where T : ViewModelBase
    {
        private IViewModelParameterSetter? viewModelParameterSetter;
        
        public MvvmComponentBase(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            SetBindingContext();
        }

        public MvvmComponentBase() {}

        protected internal T BindingContext { get; set; } = null!;

        private void SetBindingContext()
        {
            BindingContext ??= ServiceProvider.GetRequiredService<T>();
        }

        private void SetParameters()
        {
            if (BindingContext is null)
            {
                throw new InvalidOperationException($"{nameof(BindingContext)} is not set");
            }
            viewModelParameterSetter ??= ServiceProvider.GetRequiredService<IViewModelParameterSetter>();
            viewModelParameterSetter.ResolveAndSet(this, BindingContext);
        }

        public TValue Bind<TValue>(Expression<Func<T, TValue>> property)
        {
            if (BindingContext is null)
            {
                throw new InvalidOperationException($"{nameof(BindingContext)} is not set");
            }
            return AddBinding(BindingContext, property);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SetBindingContext();
            SetParameters();
            BindingContext?.OnInitialized();
        }

        protected override Task OnInitializedAsync()
        {
            return BindingContext?.OnInitializedAsync() ?? Task.CompletedTask;
        }

        protected override void OnParametersSet()
        {
            SetParameters();
            BindingContext?.OnParametersSet();
        }

        protected override Task OnParametersSetAsync()
        {
            return BindingContext?.OnParametersSetAsync() ?? Task.CompletedTask;
        }

        protected override bool ShouldRender()
        {
            return BindingContext?.ShouldRender() ?? true;
        }

        protected override void OnAfterRender(bool firstRender)
        {
            BindingContext?.OnAfterRender(firstRender);
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return BindingContext?.OnAfterRenderAsync(firstRender) ?? Task.CompletedTask;
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(false);
            if (BindingContext != null)
            {
                await BindingContext.SetParametersAsync(parameters).ConfigureAwait(false);
            }
        }
    }
}