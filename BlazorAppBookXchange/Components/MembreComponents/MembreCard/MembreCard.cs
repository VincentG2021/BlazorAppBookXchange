using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Components.MembreComponents.MembreCard
{
    public partial class MembreCard
    {
        [Inject] private AccountManager _accountManager { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }
        [Inject] private ApiRequester _requester { get; set; }

        public ConnectedMember connectedMember = new ConnectedMember();

        private bool isEditing { get; set; }

        protected override async Task OnInitializedAsync()
        {
            bool isTokenPresent = await _accountManager.checkIfTokenStored();
            if (!_accountManager.IsConnected && !isTokenPresent)
            {
                _navigationManager.NavigateTo("/login");
                return;
            }

            await GetMemberInfo();
            //string Pseudo = await _requester.Get<ConnectedMember>("GetConnectedMember/", Token);

            //memberList = await Http.GetFromJsonAsync<List<MembreModel>>("GetMemberList");
            //connectedMember = await _requester.Get<ConnectedMember>($"GetConnectedMember/{Pseudo}", Token);
        }

        public async Task<ConnectedMember> GetMemberInfo()
        {
            string Token = await _accountManager.GetToken("token");
            connectedMember.Pseudo = await _accountManager.GetPseudo("pseudo");
            connectedMember.IdMembre = await _accountManager.GetMemberId("idMembre");
            connectedMember = await _requester.Get<ConnectedMember>($"membre/GetMembreById/{connectedMember.IdMembre}", Token);
            return connectedMember;

        }
        public void ToggleEditing()
        {
            isEditing = !isEditing;
        }
    }
}
