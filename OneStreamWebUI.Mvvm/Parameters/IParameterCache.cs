﻿using System;
using System.Collections.Generic;

namespace OneStreamWebUI.Mvvm.Parameters
{
    public interface IParameterCache
    {
        ParameterInfo? Get(Type type);
        void Set(Type type, ParameterInfo info);
    }
}
