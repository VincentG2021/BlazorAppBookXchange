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

        protected override async Task OnInitializedAsync()
        {
            bool isTokenPresent = await accountManager.checkIfTokenStored();
            if (!accountManager.IsConnected && !isTokenPresent)
            {
                navigationManager.NavigateTo("/login");
                return;
            }
            string Token = await accountManager.GetToken("token");
            //bookList = await Http.GetFromJsonAsync<List<LivreModel>>("https://localhost:7144/BookXchangeAPI/GetBookList");
            bookList = await _requester.Get<List<LivreModel>>("https://localhost:7144/BookXchangeAPI/GetBookList", Token);

        }

        public void SetSelected(LivreModel book)
        {
            Selected = book;
        }
    }
}
