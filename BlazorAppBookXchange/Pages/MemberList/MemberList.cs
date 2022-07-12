using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorAppBookXchange.Pages.MemberList
{
    public partial class MemberList
    {

        [Inject]
        private ApiRequester _requester { get; set; }

        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private AccountManager accountManager { get; set; }



        public List<MembreModel> memberList = new List<MembreModel>();
        protected override async Task OnInitializedAsync()
        {
            bool isTokenPresent = await accountManager.checkIfTokenStored();
            if (!accountManager.IsConnected && !isTokenPresent)
            {
                navigationManager.NavigateTo("/login");
                return;
            }

            string Token = await accountManager.GetToken("token");

            //memberList = await Http.GetFromJsonAsync<List<MembreModel>>("https://localhost:7144/BookXchangeAPI/GetMemberList");
            memberList = await _requester.Get<List<MembreModel>>("https://localhost:7144/BookXchangeAPI/GetMemberList", Token);


        }
    }
}
