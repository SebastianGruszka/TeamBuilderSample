using System.Text.Json;
using TeamBuilder.Models.Consts;
using TeamBuilder.Models.POCO;
using TeamBuilder.TeamMembers.Domain;
using TeamBuilder.Validations;

namespace TeamBuilder.TeamMembers.Infrastructure
{
    public class DummyTeamMembersRepository : ITeamMembersRepository
    {
        private List<MemberModel> _teamMemberList = new();
        private TextValidator _textvalidator = new();
        private PhoneNumberValidator _phoneNumberValidator = new();
        private int _listCount;


        /// <summary>
        /// Initializes a new instance of the <see cref="DummyTeamMembersRepository"/> class.
        /// </summary>
        public DummyTeamMembersRepository()
        {
        }

        /// <summary>
        /// Adds the team member.
        /// </summary>
        /// <param name="teamMember">The team member.</param>
        /// <returns>A Task.</returns>
        public Task AddTeamMember(MemberModel teamMember)
        {
            var bName = _textvalidator.TextValidation(teamMember.Name);
            var bNickName = _textvalidator.TextValidation(teamMember.NickName);
            var bPosition = _textvalidator.TextValidation(teamMember.Position);
            var bPhone = _phoneNumberValidator.PhoneNumberIsValid(teamMember.PhoneNumber);

            if (bName == true && bPhone == true)
            {
                if (teamMember != null)
                {
                    _teamMemberList.Add(teamMember);

                    var json = JsonSerializer.Serialize(_teamMemberList);
                    int _listCount = _teamMemberList.Count();

                    SecureStorage.SetAsync(MemberConst.MEMBER + _listCount, json);
                }
            }
            return Task.CompletedTask;
        }


        /// <summary>
        /// Gets the team members.
        /// </summary>
        /// <returns>A Task.</returns>
        public async Task<List<MemberModel>> GetTeamMembers()
        {
            string json;
            List<MemberModel> list = new List<MemberModel>();
            int _listCount = _teamMemberList.Count();

            for (int i = 0; i < _listCount; i++)
            {
                json = await SecureStorage.GetAsync(MemberConst.MEMBER + i);
                var obj = JsonSerializer.Deserialize<MemberModel>(json);

                _teamMemberList.Add(obj);
            }
            return _teamMemberList;
        }
    }
}

