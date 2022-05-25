using festivalprojekt.Shared.Models;


namespace festivalprojekt.Client.Services
{
    public interface ILoginService
    {
        Task<IEnumerable<PersonDTO>> HentLoginPerson(LoginDTO login);
    }
}
