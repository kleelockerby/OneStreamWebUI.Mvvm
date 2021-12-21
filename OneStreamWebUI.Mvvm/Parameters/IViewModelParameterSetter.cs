using System;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using OneStreamWebUI.Mvvm.ViewModel;

namespace OneStreamWebUI.Mvvm.Parameters
{
    public interface IViewModelParameterSetter
    {
        void ResolveAndSet(ComponentBase component, ViewModelBase viewModel);
    }
}
