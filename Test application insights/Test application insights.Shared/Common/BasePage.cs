using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Test_application_insights.Common
{
    public class BasePage : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var navigableViewModel = DataContext as INavigable;
            if (navigableViewModel != null)
                navigableViewModel.Navigated(e.Parameter);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            var navigableViewModel = DataContext as INavigable;
            if (navigableViewModel != null)
                navigableViewModel.Left(e.Parameter);
        }
    }
}
