using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TeamBuilder.Models.POCO;

namespace TeamBuilder.Api.Services
{
    public class ApiService : IApiService
    {

        #region Fields
        HttpClient _client = new();
        HttpRequestMessage _request = new();
        Guid _guid = Guid.NewGuid();
        string _url = string.Empty;
        #endregion

        #region Constructor
        public ApiService()
        {
        }
        #endregion

        #region Private Methods

        #endregion

        #region Public Methods
        public async Task<List<MemberModel>> GetTeamMembers()
        {
            List<MemberModel> membersList = new();
            _url = $"https://localhost:7222/api/v1/Team/{_guid}" + "/Members";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, _url);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

                var response = _client.GetAsync(request.ToString()).ContinueWith(async (respMsg) =>
                {
                    var res = respMsg.Result;
                    var members = await res.Content.ReadFromJsonAsync<List<MemberModel>>();

                    foreach (var item in members.Where(x => x.IsActive == true)) // Only add active members to the list
                    {
                        membersList.Add(item);
                    }
                });
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex.ToString());
#endif           
            }
            return membersList;
        }

        public async Task PostTeamMember(MemberModel teamMember)
        {
            try
            {
                Guid _guid = Guid.NewGuid();
                string url = $"https://localhost:7222/api/v1/Team/{_guid}" + "/AddMember";

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

                var response = _client.SendAsync(request).ContinueWith(async (respMsg) =>
                {
                    var res = respMsg.Result;

                    if (res.IsSuccessStatusCode)
                    {
                        return;
                    }
                });
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex.ToString());
#endif
            }
        }
        #endregion
    }
}
