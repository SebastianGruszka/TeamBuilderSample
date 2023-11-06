using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TeamBuilder.Models.POCO;
using TeamBuilder.Resources.Resx;
using TeamBuilder.Services.Network;
using TeamBuilder.TeamMembers.Domain;
using TeamBuilder.ViewModels.Base;

namespace TeamBuilder.ViewModels.TeamMember
{
    /// <summary>
    /// The add team members view model.
    /// </summary>
    public partial class AddTeamMembersViewModel : BaseViewModel
    {
        #region Interfaces

        private readonly ITeamMembersRepository _repository;
        private readonly INetworkService _networkService;       
        private readonly ISecureStorage _secureStorage;
        #endregion

        #region Constructors
        public AddTeamMembersViewModel(ITeamMembersRepository repository,
                                       INetworkService networkService,
                                       ISecureStorage secureStorage)
        {
            _repository = repository;
            _networkService = networkService;
            _secureStorage = secureStorage;

            NewMembersList = new();
            Model = new();
            IsVisibleAddMoreMembersList = false;
            IsVisibleMainFrame = true;


            NamePlaceholder = AppResources.NamePlaceholder;
            NickNamePlaceholder = AppResources.NickNamePlaceholder;
            PositionPlaceholder = AppResources.PositionPlaceholder;
            TelephonePlaceholder = AppResources.PhoneNumberPlaceholder;
            AddNewButtonText = AppResources.AddNewText;
            SaveButtonText = AppResources.SaveText;
            Title = AppResources.AddMembersTitle;
        }
        #endregion

        #region Properties


        [ObservableProperty]
        private ObservableCollection<MemberModel> newMembersList;

        [ObservableProperty]
        private MemberModel model;

        [ObservableProperty]
        private bool isVisibleAddMoreMembersList;

        [ObservableProperty]
        private bool isVisibleMainFrame;


        [ObservableProperty]
        private string namePlaceholder;

        [ObservableProperty]
        private string nickNamePlaceholder;

        [ObservableProperty]
        private string positionPlaceholder;

        [ObservableProperty]
        private string telephonePlaceholder;

        [ObservableProperty]
        private string addNewButtonText;

        [ObservableProperty]
        private string saveButtonText;


        #endregion

        #region Private methods
        /// <summary>
        /// Adds the new member.
        /// </summary>
        [RelayCommand]
        private void AddNew()
        {
            if (IsVisibleAddMoreMembersList == false)            
                IsVisibleAddMoreMembersList = true;
         
            NewMembersList.Add(Model);
            ClearModel();
        }

        /// <summary>
        /// Saves members.
        /// </summary>
        [RelayCommand]
        private async void Save()
        {            
            await _repository.AddTeamMember(Model);
        }


        /// <summary>
        /// Clears the model.
        /// </summary>
        private void ClearModel()
        {
            Model.Name = string.Empty;
            Model.NickName = string.Empty;
            Model.Position = string.Empty;
            Model.PhoneNumber = string.Empty;
        }

        #endregion
    }
}
