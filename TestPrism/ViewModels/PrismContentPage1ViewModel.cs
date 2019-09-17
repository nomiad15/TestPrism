using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TestPrism.Entities;

namespace TestPrism.ViewModels
{
    public class PrismContentPage1ViewModel : ViewModelBase, IConfirmNavigationAsync//BindableBase
    {
        #region # Local variables

        private readonly IPageDialogService _pageDialogServise;
        private readonly INavigationService _navigationService;
        private List<Friend> _listFr;

        #endregion
        #region # Properties
        // данные в listView
        public List<Friend> ListFr { get { return _listFr; } set { SetProperty(ref _listFr, value); } }

        #endregion
        #region # Commands

        private DelegateCommand _navigateComand;
        private DelegateCommand<Friend> _selFriendCommand;
        // нажатие
        public DelegateCommand NavigateComand => _navigateComand ?? (_navigateComand = new DelegateCommand(ExecuteNavCommand));
        public DelegateCommand<Friend> SelectFriendCommand => _selFriendCommand ?? (_selFriendCommand = new DelegateCommand<Friend>(ShowFriendDetails));
        public DelegateCommand ClickCommand { get; }
        //или
        //public ICommand ClickCommand
        //{ get; private set; }
        
        #endregion

        #region # PrismContentPage1ViewModel constructor
        public PrismContentPage1ViewModel(INavigationService navigationService, IPageDialogService pageDialogServise)
            : base(navigationService)
        {
            Title = "Страница1";

            ClickCommand = new DelegateCommand(ClickedMethod);

            _navigationService = navigationService;
            _pageDialogServise = pageDialogServise;

            ListFr = GetFriends();
        }
        #endregion

        #region # Нажатие на кнопку
        private void ClickedMethod()
        {
            //TestModel.Message = "You Have Clicked the button";
            var fr = GetFriends();
        }
        #endregion
        #region # Переход на страницу PrismContentPage2
        async void ExecuteNavCommand()
        {
            // передача параметров
            var p1 = new NavigationParameters();
            p1.Add("title", "Привет из Page1");
            p1.Add("frends", GetFriends());

            //var p2 = new NavigationParameters("id=1&name=Vasya&color=black");

            await _navigationService.NavigateAsync("PrismContentPage2", p1); // 
        }
        // разрешение/запрет перехода на страницу
        public Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            return _pageDialogServise.DisplayAlertAsync("Сообщение", "Открыть стр2 ?", "ДА", "НЕТ");
        }
        #endregion

        #region # GetFriends()
        private List<Friend> GetFriends()
        {
            var result = new List<Friend>()
            {
                new Friend()
                {
                    Name = "Петя",
                    Phone = "777"
                },
                new Friend()
                {
                    Name = "Дуся",
                    Phone = "555"
                }
            };
            return result;
        }
        #endregion

        #region # Передача параметров и Переход на страницу PrismContentPage2
        private async void ShowFriendDetails(Friend paramData)
        {
            var parameters = new NavigationParameters
            {
                { "friend", paramData }
            };

            await _navigationService.NavigateAsync("PrismContentPage2", parameters);
        }
        #endregion
    }
}
