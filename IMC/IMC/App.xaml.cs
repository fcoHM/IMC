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
            services.AddSingleton<IPaciente, PacienteService>();//registramos el servicio de paciente
            services.AddSingleton<IMedicion, MedicionService>(); //registramos el servicio de medicion
            services.AddSingleton<VMMainPage>(); //registramos el view model de la pagina principal
            services.AddSingleton<MainPage>(); //registramos la pagina principal
            services.AddTransient<HistorialMediciones>(); //registramos el view model de la pagina de registro
            services.AddSingleton<Home>(); //registramos la pagina home
            services.AddTransient<MiInformacion>(); //registramos el view model de la pagina de historial de mediciones
            services.AddTransient<VMMiInformacion>(); //registramos el view model de la pagina de historial de mediciones
            services.AddSingleton<AppShell>(); //registramos el shell de la aplicacion
            services.AddTransient<VMPageRegister>(); //registramos el view model de la pagina de registro
            services.AddSingleton<VMHistorialMediciones>(); //registramos el view model de la pagina calculadora
            services.AddSingleton<PageRegister>(); //registramos la pagina de registro
            services.AddTransient<Calculadora>(); //registramos la pagina calculadora
            services.AddTransient<VMCalculadora>(); //registramos el view model de la pagina calculadora
            services.AddSingleton<VMHome>(); //registramos el view model de la pagina home
            services.AddSingleton<SesionPacienteService>(); //registramos el servicio de sesion de paciente
            return services.BuildServiceProvider(); //construimos el proveedor de servicios

        }
    }
}