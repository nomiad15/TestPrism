using Plugin.Toasts;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPrism.Entities;
using TestPrism.Services;
using Xamarin.Forms;

namespace TestPrism.ViewModels
{
    public interface IBackgroundService
    {
        void StartService();
        void StopService();
    }

    public class ServicePageViewModel : BindableBase
    {
        #region # Local variables

        IBookService _bookService;

        #endregion
        #region # Commands

        private DelegateCommand<string> _clickCommand;
        public DelegateCommand<string> ClickCommand => _clickCommand ?? (_clickCommand = new DelegateCommand<string>(ExClickCommand));

        #endregion

        #region # ServicePageViewModel constructor
        public ServicePageViewModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        #endregion

        #region # ExClickCommand 
        private void ExClickCommand(string param)
        {
            switch (param)
            {
                case "Toast":
                    Toast();
                    break;
                case "Servise":
                    Servise();
                    break;
                default:
                    break;
            }

        }
        #endregion

        #region # Toast Notification
        private async void Toast()
        {
            var options = new NotificationOptions()
            {
                Title = "Title",
                Description = "Some Description",
                IsClickable = false // Set to true if you want the result Clicked to come back (if the user clicks it)
            };
            var notification = DependencyService.Get<IToastNotificator>();
            var result = await notification.Notify(options);
        }
        #endregion
        #region # Servise
        private async void Servise()
        {
            //var Books = new List<Book>(await _bookService.GetBooks());
            var Books = await _bookService.GetBooks();
        }
        #endregion
    }
}
