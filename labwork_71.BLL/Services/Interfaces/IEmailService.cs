namespace labwork_71.BLL.Services.Interfaces;

public interface IEmailService
{
    Task SendMessageAsync(string email, string subject, string body );
}