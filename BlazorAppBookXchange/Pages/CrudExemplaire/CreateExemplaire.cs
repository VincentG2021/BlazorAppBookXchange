using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Pages.CrudExemplaire
{
    public partial class CreateExemplaire
    {
        //[Inject] HttpClient Http { get; set; }
        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private AccountManager _accountManager { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        public CreateExemplaireModel createExemplaire { get; set; }
        public string Token { get; set; }

        public string defaultValue = "2021-12-15T21:00";

        public CreateExemplaire()
        {
            createExemplaire = new CreateExemplaireModel();

        }

        protected override async Task OnInitializedAsync()
        {
            bool isTokenPresent = await _accountManager.checkIfTokenStored();
            if (!_accountManager.IsConnected && !isTokenPresent)
            {
                _navigationManager.NavigateTo("/login");
                return;
            }
        }

        public async Task SubmitCreateExemplaireForm()
        {
            string Token = await _accountManager.GetToken("token");
            int IdMembre = await _accountManager.GetMemberId("idMembre");

            createExemplaire.IdMembre = IdMembre;

            int responseIdExemplaire = await _requester.Post<CreateExemplaireModel, int>("Exemplaire/CreateExemplaire", createExemplaire, Token);

            if (responseIdExemplaire <0)
            {
                if (Token is null)
                {
                    _navigationManager.NavigateTo("/login");
                    return;
                }
                _navigationManager.NavigateTo("/createExemplaire");
            }
            else
            {
                _navigationManager.NavigateTo("/exemplairelist");
            }


        }
    }
}
