using MauiBlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorApp.Interfaces
{
    public interface IAppService
    {
        public Task<MainResponse> AuthenticateUser(LoginModel loginModel);
        public Task<(bool IsSuccess, string ErrorMessage)> RegisterUser(RegistrationModel registerUser);
        public Task<MainResponse> RefreshToken(AuthenticationResponse twoTokens);
    }
}
