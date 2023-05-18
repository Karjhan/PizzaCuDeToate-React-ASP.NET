using Google.Apis.PeopleService.v1.Data;
using PizzaCuDeToateAPI.DTOClasses;

namespace PizzaCuDeToateAPI.Services;

public interface IGoogleService
{
    Task<GoogleUserDTO> GetUserData(string accessToken);
}