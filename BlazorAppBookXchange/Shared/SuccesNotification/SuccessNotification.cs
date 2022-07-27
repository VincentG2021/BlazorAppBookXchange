
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Shared.SuccesNotification
{
    public partial class SuccessNotification
    {
        private string _modalDisplay;
        private string _modalClass;
        private bool _showBackdrop;

        [Inject] public NavigationManager NavigationManager { get; set; }

        public void Show()
        {
            _modalDisplay = "block;";
            _modalClass = "show";
            _showBackdrop = true;
            StateHasChanged();
        }
        private void Hide()
        {
            _modalDisplay = "none;";
            _modalClass = "";
            _showBackdrop = false;
            StateHasChanged();
            NavigationManager.NavigateTo("/profilmembre");
        }
    }
}
