using TeamBuilder.Models.POCO;

namespace TeamBuilder.Api.Services
{
    public interface IApiService
    {
        Task<List<MemberModel>> GetTeamMembers();
        Task PostTeamMember(MemberModel teamMember);
    }
}