using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorAppBookXchange.Components.LivreComponents.BookList
{
    public partial class BookList
    {
        [Inject]
        HttpClient Http { get; set; }

        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private AccountManager _accountManager { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        public List<LivreModel> bookList = new List<LivreModel>();
        private LivreModel _selectedBook = new LivreModel() { IdLivre = 0 };
        public int IdSelectedBook { get; set; }
        public string? TitreSelectedBook { get; set; }

        private bool _ShowExemplaires;
        private bool _ShowEditions;

        protected override async Task OnInitializedAsync()
        {
            //GetBookList AVEC [Autorize("isConnected")] dans Livre.API:

            //bool isTokenPresent = await accountManager.checkIfTokenStored();
            //if (!accountManager.IsConnected && !isTokenPresent)
            //{
            //    navigationManager.NavigateTo("/login");
            //    return;
            //}
            //string Token = await accountManager.GetToken("token");
            ////bookList = await Http.GetFromJsonAsync<List<LivreModel>>("GetBookList");
            //bookList = await _requester.Get<List<LivreModel>>("GetBookList", Token);

            await _accountManager.checkIfTokenStored();
            _accountManager.OnChange += StateHasChanged;

            //GetBookList SANS [Autorize("isConnected")], AVEC [AllowAnonymous] dans Livre.API:
            //bookList = await _requester.Get<List<LivreModel>>("membre/GetBookList"); 
            bookList = await _requester.Get<List<LivreModel>>("livre/ReadLivreList");
        }

        public void ShowEditionsByBook(LivreModel book)
        {
            //Console.WriteLine($"at starting method: {_ShowEditions}");
            if (_ShowEditions)
            { 
                _ShowEditions = false;
            }
            //Console.WriteLine($"after checking and assigning false:{_ShowEditions}");
            _selectedBook = book;
            _ShowEditions = true;
            //Console.WriteLine($"after assigning true and before assigning IdSelectedBook:{_ShowEditions}");
            IdSelectedBook = book.IdLivre;
            TitreSelectedBook = book.Titre;
            StateHasChanged();
        }

        public void ShowExemplairesByBook(LivreModel book)
        {
            //Console.WriteLine($"at starting method: {_ShowExemplaires}");
            if (_ShowExemplaires)
            {
                _ShowExemplaires = false;
            }
            //Console.WriteLine($"after checking and assigning false: {_ShowExemplaires}");
            _selectedBook = book;
            _ShowExemplaires = true;
            //Console.WriteLine($"after assigning true and before assigning IdSelectedBook: {_ShowExemplaires}");
            IdSelectedBook = book.IdLivre;
            TitreSelectedBook = book.Titre;
        }

        public void GoToBookDetails(int id)
        {
            _navigationManager.NavigateTo("/");
        }

        public void GoToBookEdit()
        {
            _navigationManager.NavigateTo("/editlivre");
        }

        public void Dispose()
        {
            _accountManager.OnChange -= StateHasChanged;
        }
    }
}
