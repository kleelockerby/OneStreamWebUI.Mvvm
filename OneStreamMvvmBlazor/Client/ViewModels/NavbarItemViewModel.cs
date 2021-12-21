﻿using OneStreamWebUI.Mvvm.ViewModel;

namespace OneStreamMvvmBlazor.Client
{
    public class NavbarItemViewModel : ViewModelBase
    {
        private bool _isActive;

        public NavbarItemViewModel(string displayName, string template, string? icon)
        {
            DisplayName = displayName;
            Template = template;
            Icon = icon;
        }

        public string DisplayName { get; }

        public string Template { get; }

        public string? Icon { get; set; }

        public bool IsActive
        {
            get => _isActive;
            set => Set(ref _isActive, value, nameof(IsActive));
        }
    }
}