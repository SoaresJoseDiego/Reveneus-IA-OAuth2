namespace MyRecipeBook.Domain.Repositories.User;

public interface IUser
{
    public Task Add(Entities.User user);

    public Task<bool> ExistActiveUserWithEmail(string email);
}
