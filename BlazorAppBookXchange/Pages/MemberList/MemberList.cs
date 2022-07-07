using BlazorAppBookXchange.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorAppBookXchange.Pages.MemberList
{
    public partial class MemberList
    {
        [Inject]
        HttpClient Http { get; set; }

        public List<MembreModel> memberList = new List<MembreModel>();
        protected override async Task OnInitializedAsync()
        {
            memberList = await Http.GetFromJsonAsync<List<MembreModel>>("https://localhost:7144/api/BookXchangeAPI/GetMemberList");
        }

    }
}
