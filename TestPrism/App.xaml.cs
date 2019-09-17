﻿using Prism;
using Prism.Ioc;
using TestPrism.Services;
using TestPrism.ViewModels;
using TestPrism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TestPrism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage"); //отбражение первой страницы
            //await NavigationService.NavigateAsync("NavigationPage/PrismContentPage1"); 
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<PrismContentPage1, PrismContentPage1ViewModel>();
            containerRegistry.RegisterForNavigation<PrismContentPage2, PrismContentPage2ViewModel>();
            containerRegistry.RegisterForNavigation<RealmDBPage, RealmDBPageViewModel>();
            containerRegistry.RegisterForNavigation<ServicePage, ServicePageViewModel>();

            //здесь также регистрируются службы (Services)
            //containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IBookService, BookService>();
        }
    }
}
