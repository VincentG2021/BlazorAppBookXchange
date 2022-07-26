using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Pages.Logout
{
    public partial class Logout : ComponentBase
    {
        [Inject] private NavigationManager _navigationManager { get; set; }
        [Inject] private AccountManager _accountManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await _accountManager.RemoveToken("token");
            await _accountManager.RemoveMemberInfos();
            _navigationManager.NavigateTo("/");
        }
    }
}
