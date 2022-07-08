using Autofac;
using Autofac.Core;
using habahabamall.ViewModels.AboutUs;
using habahabamall.ViewModels.Bookmarks;
using habahabamall.ViewModels.Catalog;
using habahabamall.ViewModels.Detail;
using habahabamall.ViewModels.ErrorAndEmpty;
using habahabamall.ViewModels.Forms;
using habahabamall.ViewModels.History;
using habahabamall.ViewModels.Onboarding;
using habahabamall.ViewModels.Transaction;
using System;

namespace habahabamall.DataService
{
    public static class TypeLocator
    {
        #region Fields

        private static ILifetimeScope _rootScope;

        #endregion

        #region Methods

        public static T Resolve<T>()
        {
            return _rootScope == null ? throw new Exception("Bootstrapper hasn't been started!") : _rootScope.Resolve<T>(new Parameter[0]);
        }

        public static T Resolve<T>(Parameter[] parameters)
        {
            return _rootScope == null ? throw new Exception("Bootstrapper hasn't been started!") : _rootScope.Resolve<T>(parameters);
        }

        public static void Start()
        {
            if (_rootScope != null)
            {
                return;
            }

            ContainerBuilder containerBuilder = new ContainerBuilder();
            _ = containerBuilder.RegisterType<CartDataService>().As<ICartDataService>();
            _ = containerBuilder.RegisterType<CatalogDataService>().As<ICatalogDataService>();
            _ = containerBuilder.RegisterType<CategoryDataService>().As<ICategoryDataService>();
            _ = containerBuilder.RegisterType<ProductHomeDataService>().As<IProductHomeDataService>();
            _ = containerBuilder.RegisterType<UserDataService>().As<IUserDataService>();
            _ = containerBuilder.RegisterType<ApiTokenService>().As<IApiTokenService>();

            _ = containerBuilder.RegisterType<AboutUsViewModel>();
            _ = containerBuilder.RegisterType<CartPageViewModel>();
            _ = containerBuilder.RegisterType<WishlistViewModel>();
            _ = containerBuilder.RegisterType<CatalogPageViewModel>();
            _ = containerBuilder.RegisterType<CategoryPageViewModel>();
            _ = containerBuilder.RegisterType<ProductHomePageViewModel>();
            _ = containerBuilder.RegisterType<DetailPageViewModel>();
            _ = containerBuilder.RegisterType<NoInternetConnectionPageViewModel>();
            _ = containerBuilder.RegisterType<ForgotPasswordViewModel>();
            _ = containerBuilder.RegisterType<LoginPageViewModel>();
            _ = containerBuilder.RegisterType<LoginViewModel>();
            _ = containerBuilder.RegisterType<SignUpPageViewModel>();
            _ = containerBuilder.RegisterType<OnBoardingAnimationViewModel>();
            _ = containerBuilder.RegisterType<CheckoutPageViewModel>();
            _ = containerBuilder.RegisterType<PaymentViewModel>();
            _ = containerBuilder.RegisterType<MyOrdersPageViewModel>();

            _rootScope = containerBuilder.Build();
        }

        public static void Stop()
        {
            _rootScope.Dispose();
        }

        #endregion
    }
}