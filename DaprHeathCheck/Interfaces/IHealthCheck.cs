namespace DaprHeathCheck.Interfaces
{
    public interface IHealthCheck
    {
        Task<HttpResponseMessage> GetHealth();
    }
}
