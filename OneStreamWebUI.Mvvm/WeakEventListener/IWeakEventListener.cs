using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStreamWebUI.Mvvm.WeakEventListener
{
    public interface IWeakEventListener
    {
        bool IsAlive { get; }
        object? Source { get; }
        Delegate? Handler { get; }
        void StopListening();
    }
}
