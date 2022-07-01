using BlazorAppBookXchange.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorAppBookXchange.Pages.BookList
{
    public partial class BookList
    {
        [Inject]
        HttpClient Http { get; set; }

        public List<LivreModel> bookList = new List<LivreModel>();
        protected override async Task OnInitializedAsync()
        {
            bookList = await Http.GetFromJsonAsync<List<LivreModel>>("https://localhost:7003/api/BookXchangeAPI/GetBookList");
        }
    }
}
