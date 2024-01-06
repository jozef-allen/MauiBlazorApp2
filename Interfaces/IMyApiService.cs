using MauiBlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorApp.Interfaces
{
    public interface IMyApiService
    {
        Task<List<UserDTO>> GetUsers();
        Task<UserDTO> GetUserById(int userId);
        Task<HttpStatusCode> AddUser(UserDTO newUser);
        Task<HttpStatusCode> UpdateUser(UserDTO updatedUser);
        Task<HttpStatusCode> DeleteUser(int userId);
    }
}
