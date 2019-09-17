using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TestPrism.Entities;

namespace TestPrism.ViewModels
{
    public class PrismContentPage2ViewModel : BindableBase, INavigationAware
    {
        #region # Local variables

        private string _title;
        private string _name;
        private string _phone;

        #endregion
        #region # Properties

        public string Title
        { get { return _title; } set { SetProperty(ref _title, value); } }
        public string Name
        { get { return _name; } set { SetProperty(ref _name, value); } }
        public string Phone
        { get { return _phone; } set { SetProperty(ref _phone, value); } }

        #endregion

        #region # PrismContentPage2ViewModel constructor

        public PrismContentPage2ViewModel()
        {
            Title = "Страница2";
        }
        #endregion

        #region # Принимаем параметры с другой страницы
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Title = parameters.GetValue<string>("title");
            //var friends = parameters.GetValue<List<Friend>>("frends");
            //var friend = friends.FirstOrDefault();
            var friend = parameters.GetValue<Friend>("friend");

            Name = friend.Name;
            Phone = friend.Phone;
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {
            
        }

        #endregion
    }
}
