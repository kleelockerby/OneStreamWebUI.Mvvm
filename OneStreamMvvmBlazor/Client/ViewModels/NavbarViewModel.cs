using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AspNetCore.Components;
using OneStreamWebUI.Mvvm.ViewModel;

namespace OneStreamMvvmBlazor.Client
{
    public class NavbarViewModel : ViewModelBase
    {
        private readonly NavigationManager _navigationManager;

        private bool _isMenuOpen = true;

        public NavbarViewModel(INavbarService navbarService, NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            navigationManager.LocationChanged += (_, __) => UpdateActiveItem();
            NavbarItems = new ObservableCollection<NavbarItemViewModel>(navbarService.NavbarItems.Select(x => new NavbarItemViewModel(x.DisplayName, x.Template!, x.Icon)));
            UpdateActiveItem();
        }

        public ObservableCollection<NavbarItemViewModel> NavbarItems { get; }

        public bool IsMenuOpen
        {
            get => _isMenuOpen;
            set => Set(ref _isMenuOpen, value, nameof(IsMenuOpen));
        }

        public void ToggleMenu()
        {
            IsMenuOpen = !IsMenuOpen;
        }

        private void UpdateActiveItem()
        {
            var relativePath = _navigationManager.ToBaseRelativePath(_navigationManager.Uri);

            foreach (var navbarItem in NavbarItems)
                if (string.IsNullOrEmpty(relativePath))
                    navbarItem.IsActive = navbarItem.Template == "/";
                else
                    navbarItem.IsActive = navbarItem.Template.StartsWith("/" + relativePath);
        }
    }
}
