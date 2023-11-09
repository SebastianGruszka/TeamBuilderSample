using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TeamBuilder.Models.POCO;
using TeamBuilder.Services.Network;
using TeamBuilder.TeamMembers.Application.AddTeamMembers;
using TeamBuilder.TeamMembers.Domain;
using TeamBuilder.ViewModels.Base;

namespace TeamBuilder.ViewModels.TeamMember
{
    public partial class TeamMembersViewModel : BaseViewModel
    {
        #region Interfaces

        private readonly ITeamMembersRepository _repository;
        private readonly INetworkService _networkService;

        #endregion

        #region Constructors
        public TeamMembersViewModel(ITeamMembersRepository repository,
                                    INetworkService networkService
                                   )
        {
            _repository = repository;
            _networkService = networkService;
            TeamMembers = new();

            LoadTeamMembers();
        }
        #endregion

        #region Properties
        [ObservableProperty]
        private ObservableCollection<MemberModel> teamMembers;
        #endregion

        #region Private methods
        [RelayCommand]
        private async void AddNewMembers()
        {
            await Shell.Current.GoToAsync(nameof(AddTeamMembersPage));
        }

        private async void LoadTeamMembers()
        {
            if (_networkService.HasNetwork())
            {
                try
                {
                    if (_networkService.HasNetwork() == true)
                    {
                        var response = await _repository.GetTeamMembers();

                        foreach (var item in response)
                        {
                            TeamMembers.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    Debug.WriteLine(ex.ToString());
#endif
                }
            }
            else
            {
                var list = await _repository.GetTeamMembers();
                foreach (var item in list)
                {
                    TeamMembers.Add(item);
                }
            }
        }


        void OnAppearing()
        {
            LoadTeamMembers();
        }



        #endregion
    }
}

