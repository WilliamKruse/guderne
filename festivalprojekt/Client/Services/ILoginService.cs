using festivalprojekt.Shared.Models;


namespace festivalprojekt.Client.Services
{
    //laver interfacet med metoder til klassen LoginDTO 
    public interface ILoginService
    {
        Task<IEnumerable<PersonDTO>> HentLoginPerson(LoginDTO login);
    }
}
