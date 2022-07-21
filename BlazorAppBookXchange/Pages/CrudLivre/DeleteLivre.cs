using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Pages.CrudLivre
{
    public partial class DeleteLivre
    {
        [Inject]
        private ApiRequester _requester { get; set; }

        //[Inject]
        //HttpClient Http { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private AccountManager accountManager { get; set; }

        public string Token { get; set; }

        public DeleteLivre()
        {
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

        public async Task SubmitDeleteLivre(int id)
        {
            string Token = await accountManager.GetToken("token");
            if (Token is null)
            {
                navigationManager.NavigateTo("/login");
                return;
            }

            bool isDeleted = await _requester.Delete<bool>($"Livre/DeleteLivre/", id, Token);

            if (isDeleted)
            {
                navigationManager.NavigateTo("/booklist");
                return;
            }

            navigationManager.NavigateTo("/");
        }

    }
}
