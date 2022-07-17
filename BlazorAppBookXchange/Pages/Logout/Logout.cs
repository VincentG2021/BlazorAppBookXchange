using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Pages.Logout
{
    public partial class Logout : ComponentBase
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private AccountManager accountManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await accountManager.RemoveToken("token");
            await accountManager.RemoveMemberInfos();

            navigationManager.NavigateTo("/");

        }

    }
}
