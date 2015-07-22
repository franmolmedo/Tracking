using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Test_application_insights.Model;
using Test_application_insights.Services;

namespace Test_application_insights.ViewModel
{
    public class ContactDetailsViewModel : AppViewModelBase
    {
        #region Variables
        private readonly IRepository _repository;
        private readonly INavigationService _navigationService;
        private Contact _contact;
        #endregion

        #region Properties
        public Contact Contact
        {
            get { return _contact; }
            set
            {
                _contact = value;
                RaisePropertyChanged(()=>Contact);
            }
        }
        #endregion

        #region Commands
        private ICommand _goBackCommand;
        public ICommand GoBackCommand
        {
            get { return _goBackCommand; }
        }
        #endregion

        #region Constructor
        public ContactDetailsViewModel(INavigationService navigationService, IRepository repository)
        {
            _navigationService = navigationService;
            _repository = repository;
            InitializeCommands();
        }
        #endregion

        #region Private Methods
        public void InitializeCommands()
        {
            _goBackCommand = new RelayCommand(() => _navigationService.GoBack());
        }

        #endregion

        #region Events Control
        public async override void Navigated(object parameter)
        {
            GoogleAnalytics.EasyTracker.GetTracker().SendView("ContactDetailsView");
            if (parameter is int)
            {
                var contactId = (int) parameter;
                Contact = await _repository.GetContactById(contactId);
            } 
        }
        #endregion
    }
}
