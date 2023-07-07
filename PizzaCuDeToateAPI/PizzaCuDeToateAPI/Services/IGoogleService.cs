using System.Threading.Tasks;
using PizzaCuDeToateAPI.DTOClasses;

namespace PizzaCuDeToateAPI.Services;

public interface IGoogleService
{
    Task<GoogleUserDTO> GetUserData(string accessToken);
}