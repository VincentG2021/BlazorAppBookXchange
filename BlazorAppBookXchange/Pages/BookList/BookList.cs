using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorAppBookXchange.Pages.BookList
{
    public partial class BookList
    {
        //[Inject]
        //HttpClient Http { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private ApiRequester _requester { get; set; }

        [Inject]
        private AccountManager accountManager { get; set; }


        public List<LivreModel> bookList = new List<LivreModel>();

        public LivreModel Selected { get; set; }

        public List<EditionModel> EditionsByBookList { get; set; }

        public int IdLivre { get; set; }


        protected override async Task OnInitializedAsync()
        {
            //AVEC [Autorize("isConnected")] dans BookXchangeBE.API:

            //bool isTokenPresent = await accountManager.checkIfTokenStored();
            //if (!accountManager.IsConnected && !isTokenPresent)
            //{
            //    navigationManager.NavigateTo("/login");
            //    return;
            //}
            //string Token = await accountManager.GetToken("token");
            ////bookList = await Http.GetFromJsonAsync<List<LivreModel>>("https://localhost:7144/BookXchangeAPI/GetBookList");
            //bookList = await _requester.Get<List<LivreModel>>("https://localhost:7144/BookXchangeAPI/GetBookList", Token);


            //SANS [Autorize("isConnected")], AVEC [AllowAnonymous] dans BookXchangeBE.API:
            bookList = await _requester.Get<List<LivreModel>>("https://localhost:7144/BookXchangeAPI/GetBookList");
               
            await accountManager.checkIfTokenStored();

            accountManager.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            accountManager.OnChange -= StateHasChanged;
        }


        public void SetSelected(LivreModel book)
        {
            Selected = book;
        }

        public async Task<IEnumerable<EditionModel>> GetSelectedBookEditions(LivreModel book)
        {
            Selected = book;

            if (Selected is null)
            {
                return new List<EditionModel>();
            }
                IdLivre = Selected.IdLivre;
            EditionsByBookList = await _requester.Get<List<EditionModel>>($"https://localhost:7144/EditionApi/GetEditionByLivre/{IdLivre}");
            return EditionsByBookList;

        }


        public void GoToBookDetails(int id)
        {
            navigationManager.NavigateTo("/");
        }

        public void GoToBookEdit()
        {
            navigationManager.NavigateTo("/counter");
        }
    }
}
