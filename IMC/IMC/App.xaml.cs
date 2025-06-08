using IMC.Auxiliares;
using IMC.View;

namespace IMC
{
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }
        public App(IServiceProvider serviceProvider)
        {
            var services = new ServiceCollection();
            Services = ConfigureServices(services);
            InitializeComponent();
            MainPage = new AppShell(); //inicializamos la pagina principal

        }

        private static IServiceProvider ConfigureServices(ServiceCollection services)
        { //propio conector de dependencias
            services.AddSingleton<IPaciente, PacienteService>();
            services.AddSingleton<VMMainPage>();
            services.AddSingleton<MainPage>();
            services.AddSingleton<Home>();
            services.AddSingleton<AppShell>(); //registramos el shell de la aplicacion
            services.AddTransient<VMPageRegister>();
            services.AddSingleton<PageRegister>();
            services.AddSingleton<VMHome>(); //registramos el view model de la pagina home
            services.AddSingleton<SesionPacienteService>(); //registramos el servicio de sesion de paciente
            return services.BuildServiceProvider(); //construimos el proveedor de servicios

        }
    }
}