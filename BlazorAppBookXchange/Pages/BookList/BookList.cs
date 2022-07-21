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

        [Inject] private NavigationManager navigationManager { get; set; }

        [Inject] private ApiRequester _requester { get; set; }

        [Inject] private AccountManager accountManager { get; set; }


        public List<LivreModel> bookList = new List<LivreModel>();

        public List<EditionModel> EditionsByBookList { get; set; }

        public int IdSelectedBook { get; set; }

        public string TitreSelectedBook { get; set; }

        public bool voirEditionsClicked;

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
            ////bookList = await Http.GetFromJsonAsync<List<LivreModel>>("GetBookList");
            //bookList = await _requester.Get<List<LivreModel>>("GetBookList", Token);


            //SANS [Autorize("isConnected")], AVEC [AllowAnonymous] dans BookXchangeBE.API:
            bookList = await _requester.Get<List<LivreModel>>("GetBookList");
               
            await accountManager.checkIfTokenStored();

            accountManager.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            accountManager.OnChange -= StateHasChanged;
        }


        public void SetSelected(int idBook, string nameBook)
        {
            IdSelectedBook = idBook;
            TitreSelectedBook = nameBook;
        }

        public void voirEditions(int idBook, string nameBook)
        {
            IdSelectedBook = idBook;
            TitreSelectedBook = nameBook;
            voirEditionsClicked = true;
        }

        //public async Task<IEnumerable<EditionModel>> GetSelectedBookEditions(LivreModel book)
        //{
        //    if (IdSelectedBook == 0)
        //    {
        //        return new List<EditionModel>();
        //    }
        //    EditionsByBookList = await _requester.Get<List<EditionModel>>($"Edition/GetEditionByLivre/{IdSelectedBook}");
        //    return EditionsByBookList;

        //}


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
