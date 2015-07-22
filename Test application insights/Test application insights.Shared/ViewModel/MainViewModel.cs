using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Test_application_insights.Model;
using Test_application_insights.Services;

namespace Test_application_insights.ViewModel
{
    public class MainViewModel : AppViewModelBase
    {
        #region Variables
        private readonly INavigationService _navigationService;
        private readonly IRepository _repository;
        private ObservableCollection<Contact> _contacts;
        #endregion

        #region Properties

        public ObservableCollection<Contact> Contacts
        {
            get { return _contacts; }
            set
            {
                _contacts = value;
                RaisePropertyChanged(()=>Contacts);
            }
        } 
        #endregion

        #region Commands
        private ICommand _contactSelectedCommand;
        public ICommand ContactSelectedCommand
        {
            get { return _contactSelectedCommand; }
        }

        private ICommand _addContactCommand;
        public ICommand AddContactCommand
        {
            get { return _addContactCommand; }
        }
        #endregion

        #region Constructor
        public MainViewModel(INavigationService navigationService, IRepository repository)
        {
            _navigationService = navigationService;
            _repository = repository;
            InitializeCommands();
            InitializeViewModel();
        }
        #endregion

        #region Private Methods
        public void InitializeCommands()
        {
            _contactSelectedCommand = new RelayCommand<ItemClickEventArgs>(args =>
            {
                if (!(args.ClickedItem is Contact)) return;
                var clickedItem = (Contact) args.ClickedItem;
                _navigationService.NavigateTo("Details", clickedItem.Id);
            });

            _addContactCommand = new RelayCommand(async () =>
            {
                if (Contacts.Count < 10)
                {
                    var properties = new Dictionary<string, string>();
                    var metrics = new Dictionary<string, double>();
#if WINDOWS_PHONE_APP
                    properties.Add("App","Windows Phone");
                    GoogleAnalytics.EasyTracker.GetTracker()
                        .SendEvent("Windows Phone", "Add Contact", null, Contacts.Count);
#endif
#if WINDOWS_APP
                    properties.Add("App", "Windows App");
                    GoogleAnalytics.EasyTracker.GetTracker()
                        .SendEvent("Windows App", "Add Contact", null, Contacts.Count);
#endif
                    metrics.Add("Number of Contacts", Contacts.Count);
                    _telemetryClient.TrackEvent("Add Contact",properties,metrics);
                    Contacts.Add(await _repository.GetNextContact());
                }
                else
                {
                    try
                    {
                        throw new ContactLimitException();
                    }
                    catch (ContactLimitException e)
                    {
                        GoogleAnalytics.EasyTracker.GetTracker().SendException("More than 10 contacts", false);
                        _telemetryClient.TrackException(e);
                        new MessageDialog("Can´t add more than 10 contacts").ShowAsync();
                    }
                }
            });
        }
        public async void InitializeViewModel()
        {
            Contacts = new ObservableCollection<Contact>(await _repository.GetAllContacts());
        }
        #endregion

        #region Events Control
        public override void Navigated(object parameter)
        {
            GoogleAnalytics.EasyTracker.GetTracker().SendView("MainView");
        }
        #endregion
    }
}
