using GalaSoft.MvvmLight;
using Microsoft.ApplicationInsights;
using Test_application_insights.Common;

namespace Test_application_insights.ViewModel
{
    public class AppViewModelBase : ViewModelBase, INavigable
    {
        #region Variables
        protected TelemetryClient _telemetryClient;
        #endregion

        #region Constructor
        public AppViewModelBase()
        {
            _telemetryClient = new TelemetryClient();
        }
        #endregion

        #region Events Control
        public virtual void Navigated(object parameter)
        {
        }

        public virtual void Left(object parameter)
        {
        }
        #endregion
    }
}
