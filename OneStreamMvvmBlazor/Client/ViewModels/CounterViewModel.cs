﻿using OneStreamWebUI.Mvvm.ViewModel;

namespace OneStreamMvvmBlazor.Client
{
    public class CounterViewModel : ViewModelBase
    {
        private int _currentCount;

        public int CurrentCount
        {
            get => _currentCount;
            set => Set(ref _currentCount, value, nameof(CurrentCount));
        }

        public void IncrementCount()
        {
            CurrentCount++;
        }
    }
}