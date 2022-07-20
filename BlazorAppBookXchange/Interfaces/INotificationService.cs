namespace BlazorAppBookXchange.Interfaces
{
    public interface INotificationService
    {
        event Action OnChange;
        //Task<ServiceResponse<Notification>> AddNotificationAsync(Notification notification);
    }
}
