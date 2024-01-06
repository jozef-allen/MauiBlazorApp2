using MauiBlazorApp.Interfaces;
using MauiBlazorApp.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorApp.Services
{
    public class AppService : IAppService
    {
        private readonly HttpClient _httpClient;

        public AppService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.Timeout = TimeSpan.FromSeconds(1000);
        }

        //public async Task<string> AuthenticateUser(LoginModel loginModel)
        //{
        //    string returnStr = string.Empty;

        //    var serializedStr = JsonConvert.SerializeObject(loginModel);

        //    var content = new StringContent(serializedStr, Encoding.UTF8, "application/json");

        //    var response = await _httpClient.PostAsync("api/Registration/AuthenticateUser", content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        returnStr = await response.Content.ReadAsStringAsync();
        //    }

        //    return returnStr;
        //}


        public async Task<MainResponse> AuthenticateUser(LoginModel loginModel)
        {
            var returnResponse = new MainResponse();

            try
            {
                var serializedStr = JsonConvert.SerializeObject(loginModel);
                var content = new StringContent(serializedStr, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Registration/AuthenticateUser", content);

                if (response.IsSuccessStatusCode)
                {
                    // We use await response.Content.ReadAsStringAsync() to get the JSON string. Once we have the JSON string, we can use JsonConvert.DeserializeObject<MainResponse>() to deserialize it into a MainResponse object. This is coming as a HTTP response with a MainResponse inside.
                    returnResponse = JsonConvert.DeserializeObject<MainResponse>(await response.Content.ReadAsStringAsync());
                    returnResponse.IsSuccess = true;
                }
                else
                {
                    returnResponse.ErrorMessage = $"Error: {response.StatusCode}";
                }
            }
            catch (TaskCanceledException)
            {
                returnResponse.ErrorMessage = "The request timed out. Please check connection to the API.";
            }

            return returnResponse;
        }

        public async Task<MainResponse> RefreshToken(AuthenticationResponse twoTokens)
        {
            var returnResponse = new MainResponse();
            var serializedStr = JsonConvert.SerializeObject(twoTokens);
            var content = new StringContent(serializedStr, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Registration/RefreshToken", content);

            if (response.IsSuccessStatusCode)
            {
                string contentStr = await response.Content.ReadAsStringAsync();
                returnResponse = JsonConvert.DeserializeObject<MainResponse>(contentStr);
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                // Handle BadRequest response
                string errorContent = await response.Content.ReadAsStringAsync();
                returnResponse.ErrorMessage = $"Error: {response.StatusCode}, {errorContent}";
            }
            else
            {
                // Handle other non-successful responses
                returnResponse.ErrorMessage = $"Error: {response.StatusCode}";
            }

            return returnResponse;
        }





        public async Task<(bool IsSuccess, string ErrorMessage)> RegisterUser(RegistrationModel registerUser)
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            
            var serializedStr = JsonConvert.SerializeObject(registerUser);
            var content = new StringContent(serializedStr, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Registration/RegisterUser", content);
            if (response.IsSuccessStatusCode)
            {
                isSuccess = true;
            } else
            {
                errorMessage= await response.Content.ReadAsStringAsync();
            }
            return (isSuccess, errorMessage);
        }
    }
}
