using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestPrism.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region # Local variables

        private readonly INavigationService _navigationService;

        #endregion
        #region # Commands

        //private DelegateCommand _navigateComand;//без параметров
        //public DelegateCommand NavigateComand => _navigateComand ?? (_navigateComand = new DelegateCommand(ExecuteNavCommand));
        private DelegateCommand<string> _navigateComand;
        public DelegateCommand<string> NavigateComand => _navigateComand ?? (_navigateComand = new DelegateCommand<string>(ExecuteNavCommand));

        #endregion
        
        #region # PrismContentPage1ViewModel constructor

        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Main Page";
            _navigationService = navigationService;
        }

        #endregion

        #region # Переход на страницу PrismContentPage1
        //без параметров
        //async void ExecuteNavCommand()
        //{
        //    await _navigationService.NavigateAsync("PrismContentPage1"); // - с возвратом Назад
            //await _navigationService.NavigateAsync("/NavigationPage/MainPage"); // - на главную без возврата Назад
        //}
        //с параметрами
        async void ExecuteNavCommand(string paramPage)
        {
            await _navigationService.NavigateAsync(paramPage);
        }
        #endregion
    }
}
