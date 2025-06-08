
using Microsoft.Extensions.Logging;
using IMC.View;
using IMC.Model.Repositories;

namespace IMC
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
                    // Fuentes predeterminadas (OpenSans)
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    // Fuentes Raleway (agregadas manualmente)
                    fonts.AddFont("Raleway-VariableFont_wght.ttf", "Raleway");
                    fonts.AddFont("Raleway-Italic-VariableFont_wght.ttf", "RalewayItalic");
                    fonts.AddFont("Raleway-SemiBold.ttf", "RalewaySemiBold");
                });


            //fuentes sacadas: https://fonts.google.com/specimen/Raleway?preview.text=Whereas%20recognition%20of%20the%20inherent%20dignity
#if DEBUG
            builder.Logging.AddDebug();
#endif      
           
            return builder.Build();
        }
    }
}