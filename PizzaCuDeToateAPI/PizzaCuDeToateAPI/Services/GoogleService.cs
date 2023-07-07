using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using PizzaCuDeToateAPI.DTOClasses;

namespace PizzaCuDeToateAPI.Services;

public class GoogleService : IGoogleService
{
    public async Task<GoogleUserDTO> GetUserData(string accessToken)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await client.GetAsync("https://www.googleapis.com/oauth2/v3/userinfo");
            string userInfoJsonString = await response.Content.ReadAsStringAsync();
            GoogleUserDTO? userInfo = JsonSerializer.Deserialize<GoogleUserDTO>(userInfoJsonString);
            return userInfo!;
        }
    }
}