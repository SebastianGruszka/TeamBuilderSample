using TeamBuilder.ViewModels.TeamMember;

namespace TeamBuilder.TeamMembers.Application;

public partial class TeamMembersPage
{
    private TeamMembersViewModel _viewModel;
    public TeamMembersPage(TeamMembersViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }
}
