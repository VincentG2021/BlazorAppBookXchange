using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Components.ExemplaireComponents
{
    public partial class ExemplaireList
    {
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private AccountManager accountManager { get; set; }

        public List<ExemplaireModel> exemplairesList = new List<ExemplaireModel>();

        public List<ExemplaireModel> exemplairesByBookList = new List<ExemplaireModel>();

        [Parameter] public int IdSelectedBook { get; set; }
        [Parameter] public string TitreSelectedBook { get; set; }

        public int IdSelectedEdition { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //AVEC [Autorize("isConnected")] dans EditionApi.API:

            //bool isTokenPresent = await accountManager.checkIfTokenStored();
            //if (!accountManager.IsConnected && !isTokenPresent)
            //{
            //    navigationManager.NavigateTo("/login");
            //    return;
            //}
            //string Token = await accountManager.GetToken("token");
            ////exemplairesList = await Http.GetFromJsonAsync<List<ExemplaireModel>>("Exemplaire/GetExemplaireList");
            //exemplairesList = await _requester.Get<List<ExemplaireModel>>("Exemplaire/GetExemplaireList", Token);


            //SANS [Autorize("isConnected")], AVEC [AllowAnonymous] dans BookXchangeBE.API:
            //exemplairesList = await _requester.Get<List<ExemplaireModel>>("Exemplaire/GetExemplaireList");


            if (IdSelectedBook != null)
            {
                exemplairesByBookList = await _requester.Get<List<ExemplaireModel>>($"Exemplaire/GetExemplaireByLivre/{IdSelectedBook}");
            }

            await accountManager.checkIfTokenStored();
            accountManager.OnChange += StateHasChanged;
        }

        private async Task<List<ExemplaireModel>> ShowExemplairesByLivre()
        {
            return exemplairesByBookList = await _requester.Get<List<ExemplaireModel>>($"Exemplaire/GetExemplaireByLivre/{IdSelectedBook}");
            //await OnSelectedBook.InvokeAsync(currentCount);
            //StateHasChanged();
        }

        public void SetSelected(int IdLivre)
        {
            IdSelectedBook = IdLivre;
            StateHasChanged();
        }

        public void Dispose()
        {
            accountManager.OnChange -= StateHasChanged;
        }

        //public void GoToBookDetails(int id)
        //{
        //    navigationManager.NavigateTo("/");
        //}

        //public void GoToBookEdit()
        //{
        //    navigationManager.NavigateTo("/counter");
        //}

    }
}
