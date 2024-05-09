namespace ServiceCompany.Hubs
{
    public interface IAlertHub
    {
        Task PushAlertAsync(string message, DateTime date, int alertId);
    }
}