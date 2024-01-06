using MauiBlazorApp.Interfaces;
using MauiBlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiBlazorApp.Services
{
    public class MyApiService : IMyApiService
    {
        private readonly HttpClient _httpClient;

        public MyApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.Timeout = TimeSpan.FromSeconds(10);
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            var response = await _httpClient.GetAsync("api/User/GetUsers");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<UserDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            var response = await _httpClient.GetAsync($"api/User/GetUserById?Id={userId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<HttpStatusCode> AddUser(UserDTO newUser)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(newUser), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/User/InsertUser", jsonContent);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> UpdateUser(UserDTO updatedUser)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(updatedUser), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("api/User/UpdateUser", jsonContent);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteUser(int userId)
        {
            var response = await _httpClient.DeleteAsync($"api/User/DeleteUser/{userId}");
            return response.StatusCode;
        }
        // Add other methods as needed
    }
}
