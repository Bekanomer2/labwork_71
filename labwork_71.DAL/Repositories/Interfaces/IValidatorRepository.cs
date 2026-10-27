namespace labwork_71.DAL.Repositories.Interfaces;

public interface IValidatorRepository
{
    Task<bool> CheckEmail(string email, int? id);
}