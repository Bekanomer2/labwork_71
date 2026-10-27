namespace labwork_71.BLL.Services.Interfaces;

public interface IValidatorService
{
    Task<bool> CheckEmail(string? email, int? id);
    bool CheckPassword(string? password);
}