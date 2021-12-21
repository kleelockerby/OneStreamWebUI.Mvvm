using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace OneStreamWebUI.Mvvm.Parameters
{
    public interface IParameterResolver
    {
        IEnumerable<PropertyInfo> ResolveParameters(Type memberType);
    }
}
