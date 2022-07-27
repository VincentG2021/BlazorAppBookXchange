using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Shared.SuccesNotification;
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
        public EditMembreCardModel editMembre { get; set; } = new EditMembreCardModel();

        //public bool InputDialogOpen { get; set; } // Cette propriété détermine si ModalDialog doit s'acfficher

        //private string _imageUrl;

        //private SuccessNotification _notification;


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

        //public async Task EditImage()
        //{
        //    _notification.Show();
        //}

        //private void OpenInputDialog(string imgUrl)
        //{
        //    _imageUrl = imgUrl;
        //    InputDialogOpen = true;
        //}

        //private async Task OnInputDialogClose(bool accepted)
        //{
        //    if (accepted)
        //    {
        //        await _accountManager.checkIfTokenStored();
        //        string token = await _accountManager.GetToken("token");

        //        Console.WriteLine($"Image url: {_imageUrl}");
        //        bool result = await _requester.Update<bool>($"membre/image/", _imageUrl, connectedMember.IdMembre, token);
        //        Console.WriteLine(result);
        //        _navigationManager.NavigateTo("/profilmembre");
        //    }
        //    InputDialogOpen = false;
        //    StateHasChanged();
        //}


        public async Task SubmitEditMembreForm()
        {
            string Token = await _accountManager.GetToken("token");
            int IdMembre = await _accountManager.GetMemberId("idMembre");
            editMembre.IdMembre = IdMembre;
            bool responseUpdated = await _requester.Put<EditMembreCardModel, bool>("membre/UpdateMembre", editMembre, Token);

            if (!responseUpdated)
            {
                if (Token is null)
                {
                    _navigationManager.NavigateTo("/login");
                    return;
                }
                _navigationManager.NavigateTo("/profilmembre");
            }
            else
            {
                _navigationManager.NavigateTo("/profilmembre");
            }
        }

    }
}
