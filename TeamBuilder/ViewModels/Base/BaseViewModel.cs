
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using TeamBuilder.Managers.Busy;
using TeamBuilder.Managers.Resource;

namespace TeamBuilder.ViewModels.Base
{
    public abstract partial class BaseViewModel : ObservableObject, IDisposable
    {
        #region Interfaces       
     
        protected readonly ILogger Logger;
        private readonly IBusyManager _busyManager;
        #endregion

        #region Constructors   

        public BaseViewModel()
        {
         
        }

        #region IDisposable

        public virtual void Dispose()
        {

        }

        #endregion IDisposable

        #endregion

        #region Properties

        [ObservableProperty]
        private string? title;

        [ObservableProperty]
        private bool isBusy = false;

        [ObservableProperty]
        private bool showPopup = false;

        [ObservableProperty]
        private string? popupTitle;

        [ObservableProperty]
        private string? popupText1;

        [ObservableProperty]
        private bool showFlyout = false;

        /// <summary>
        /// Gets the resource manager.
        /// </summary>
        internal IResourceManager ResourceManager { get; }

        #endregion

        #region Command Methods
        [RelayCommand]
        protected virtual void OnNavigate(string uri)
        {

        }
               
        [RelayCommand]
        protected virtual void OnGoBack() => Shell.Current.GoToAsync("..");



        #endregion

        #region Protected Methods

        /// <summary>
        /// Ons the appearing async.
        /// </summary>
        protected virtual void OnAppearingAsync()
        {
        }

        /// <summary>
        /// Ons the disappearing.
        /// </summary>
        protected virtual void OnDisappearing()
        {
        }

        /// <summary>
        /// Destroys the.
        /// </summary>
        protected virtual void Destroy()
        {
        }
        #endregion
              

        #region Public Methods

        #endregion
    }

    public abstract class BaseViewModel<TParameter> : BaseViewModel where TParameter : notnull
    {
        protected BaseViewModel(TParameter parameter)
        {
        }


        public virtual void Prepare(TParameter parameter)
        {
        }
    }
}
