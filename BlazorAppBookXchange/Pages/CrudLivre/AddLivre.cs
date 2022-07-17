using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorAppBookXchange.Pages.CrudLivre
{
    public partial class AddLivre : ComponentBase
    {
        [Inject]
        private ApiRequester _requester { get; set; }

        //[Inject]
        //HttpClient Http { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private AccountManager accountManager { get; set; }

        public AddLivreModel addLivre { get; set; }
        public string Token { get; set; }

        public AddLivre()
        {
            addLivre = new AddLivreModel();
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

            LivreModel lm = await _requester.Post<AddLivreModel, LivreModel>("https://localhost:7144/LivreApi/CreateLivre", addLivre, Token);

            if (lm != null)
            {
                if (Token is null)
                {
                    navigationManager.NavigateTo("/login");
                    return;
                }
                navigationManager.NavigateTo("/booklist");
            }
            else
            {
                navigationManager.NavigateTo("/");
            }


        }
    }
}
