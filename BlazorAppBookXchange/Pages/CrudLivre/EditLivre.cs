using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorAppBookXchange.Pages.CrudLivre
{
    public partial class EditLivre : ComponentBase
    {
        [Inject]
        private ApiRequester _requester { get; set; }

        //[Inject]
        //HttpClient Http { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private AccountManager accountManager { get; set; }

        public EditLivreModel editLivre { get; set; }
        public string Token { get; set; }

        public EditLivre()
        {
            editLivre = new EditLivreModel();
        }

        protected override async Task OnInitializedAsync()
        {
            bool isTokenPresent = await accountManager.checkIfTokenStored();
            if (!accountManager.IsConnected && !isTokenPresent)
            {
                navigationManager.NavigateTo("/login");
                return;
            }
        }

        public async Task SubmitEditLivreForm()
        {
            string Token = await accountManager.GetToken("token");
            if (Token is null)
            {
                navigationManager.NavigateTo("/login");
                return;
            }

            bool isUpdated = await _requester.Put<EditLivreModel, bool>("https://localhost:7144/LivreApi/UpdateLivre", editLivre, Token);

            if (isUpdated)
            {
                navigationManager.NavigateTo("/booklist");
                return;
            }

            navigationManager.NavigateTo("/");
        }
    }
}
