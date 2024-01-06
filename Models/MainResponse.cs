using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace MauiBlazorApp.Models
{
    public class MainResponse
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public AuthenticationResponse? Content { get; set; }
    }
}
