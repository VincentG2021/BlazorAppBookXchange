namespace BlazorAppBookXchange.Services
{
    public class RefreshService
    {
        public event Action RefreshRequested;
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }

    //child component
    //RefreshService.CallRequestRefresh();


    //parent component
    //RefreshService.RefreshRequested += RefreshMe;

    //private void RefreshMe()
    //    {
    //        StateHasChanged();
    //    }
}
