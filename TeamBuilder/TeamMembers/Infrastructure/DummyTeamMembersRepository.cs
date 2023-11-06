using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using TeamBuilder.Models.Enums;
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
        HttpClient _client = new();
        HttpRequestMessage _request = new();

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
                }

                var json = JsonSerializer.Serialize(_teamMemberList);

                SecureStorage.SetAsync(MembersEnum.member.ToString(), json);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Adds the team member.
        /// </summary>
        /// <param name="teamMembersList">The team members list.</param>
        /// <returns>A Task.</returns>
        public Task AddTeamMember(List<MemberModel> teamMembersList)
        {
            if (teamMembersList != null)
            {
                foreach (var item in teamMembersList)
                {
                    _teamMemberList.Add(new MemberModel()
                    {
                        IsActive = item.IsActive,
                        Name = item.Name,
                        NickName = item.NickName,
                        PhoneNumber = item.PhoneNumber,
                        Position = item.Position
                    });
                }
            }

            var json = JsonSerializer.Serialize(_teamMemberList);


            SecureStorage.SetAsync(MembersEnum.member.ToString(), json);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets the team members.
        /// </summary>
        /// <returns>A Task.</returns>
        public async Task<List<MemberModel>> GetTeamMembers()
        {
            Guid _guid = Guid.NewGuid();

            try
            {
                string url = $"https://localhost:7222/api/v1/Team/{_guid}"+"/Members";

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

                var response = _client.GetAsync(request.ToString()).ContinueWith(async(respMsg)=> 
                {
                    var res = respMsg.Result;
                    var members = await res.Content.ReadFromJsonAsync<List<MemberModel>>();

                    foreach (var item in members)
                    {
                        _teamMemberList.Add(item);
                    }

                    string json = SecureStorage.GetAsync("member").ToString();

                    var list = JsonSerializer.Deserialize<List<MemberModel>>(json);

                    foreach (var oldMember in list)
                    {
                        _teamMemberList.Add(oldMember);
                    }                    
                });
                return _teamMemberList;

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                string json = SecureStorage.GetAsync(MembersEnum.member.ToString()).ToString();

                var list = JsonSerializer.Deserialize<List<MemberModel>>(json);

                foreach (var oldMember in list)
                {
                    _teamMemberList.Add(oldMember);
                }

                return _teamMemberList;
            }
        }
    }
}

