using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Test_application_insights.Services;
using Test_application_insights.View;

namespace Test_application_insights.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register(CreateNavigationService);
            SimpleIoc.Default.Register<IRepository,RepositoryStatic>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ContactDetailsViewModel>();
        }

        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public ContactDetailsViewModel ContactDetailsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ContactDetailsViewModel>();
            }
        }

        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure("Details", typeof(ContactDetailsView));            
            return navigationService;
        }
    }
}
