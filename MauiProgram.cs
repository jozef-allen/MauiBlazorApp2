using MauiBlazorApp.Services;
using MauiBlazorApp.Interfaces;

namespace MauiBlazorApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            // Add your API service registration
            builder.Services.AddTransient<IMyApiService, MyApiService>();
            builder.Services.AddTransient<IAppService, AppService>();
            builder.Services.AddHttpClient<IMyApiService, MyApiService>(client =>
            {
                client.BaseAddress = new Uri("http://10.0.2.2:5157/");
                //client.BaseAddress = new Uri("https://localhost:7267/");
            });
            builder.Services.AddHttpClient<IAppService, AppService>(client =>
            {
                //client.BaseAddress = new Uri("https://localhost:7267/");
                client.BaseAddress = new Uri("http://10.0.2.2:5157/");
            })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        });

            return builder.Build();
        }

    }
}
