using TeamBuilder.ViewModels.TeamMember;

namespace TeamBuilder.TeamMembers.Application.AddTeamMembers;

public partial class AddTeamMembersPage
{
    private AddTeamMembersViewModel _viewModel;
    public AddTeamMembersPage(AddTeamMembersViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }
}