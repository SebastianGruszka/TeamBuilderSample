using TeamBuilder.Models.POCO;

namespace TeamBuilder.TeamMembers.Domain;

public interface ITeamMembersRepository
{
    /// <summary>
    /// This method should allow the client to add team member
    /// </summary>
    /// <param name="teamMember">It's object but you can change it to whatever you like :)</param>
    Task AddTeamMember(MemberModel teamMember);

    /// <summary>
    /// Adds the team member.
    /// </summary>
    /// <param name="teamMembersList">The team members list.</param>
    /// <returns>A Task.</returns>
    Task AddTeamMember(List<MemberModel> teamMembersList);


    /// <summary>
    /// This method allows client to get GetAllTeamMembers. IList<object> needs to be adjusted. Probably :)
    /// </summary>
    Task<List<MemberModel>> GetTeamMembers();
}
