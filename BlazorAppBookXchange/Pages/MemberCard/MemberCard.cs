using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Pages.MemberCard
{
    public partial class MemberCard
    {
        [Inject]
        private AccountManager accountManager { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private ApiRequester _requester { get; set; }

        public ConnectedMember connectedMember = new ConnectedMember();

        protected override async Task OnInitializedAsync()
        {
            bool isTokenPresent = await accountManager.checkIfTokenStored();
            if (!accountManager.IsConnected && !isTokenPresent)
            {
                navigationManager.NavigateTo("/login");
                return;
            }

            string Token = await accountManager.GetToken("token");
            connectedMember.Pseudo = await accountManager.GetPseudo("pseudo");
            //string Pseudo = await _requester.Get<ConnectedMember>("https://localhost:7144/BookXchangeAPI/GetConnectedMember/", Token);

            //memberList = await Http.GetFromJsonAsync<List<MembreModel>>("https://localhost:7144/BookXchangeAPI/GetMemberList");
            //connectedMember = await _requester.Get<ConnectedMember>($"https://localhost:7144/BookXchangeAPI/GetConnectedMember/{Pseudo}", Token);


        }
    }
}
