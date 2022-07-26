using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Components.LivreComponents.BookCard
{
    public partial class BookCard
    {
        [Inject] private NavigationManager _navigationManager { get; set; }
        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private AccountManager _accountManager { get; set; }

        public List<LivreModel> bookList = new List<LivreModel>();
        public List<EditionModel> EditionsByBookList { get; set; }
        public int IdSelectedBook { get; set; }
        public string TitreSelectedBook { get; set; }

        public bool voirEditionsClicked;
        protected override async Task OnInitializedAsync()
        {
            //AVEC [Autorize("isConnected")] dans BookXchangeBE.API:

            //bool isTokenPresent = await _accountManager.checkIfTokenStored();
            //if (!_accountManager.IsConnected && !isTokenPresent)
            //{
            //    _navigationManager.NavigateTo("/login");
            //    return;
            //}
            //string Token = await _accountManager.GetToken("token");
            ////bookList = await Http.GetFromJsonAsync<List<LivreModel>>("GetBookList");
            //bookList = await _requester.Get<List<LivreModel>>("GetBookList", Token);


            //SANS [Autorize("isConnected")], AVEC [AllowAnonymous] dans BookXchangeBE.API:
            //bookList = await _requester.Get<List<LivreModel>>("membre/GetBookList"); 

            bookList = await _requester.Get<List<LivreModel>>("livre/ReadLivreList");

            await _accountManager.checkIfTokenStored();

            _accountManager.OnChange += StateHasChanged;
        }


        public void SetSelected(LivreModel book)
        {
            IdSelectedBook = book.IdLivre;
            TitreSelectedBook = book.Titre;
        }

        //public void VoirEditions(int idBook, string nameBook)
        //{
        //    IdSelectedBook = idBook;
        //    TitreSelectedBook = nameBook;
        //    voirEditionsClicked = true;
        //}

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
            _navigationManager.NavigateTo("/");
        }

        public void GoToBookEdit()
        {
            _navigationManager.NavigateTo("/counter");
        }
        public void Dispose()
        {
            _accountManager.OnChange -= StateHasChanged;
        }
    }
}
