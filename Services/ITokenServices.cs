using CatalogoAPI.Models;

namespace CatalogoAPI.Services;

public interface ITokenServices
{
    string GetToken(string key, string issuer, string audience, UserModel userModel);
}
