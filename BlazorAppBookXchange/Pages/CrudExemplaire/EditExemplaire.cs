using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Pages.CrudExemplaire
{
    public partial class EditExemplaire
    {
        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private AccountManager _accountManager { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }
        [Parameter] public int idExemplaire { get; set; } 
        public EditExemplaireModel editExemplaire { get; set; } = new EditExemplaireModel();
        public string Token { get; set; }
        public string defaultValue = "2021-12-15T21:00";

        protected override async Task OnInitializedAsync()
        {
            bool isTokenPresent = await _accountManager.checkIfTokenStored();
            if (!_accountManager.IsConnected && !isTokenPresent)
            {
                _navigationManager.NavigateTo("/login");
                return;
            }
        }
        protected override async Task OnParametersSetAsync()
        {
            await GetExemplaireById();
        }

        public async Task<EditExemplaireModel> GetExemplaireById()
        {
            await _accountManager.checkIfTokenStored();
            string token = await _accountManager.GetToken("token");
            editExemplaire = await _requester.Get<EditExemplaireModel>($"Exemplaire/GetExemplaireById/{idExemplaire}", token);
            return editExemplaire;
        }

        public async Task SubmitEditExemplaireForm()
        {
            string Token = await _accountManager.GetToken("token");
            int IdMembre = await _accountManager.GetMemberId("idMembre");
            editExemplaire.IdMembre = IdMembre;
            bool responseUpdated = await _requester.Put<EditExemplaireModel, bool>("Exemplaire/UpdateExemplaire", editExemplaire, Token);

            if (!responseUpdated)
            {
                if (Token is null)
                {
                    _navigationManager.NavigateTo("/login");
                    return;
                }
                _navigationManager.NavigateTo("/editExemplaire");
            }
            else
            {
                _navigationManager.NavigateTo("/exemplairelistbymembre");
            }
        }
    }
}
